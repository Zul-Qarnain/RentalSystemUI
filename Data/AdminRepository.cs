using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Controllers;
using RentalSystemUI.Models;

namespace RentalSystemUI.Data
{
    public class AdminRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public AdminRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        // 1. Get Dashboard Stats
        public Dictionary<string, object> GetDashboardStats()
        {
            var stats = new Dictionary<string, object>
            {
                { "TotalUsers", 0 },
                { "TotalLandlords", 0 },
                { "TotalTenants", 0 },
                { "TotalTransactions", 0 },
                { "TotalRevenue", 0m }
            };

            string query = @"
                SELECT 
                    (SELECT COUNT(*) FROM USERS) AS TotalUsers,
                    (SELECT COUNT(*) FROM USERS WHERE UserType = 'Landlord') AS TotalLandlords,
                    (SELECT COUNT(*) FROM USERS WHERE UserType = 'Tenant') AS TotalTenants,
                    (SELECT COUNT(*) FROM PAYMENTS) AS TotalTransactions,
                    (SELECT ISNULL(SUM(Amount), 0) FROM PAYMENTS WHERE Status = 'Verified') AS TotalRevenue
            ";

            DataTable dt = _dbHelper.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                stats["TotalUsers"] = Convert.ToInt32(row["TotalUsers"]);
                stats["TotalLandlords"] = Convert.ToInt32(row["TotalLandlords"]);
                stats["TotalTenants"] = Convert.ToInt32(row["TotalTenants"]);
                stats["TotalTransactions"] = Convert.ToInt32(row["TotalTransactions"]);
                stats["TotalRevenue"] = Convert.ToDecimal(row["TotalRevenue"]);
            }

            return stats;
        }

        // 2. Get All Users
        public DataTable GetAllUsers()
        {
            string query = "SELECT UserID, FullName, Email, Phone, UserType, IsActive, CreatedAt FROM USERS ORDER BY CreatedAt DESC";
            return _dbHelper.ExecuteQuery(query);
        }

        // 3. Delete User (Cascade)
        public bool DeleteUser(int userId)
        {
            // Simple approach: Delete from USERS. 
            // Since we have ON DELETE CASCADE or foreign keys, we might need manual cleanup if constraints aren't set to Cascade.
            // Based on create_table.sql:
            // PROPERTIES -> LandlordID (No Cascade mentioned explicitly in FK, need check)
            // BOOKINGS -> TenantID (No Cascade)
            // REVIEWS -> TenantID (No Cascade mentioned)
            // MESSAGES -> Sender/Receiver (No Cascade)
            
            // To be safe, we should delete child records first manually or ensure DB has Cascade.
            // Given I cannot easily alter DB schema without risk, I will manual delete.

            string query = @"
                BEGIN TRANSACTION;
                BEGIN TRY
                    -- 1. Delete Messages sent/received
                    DELETE FROM MESSAGES WHERE SenderID = @UserID OR ReceiverID = @UserID;
                    
                    -- 2. Delete Reviews made by user
                    DELETE FROM REVIEWS WHERE TenantID = @UserID;
                    
                    -- 3. Delete Payments for Bookings made by user (if Tenant)
                    DELETE FROM PAYMENTS WHERE BookingID IN (SELECT BookingID FROM BOOKINGS WHERE TenantID = @UserID);
                    
                    -- 4. Delete Bookings made by user (if Tenant)
                    DELETE FROM BOOKINGS WHERE TenantID = @UserID;
                    
                    -- 5. If User is Landlord, get their PropertyIDs
                    DECLARE @PropIds TABLE (ID INT);
                    INSERT INTO @PropIds SELECT PropertyID FROM PROPERTIES WHERE LandlordID = @UserID;

                    -- 6. Delete Reviews on those properties
                    DELETE FROM REVIEWS WHERE PropertyID IN (SELECT ID FROM @PropIds); 

                    -- 7. Delete Messages for those properties
                    DELETE FROM MESSAGES WHERE PropertyID IN (SELECT ID FROM @PropIds);

                    -- 8. Delete Bookings for those properties
                    -- (And payments for those bookings)
                    DELETE FROM PAYMENTS WHERE BookingID IN (SELECT BookingID FROM BOOKINGS WHERE PropertyID IN (SELECT ID FROM @PropIds));
                    DELETE FROM BOOKINGS WHERE PropertyID IN (SELECT ID FROM @PropIds);

                    -- 9. Delete Images
                    DELETE FROM PROPERTY_IMAGES WHERE PropertyID IN (SELECT ID FROM @PropIds);

                    -- 10. Delete Properties
                    DELETE FROM PROPERTIES WHERE LandlordID = @UserID;

                    -- 11. Finally Delete User
                    DELETE FROM USERS WHERE UserID = @UserID;

                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION;
                    THROW;
                END CATCH
            ";

            SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };
            
            try 
            {
                _dbHelper.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // 4. Get Transactions
        public DataTable GetAllTransactions()
        {
            string query = @"
                SELECT 
                    p.PaymentID,
                    p.BookingID,
                    p.Amount,
                    p.Method,
                    p.Status,
                    ISNULL(p.PaymentDate, GETDATE()) AS PaymentDate,
                    ISNULL(u.FullName, 'Unknown Payer') AS PayerName,
                    ISNULL(u.Email, '') AS PayerEmail,
                    ISNULL(u.Phone, '') AS PayerPhone
                FROM PAYMENTS p
                LEFT JOIN BOOKINGS b ON p.BookingID = b.BookingID
                LEFT JOIN USERS u ON b.TenantID = u.UserID
                ORDER BY ISNULL(p.PaymentDate, GETDATE()) DESC
            ";
            return _dbHelper.ExecuteQuery(query);
        }
    }
}
