using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Models;

namespace RentalSystemUI.Data
{
    public class LandlordRepository : Database
    {
        public (int TenantId, string PropertyTitle)? GetBookingNotificationInfo(int bookingId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"
                    SELECT b.TenantID, p.Title
                    FROM BOOKINGS b
                    JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                    WHERE b.BookingID=@bid", conn))
                {
                    cmd.Parameters.AddWithValue("@bid", bookingId);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) return null;
                        return ((int)r["TenantID"], r["Title"].ToString() ?? string.Empty);
                    }
                }
            }
        }

        // --- BOOKINGS (Requests) ---
        public List<BookingWithProperty> GetBookingsByLandlord(int landlordId)
        {
            var list = new List<BookingWithProperty>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT b.*, u.FullName as TenantName,
                           p.Title as PropertyTitle, p.Address, p.City, p.RentAmount
                    FROM BOOKINGS b
                    JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                    JOIN USERS u ON b.TenantID = u.UserID
                    WHERE p.LandlordID = @lid
                    ORDER BY b.CreatedAt DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lid", landlordId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new BookingWithProperty
                            {
                                BookingID = (int)reader["BookingID"],
                                PropertyID = (int)reader["PropertyID"],
                                TenantID = (int)reader["TenantID"],
                                StartDate = (DateTime)reader["StartDate"],
                                EndDate = (DateTime)reader["EndDate"],
                                DurationMonths = reader["DurationMonths"] as int?,
                                TotalAmount = (decimal)reader["TotalAmount"],
                                Status = reader["Status"].ToString() ?? "Pending",
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                TenantName = reader["TenantName"].ToString() ?? "",
                                PropertyTitle = reader["PropertyTitle"].ToString() ?? "",
                                PropertyAddress = $"{reader["Address"]}, {reader["City"]}",
                                MonthlyRent = (decimal)reader["RentAmount"]
                            });
                        }
                    }
                }
            }
            return list;
        }

        public void UpdateBookingStatus(int bookingId, string status)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                int propId = 0;
                using (var cmdGet = new SqlCommand("SELECT PropertyID FROM BOOKINGS WHERE BookingID = @id", conn))
                {
                    cmdGet.Parameters.AddWithValue("@id", bookingId);
                    object res = cmdGet.ExecuteScalar();
                    if (res != null) propId = (int)res;
                }

                // If approving a booking, reject other pending bookings for same property
                // AND mark property as Rented
                if (status == "Approved" && propId > 0)
                {
                    // Reject other pending bookings
                    string rejectQuery = "UPDATE BOOKINGS SET Status='Rejected' WHERE PropertyID=@pid AND BookingID<>@id AND Status='Pending'";
                    using (var cmdReject = new SqlCommand(rejectQuery, conn))
                    {
                        cmdReject.Parameters.AddWithValue("@pid", propId);
                        cmdReject.Parameters.AddWithValue("@id", bookingId);
                        cmdReject.ExecuteNonQuery();
                    }

                    // Mark property as Rented
                    using (var cmdProp = new SqlCommand("UPDATE PROPERTIES SET Status='Rented', AvailabilityStatus=0 WHERE PropertyID=@pid", conn))
                    {
                        cmdProp.Parameters.AddWithValue("@pid", propId);
                        cmdProp.ExecuteNonQuery();
                    }
                }

                // If terminating/rejecting, check if we should make property available again
                if ((status == "Terminated" || status == "Rejected") && propId > 0)
                {
                    // Check if there are any other approved bookings for this property
                    int approvedCount = 0;
                    using (var cmdCheck = new SqlCommand("SELECT COUNT(*) FROM BOOKINGS WHERE PropertyID=@pid AND Status='Approved' AND BookingID<>@id", conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@pid", propId);
                        cmdCheck.Parameters.AddWithValue("@id", bookingId);
                        approvedCount = (int)cmdCheck.ExecuteScalar();
                    }

                    // If no other approved bookings, make property available
                    if (approvedCount == 0)
                    {
                        using (var cmdProp = new SqlCommand("UPDATE PROPERTIES SET Status='Available', AvailabilityStatus=1 WHERE PropertyID=@pid", conn))
                        {
                            cmdProp.Parameters.AddWithValue("@pid", propId);
                            cmdProp.ExecuteNonQuery();
                        }
                    }
                }

                string query = "UPDATE BOOKINGS SET Status = @status WHERE BookingID = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", bookingId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- PAYMENTS ---
        public List<Payment> GetPaymentsByLandlord(int landlordId)
        {
            var list = new List<Payment>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT pay.PaymentID, pay.BookingID, pay.Amount, pay.TransactionID, pay.Method, pay.Status, pay.PaymentDate,
                           u.FullName as TenantName, p.Title as PropertyTitle
                    FROM PAYMENTS pay
                    JOIN BOOKINGS b ON pay.BookingID = b.BookingID
                    JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                    JOIN USERS u ON b.TenantID = u.UserID
                    WHERE p.LandlordID = @lid
                    ORDER BY pay.PaymentDate DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lid", landlordId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Payment
                            {
                                PaymentID = (int)reader["PaymentID"],
                                BookingID = (int)reader["BookingID"],
                                Amount = (decimal)reader["Amount"],
                                PaymentDate = reader["PaymentDate"] as DateTime?,
                                Status = reader["Status"].ToString() ?? "Verified",
                                TransactionID = reader["TransactionID"].ToString() ?? "",
                                PaymentMethod = reader["Method"].ToString() ?? "",
                                TenantName = reader["TenantName"].ToString() ?? "",
                                PropertyTitle = reader["PropertyTitle"].ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return list;
        }

        public void UpdatePaymentStatus(int paymentId, string status)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE PAYMENTS SET Status = @status WHERE PaymentID = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", paymentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- REVIEWS ---
        public List<Review> GetReviewsByLandlord(int landlordId)
        {
            var list = new List<Review>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT r.ReviewID, r.PropertyID, r.TenantID, r.Rating, r.Comment, r.Reply, r.CreatedAt, r.IsResolved, 
                           u.FullName as TenantName
                    FROM REVIEWS r
                    JOIN PROPERTIES p ON r.PropertyID = p.PropertyID
                    JOIN USERS u ON r.TenantID = u.UserID
                    WHERE p.LandlordID = @lid
                    ORDER BY r.CreatedAt DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lid", landlordId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Review
                            {
                                ReviewID = (int)reader["ReviewID"],
                                PropertyID = (int)reader["PropertyID"],
                                TenantID = (int)reader["TenantID"],
                                Rating = (int)reader["Rating"],
                                Comment = reader["Comment"]?.ToString() ?? "",
                                Reply = reader["Reply"]?.ToString() ?? "",
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                IsResolved = reader["IsResolved"] != DBNull.Value && (bool)reader["IsResolved"],
                                TenantName = reader["TenantName"]?.ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return list;
        }


        public void ReplyToReview(int reviewId, string reply)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE REVIEWS SET Reply = @reply, IsResolved = 1 WHERE ReviewID = @rid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@reply", reply);
                    cmd.Parameters.AddWithValue("@rid", reviewId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- DASHBOARD STATS ---
        public (int TotalProps, int PendingReqs, decimal MonthlyEarnings, int Unpaid) GetDashboardStats(int landlordId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                
                // 1. Total Props
                string q1 = "SELECT COUNT(*) FROM PROPERTIES WHERE LandlordID = @lid";
                int props = (int)new SqlCommand(q1, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                // 2. Pending Reqs
                string q2 = @"SELECT COUNT(*) FROM BOOKINGS b JOIN PROPERTIES p ON b.PropertyID = p.PropertyID 
                              WHERE p.LandlordID = @lid AND b.Status = 'Pending'";
                int reqs = (int)new SqlCommand(q2, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                // 3. Monthly Earnings (This month)
                string q3 = @"SELECT ISNULL(SUM(pay.Amount),0)
                              FROM PAYMENTS pay
                              JOIN BOOKINGS b ON pay.BookingID = b.BookingID
                              JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                              WHERE p.LandlordID = @lid AND pay.Status = 'Verified' AND MONTH(pay.PaymentDate) = MONTH(GETDATE())";
                decimal earnings = (decimal)new SqlCommand(q3, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                // 4. Unpaid
                // Unpaid = Approved bookings that have no payment recorded
                string q4 = @"SELECT COUNT(*)
                              FROM BOOKINGS b
                              JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                              WHERE p.LandlordID=@lid AND b.Status='Approved'
                                AND NOT EXISTS (SELECT 1 FROM PAYMENTS pay WHERE pay.BookingID = b.BookingID AND pay.Status='Verified')";
                int unpaid = (int)new SqlCommand(q4, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                return (props, reqs, earnings, unpaid);
            }
        }

        /// <summary>
        /// Gets comprehensive dashboard statistics for a landlord including:
        /// - Total properties count
        /// - Total all-time earnings
        /// - Approved tenants count
        /// - Pending requests count
        /// - Occupancy percentage
        /// </summary>
        public (int TotalProps, decimal TotalEarnings, int ApprovedTenants, int PendingReqs, int OccupancyPercent) GetComprehensiveStats(int landlordId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                
                // 1. Total Properties
                string q1 = "SELECT COUNT(*) FROM PROPERTIES WHERE LandlordID = @lid";
                int totalProps = (int)new SqlCommand(q1, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                // 2. Total All-Time Earnings (verified payments)
                string q2 = @"SELECT ISNULL(SUM(pay.Amount), 0)
                              FROM PAYMENTS pay
                              JOIN BOOKINGS b ON pay.BookingID = b.BookingID
                              JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                              WHERE p.LandlordID = @lid AND pay.Status = 'Verified'";
                decimal totalEarnings = (decimal)new SqlCommand(q2, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                // 3. Approved Tenants (active/approved bookings)
                string q3 = @"SELECT COUNT(*)
                              FROM BOOKINGS b
                              JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                              WHERE p.LandlordID = @lid AND b.Status = 'Approved'";
                int approvedTenants = (int)new SqlCommand(q3, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                // 4. Pending Requests
                string q4 = @"SELECT COUNT(*)
                              FROM BOOKINGS b
                              JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                              WHERE p.LandlordID = @lid AND b.Status = 'Pending'";
                int pendingReqs = (int)new SqlCommand(q4, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                // 5. Occupancy Percentage = (Approved bookings / Total properties) * 100
                int occupancy = totalProps > 0 ? (int)Math.Round((approvedTenants * 100.0) / totalProps) : 0;

                return (totalProps, totalEarnings, approvedTenants, pendingReqs, occupancy);
            }
        }
    }
}
