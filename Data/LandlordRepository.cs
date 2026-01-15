using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Models;

namespace RentalSystemUI.Data
{
    public class LandlordRepository : Database
    {
        // --- REQUESTS ---
        public List<RentalSystemUI.Models.Application> GetApplicationsByLandlord(int landlordId)
        {
            var list = new List<RentalSystemUI.Models.Application>();
            using (var conn = GetConnection())
            {
                conn.Open();
                // Join with Properties to check LandlordID
                string query = @"
                    SELECT a.*, u.FullName as TenantName, p.Title as PropertyTitle
                    FROM APPLICATIONS a
                    JOIN PROPERTIES p ON a.PropertyID = p.PropertyID
                    JOIN USERS u ON a.TenantID = u.UserID
                    WHERE p.LandlordID = @lid
                    ORDER BY a.ApplicationDate DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lid", landlordId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new RentalSystemUI.Models.Application
                            {
                                ApplicationID = (int)reader["ApplicationID"],
                                PropertyID = (int)reader["PropertyID"],
                                TenantID = (int)reader["TenantID"],
                                ApplicationDate = (DateTime)reader["ApplicationDate"],
                                Status = reader["Status"].ToString() ?? "Pending",
                                Message = reader["Message"].ToString() ?? "",
                                TenantName = reader["TenantName"].ToString() ?? "",
                                PropertyTitle = reader["PropertyTitle"].ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return list;
        }

        public void UpdateApplicationStatus(int appId, string status)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                
                if (status == "Accepted") 
                {
                    // 1. Get PropertyID for this application
                    int propId = 0;
                    using (var cmdGet = new SqlCommand("SELECT PropertyID FROM APPLICATIONS WHERE ApplicationID = @id", conn))
                    {
                        cmdGet.Parameters.AddWithValue("@id", appId);
                        object res = cmdGet.ExecuteScalar();
                        if (res != null) propId = (int)res;
                    }

                    // 2. Reject all OTHER pending applications for this property
                    if (propId > 0)
                    {
                        string rejectQuery = "UPDATE APPLICATIONS SET Status = 'Rejected' WHERE PropertyID = @pid AND ApplicationID != @id AND Status = 'Pending'";
                        using (var cmdReject = new SqlCommand(rejectQuery, conn))
                        {
                            cmdReject.Parameters.AddWithValue("@pid", propId);
                            cmdReject.Parameters.AddWithValue("@id", appId);
                            cmdReject.ExecuteNonQuery();
                        }
                    }
                }

                // 3. Update the target application
                string query = "UPDATE APPLICATIONS SET Status = @status WHERE ApplicationID = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", appId);
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
                    SELECT pay.*, u.FullName as TenantName, p.Title as PropertyTitle
                    FROM PAYMENTS pay
                    JOIN PROPERTIES p ON pay.PropertyID = p.PropertyID
                    JOIN USERS u ON pay.TenantID = u.UserID
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
                                TenantID = (int)reader["TenantID"],
                                PropertyID = (int)reader["PropertyID"],
                                Amount = (decimal)reader["Amount"],
                                PaymentDate = reader["PaymentDate"] as DateTime?,
                                DueDate = (DateTime)reader["DueDate"],
                                Status = reader["Status"].ToString() ?? "Pending",
                                TransactionID = reader["TransactionID"].ToString() ?? "",
                                PaymentMethod = reader["PaymentMethod"].ToString() ?? "",
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
                    SELECT r.*, u.FullName as TenantName
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
                                Comment = reader["Comment"].ToString() ?? "",
                                Reply = reader["Reply"].ToString() ?? "",
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                IsResolved = (bool)reader["IsResolved"],
                                TenantName = reader["TenantName"].ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return list;
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
                string q2 = @"SELECT COUNT(*) FROM APPLICATIONS a JOIN PROPERTIES p ON a.PropertyID = p.PropertyID 
                              WHERE p.LandlordID = @lid AND a.Status = 'Pending'";
                int reqs = (int)new SqlCommand(q2, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                // 3. Monthly Earnings (This month)
                string q3 = @"SELECT ISNULL(SUM(Amount),0) FROM PAYMENTS pay JOIN PROPERTIES p ON pay.PropertyID = p.PropertyID
                              WHERE p.LandlordID = @lid AND pay.Status = 'Verified' AND MONTH(PaymentDate) = MONTH(GETDATE())";
                decimal earnings = (decimal)new SqlCommand(q3, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                // 4. Unpaid
                string q4 = @"SELECT COUNT(*) FROM PAYMENTS pay JOIN PROPERTIES p ON pay.PropertyID = p.PropertyID
                              WHERE p.LandlordID = @lid AND pay.Status IN ('Pending', 'Overdue')";
                int unpaid = (int)new SqlCommand(q4, conn) { Parameters = { new SqlParameter("@lid", landlordId) } }.ExecuteScalar();

                return (props, reqs, earnings, unpaid);
            }
        }
    }
}
