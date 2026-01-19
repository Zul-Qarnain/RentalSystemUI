using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Models;

namespace RentalSystemUI.Data
{
    public class TenantRepository : Database
    {
        private int? GetLandlordIdByPropertyId(SqlConnection conn, int propertyId)
        {
            using (var cmd = new SqlCommand("SELECT LandlordID FROM PROPERTIES WHERE PropertyID=@pid", conn))
            {
                cmd.Parameters.AddWithValue("@pid", propertyId);
                var obj = cmd.ExecuteScalar();
                if (obj == null || obj == DBNull.Value) return null;
                return (int)obj;
            }
        }

        private string GetPropertyTitle(SqlConnection conn, int propertyId)
        {
            using (var cmd = new SqlCommand("SELECT Title FROM PROPERTIES WHERE PropertyID=@pid", conn))
            {
                cmd.Parameters.AddWithValue("@pid", propertyId);
                return cmd.ExecuteScalar()?.ToString() ?? $"Property #{propertyId}";
            }
        }

        public List<BookingWithProperty> GetBookingsByTenant(int tenantId)
        {
            var list = new List<BookingWithProperty>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT b.*, p.Title as PropertyTitle, p.Address, p.City, p.RentAmount
                    FROM BOOKINGS b
                    JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                    WHERE b.TenantID=@tid
                    ORDER BY b.CreatedAt DESC";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tenantId);
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

        public int CreateBooking(int tenantId, int propertyId, DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate) return -1;

            using (var conn = GetConnection())
            {
                conn.Open();

                // Check if property is available for booking
                using (var cmdCheck = new SqlCommand("SELECT Status, AvailabilityStatus FROM PROPERTIES WHERE PropertyID=@pid", conn))
                {
                    cmdCheck.Parameters.AddWithValue("@pid", propertyId);
                    using (var reader = cmdCheck.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var status = reader["Status"]?.ToString();
                            var available = reader["AvailabilityStatus"] != DBNull.Value && (bool)reader["AvailabilityStatus"];
                            
                            // Block if property is already rented or marked unavailable
                            if (status == "Rented" || !available)
                            {
                                return -2; // Property not available
                            }
                        }
                        else
                        {
                            return -1; // Property not found
                        }
                    }
                }

                // Read rent amount
                decimal rent;
                using (var cmdRent = new SqlCommand("SELECT RentAmount FROM PROPERTIES WHERE PropertyID=@pid", conn))
                {
                    cmdRent.Parameters.AddWithValue("@pid", propertyId);
                    var obj = cmdRent.ExecuteScalar();
                    if (obj == null) return -1;
                    rent = (decimal)obj;
                }

                int months = Math.Max(1, (int)Math.Ceiling(((endDate.Date - startDate.Date).TotalDays) / 30.0));
                decimal total = rent * months;

                string sql = @"
                    INSERT INTO BOOKINGS (PropertyID, TenantID, StartDate, EndDate, DurationMonths, TotalAmount, Status)
                    OUTPUT INSERTED.BookingID
                    VALUES (@pid, @tid, @start, @end, @months, @total, 'Pending')";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", propertyId);
                    cmd.Parameters.AddWithValue("@tid", tenantId);
                    cmd.Parameters.AddWithValue("@start", startDate.Date);
                    cmd.Parameters.AddWithValue("@end", endDate.Date);
                    cmd.Parameters.AddWithValue("@months", months);
                    cmd.Parameters.AddWithValue("@total", total);
                    var bookingId = (int)cmd.ExecuteScalar();

                    // Notify landlord: new booking request
                    try
                    {
                        var landlordId = GetLandlordIdByPropertyId(conn, propertyId);
                        if (landlordId.HasValue)
                        {
                            var title = GetPropertyTitle(conn, propertyId);
                            using (var ncmd = new SqlCommand(@"
                                INSERT INTO NOTIFICATIONS (UserID, Title, Message, IsRead, CreatedAt)
                                VALUES (@uid, @title, @msg, 0, GETDATE())", conn))
                            {
                                ncmd.Parameters.AddWithValue("@uid", landlordId.Value);
                                ncmd.Parameters.AddWithValue("@title", "New Booking Request");
                                ncmd.Parameters.AddWithValue("@msg", $"A new booking request was submitted for '{title}'.");
                                ncmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch { }

                    return bookingId;
                }
            }
        }

        public int CreatePaymentForBooking(int bookingId, decimal amount, string paymentMethod, string transactionId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"
                    INSERT INTO PAYMENTS (BookingID, Amount, TransactionID, Method, Status, PaymentDate)
                    OUTPUT INSERTED.PaymentID
                    VALUES (@bid, @amt, @tx, @method, 'Verified', GETDATE())", conn))
                {
                    cmd.Parameters.AddWithValue("@bid", bookingId);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@tx", transactionId ?? string.Empty);
                    cmd.Parameters.AddWithValue("@method", string.IsNullOrWhiteSpace(paymentMethod) ? "Card" : paymentMethod);
                    var paymentId = (int)cmd.ExecuteScalar();

                    // Notify landlord: payment made
                    try
                    {
                        int propertyId = 0;
                        using (var bcmd = new SqlCommand("SELECT PropertyID FROM BOOKINGS WHERE BookingID=@bid", conn))
                        {
                            bcmd.Parameters.AddWithValue("@bid", bookingId);
                            var obj = bcmd.ExecuteScalar();
                            if (obj != null) propertyId = (int)obj;
                        }

                        if (propertyId > 0)
                        {
                            var landlordId = GetLandlordIdByPropertyId(conn, propertyId);
                            if (landlordId.HasValue)
                            {
                                var title = GetPropertyTitle(conn, propertyId);
                                using (var ncmd = new SqlCommand(@"
                                    INSERT INTO NOTIFICATIONS (UserID, Title, Message, IsRead, CreatedAt)
                                    VALUES (@uid, @title, @msg, 0, GETDATE())", conn))
                                {
                                    ncmd.Parameters.AddWithValue("@uid", landlordId.Value);
                                    ncmd.Parameters.AddWithValue("@title", "Payment Received");
                                    ncmd.Parameters.AddWithValue("@msg", $"A tenant submitted a payment for '{title}' (৳{amount:N0}).");
                                    ncmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    catch { }

                    return paymentId;
                }
            }
        }

        public List<BookingWithProperty> GetApprovedUnpaidBookings(int tenantId)
        {
            var list = new List<BookingWithProperty>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT b.*, p.Title as PropertyTitle, p.Address, p.City, p.RentAmount
                    FROM BOOKINGS b
                    JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                    WHERE b.TenantID=@tid AND b.Status='Approved'
                      AND NOT EXISTS (SELECT 1 FROM PAYMENTS pay WHERE pay.BookingID=b.BookingID AND pay.Status='Verified')
                    ORDER BY b.CreatedAt DESC";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tenantId);
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
                                Status = reader["Status"].ToString() ?? "Approved",
                                CreatedAt = (DateTime)reader["CreatedAt"],
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

        public int? GetLatestApprovedBookingId(int tenantId, int propertyId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"
                    SELECT TOP 1 BookingID
                    FROM BOOKINGS
                    WHERE TenantID=@tid AND PropertyID=@pid AND Status='Approved'
                    ORDER BY CreatedAt DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tenantId);
                    cmd.Parameters.AddWithValue("@pid", propertyId);
                    var obj = cmd.ExecuteScalar();
                    return obj == null ? null : (int)obj;
                }
            }
        }
        public List<TenantRental> GetRentalsByTenant(int tenantId)
        {
            var list = new List<TenantRental>();
            using (var conn = GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT b.BookingID, b.PropertyID, b.TenantID, b.CreatedAt, b.Status,
                           b.StartDate, b.EndDate, b.DurationMonths, b.TotalAmount,
                           p.Title, p.Address, p.City, p.RentAmount
                    FROM BOOKINGS b
                    JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                    WHERE b.TenantID = @tid AND b.Status = 'Approved'
                    ORDER BY b.CreatedAt DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tenantId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new TenantRental
                            {
                                BookingId = (int)reader["BookingID"],
                                PropertyId = (int)reader["PropertyID"],
                                TenantId = (int)reader["TenantID"],
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                Status = reader["Status"].ToString() ?? "Pending",
                                PropertyTitle = reader["Title"].ToString() ?? string.Empty,
                                PropertyAddress = $"{reader["Address"]}, {reader["City"]}",
                                MonthlyRent = (decimal)reader["RentAmount"],
                                StartDate = (DateTime)reader["StartDate"],
                                EndDate = (DateTime)reader["EndDate"],
                                DurationMonths = reader["DurationMonths"] as int?,
                                TotalAmount = (decimal)reader["TotalAmount"]
                            });
                        }
                    }
                }
            }
            return list;
        }

        public int CreatePayment(int tenantId, int propertyId, decimal amount, DateTime dueDate, string paymentMethod, string transactionId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Booking schema: pay against an existing booking
                // Find most recent Approved booking for this tenant+property
                int bookingId;
                using (var cmdFind = new SqlCommand(@"
                    SELECT TOP 1 BookingID
                    FROM BOOKINGS
                    WHERE TenantID=@tid AND PropertyID=@pid AND Status='Approved'
                    ORDER BY CreatedAt DESC", conn))
                {
                    cmdFind.Parameters.AddWithValue("@tid", tenantId);
                    cmdFind.Parameters.AddWithValue("@pid", propertyId);
                    var obj = cmdFind.ExecuteScalar();
                    bookingId = obj == null ? 0 : (int)obj;
                }

                if (bookingId == 0) return -1;

                // Booking model PAYMENTS has only Verified/Failed; mark as Verified on successful submission
                using (var cmd = new SqlCommand(@"
                    INSERT INTO PAYMENTS (BookingID, Amount, TransactionID, Method, Status, PaymentDate)
                    OUTPUT INSERTED.PaymentID
                    VALUES (@bid, @amt, @tx, @method, 'Verified', GETDATE())", conn))
                {
                    cmd.Parameters.AddWithValue("@bid", bookingId);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@tx", transactionId ?? string.Empty);
                    cmd.Parameters.AddWithValue("@method", string.IsNullOrWhiteSpace(paymentMethod) ? "Card" : paymentMethod);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public bool CancelRental(int bookingId, int tenantId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                // Booking model has only Pending/Approved/Rejected. We'll mark cancelled bookings as Rejected.
                string sql = @"UPDATE BOOKINGS SET Status='Rejected'
                               WHERE BookingID=@id AND TenantID=@tid AND Status='Approved'";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", bookingId);
                    cmd.Parameters.AddWithValue("@tid", tenantId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Payment> GetPaymentsByTenant(int tenantId)
        {
            var list = new List<Payment>();
            using (var conn = GetConnection())
            {
                conn.Open();
                // Booking-model schema: PAYMENTS has BookingID and BOOKINGS contains TenantID/PropertyID
                string query = @"
                    SELECT pay.PaymentID, pay.BookingID, b.TenantID, b.PropertyID, pay.Amount, pay.PaymentDate, pay.Status,
                           pay.TransactionID, pay.Method, p.Title as PropertyTitle
                    FROM PAYMENTS pay
                    JOIN BOOKINGS b ON pay.BookingID = b.BookingID
                    JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                    WHERE b.TenantID = @tid
                    ORDER BY pay.PaymentDate DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tenantId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Payment
                            {
                                PaymentID = (int)reader["PaymentID"],
                                BookingID = (int)reader["BookingID"],
                                TenantID = (int)reader["TenantID"],
                                PropertyID = (int)reader["PropertyID"],
                                Amount = (decimal)reader["Amount"],
                                PaymentDate = reader["PaymentDate"] as DateTime?,
                                Status = reader["Status"].ToString() ?? "Verified",
                                TransactionID = reader["TransactionID"].ToString() ?? "",
                                PaymentMethod = reader["Method"].ToString() ?? "",
                                TenantName = "",
                                PropertyTitle = reader["PropertyTitle"].ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return list;
        }

        public List<Review> GetReviewsByTenant(int tenantId)
        {
            var list = new List<Review>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT r.ReviewID, r.PropertyID, r.TenantID, r.Rating, r.Comment, r.CreatedAt, r.Reply, r.IsResolved,
                           p.Title AS PropertyTitle, u.FullName AS TenantName
                    FROM REVIEWS r
                    JOIN PROPERTIES p ON r.PropertyID = p.PropertyID
                    JOIN USERS u ON r.TenantID = u.UserID
                    WHERE r.TenantID = @tid
                    ORDER BY r.CreatedAt DESC";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tenantId);
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
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                PropertyTitle = reader["PropertyTitle"]?.ToString() ?? "",
                                TenantName = reader["TenantName"]?.ToString() ?? "",
                                Reply = reader["Reply"]?.ToString() ?? "",
                                IsResolved = reader["IsResolved"] != DBNull.Value && (bool)reader["IsResolved"]
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool CreateReview(int propertyId, int tenantId, int rating, string comment)
        {
            // Check if tenant has booked this property
            using (var conn = GetConnection())
            {
                conn.Open();
                
                // Check for existing review
                using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM REVIEWS WHERE PropertyID=@pid AND TenantID=@tid", conn))
                {
                    checkCmd.Parameters.AddWithValue("@pid", propertyId);
                    checkCmd.Parameters.AddWithValue("@tid", tenantId);
                    if ((int)checkCmd.ExecuteScalar() > 0) return false; // Already reviewed
                }

                // Check for approved booking
                using (var bookingCheck = new SqlCommand("SELECT COUNT(*) FROM BOOKINGS WHERE PropertyID=@pid AND TenantID=@tid AND Status='Approved'", conn))
                {
                    bookingCheck.Parameters.AddWithValue("@pid", propertyId);
                    bookingCheck.Parameters.AddWithValue("@tid", tenantId);
                    if ((int)bookingCheck.ExecuteScalar() == 0) return false; // No approved booking
                }

                // Create review
                using (var cmd = new SqlCommand(@"
                    INSERT INTO REVIEWS (PropertyID, TenantID, Rating, Comment)
                    VALUES (@pid, @tid, @rating, @comment)", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", propertyId);
                    cmd.Parameters.AddWithValue("@tid", tenantId);
                    cmd.Parameters.AddWithValue("@rating", rating);
                    cmd.Parameters.AddWithValue("@comment", string.IsNullOrWhiteSpace(comment) ? DBNull.Value : comment);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateReview(int reviewId, int rating, string comment)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE REVIEWS SET Rating = @rating, Comment = @comment WHERE ReviewID = @rid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@rating", rating);
                    cmd.Parameters.AddWithValue("@comment", string.IsNullOrWhiteSpace(comment) ? DBNull.Value : comment);
                    cmd.Parameters.AddWithValue("@rid", reviewId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }

    public class TenantRental
    {
        public int BookingId { get; set; }
        public int PropertyId { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "Pending";
        public string PropertyTitle { get; set; } = string.Empty;
        public string PropertyAddress { get; set; } = string.Empty;
        public decimal MonthlyRent { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? DurationMonths { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
