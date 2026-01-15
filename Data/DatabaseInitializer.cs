using System;
using Microsoft.Data.SqlClient;

namespace RentalSystemUI.Data
{
    public class DatabaseInitializer : Database
    {
        public void EnsureTablesExist()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                string script = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='APPLICATIONS' AND xtype='U')
                    CREATE TABLE APPLICATIONS (
                        ApplicationID INT PRIMARY KEY IDENTITY,
                        PropertyID INT FOREIGN KEY REFERENCES PROPERTIES(PropertyID),
                        TenantID INT FOREIGN KEY REFERENCES USERS(UserID),
                        ApplicationDate DATETIME DEFAULT GETDATE(),
                        Status NVARCHAR(50) DEFAULT 'Pending',
                        Message NVARCHAR(MAX)
                    );

                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PAYMENTS' AND xtype='U')
                    CREATE TABLE PAYMENTS (
                        PaymentID INT PRIMARY KEY IDENTITY,
                        TenantID INT FOREIGN KEY REFERENCES USERS(UserID),
                        PropertyID INT FOREIGN KEY REFERENCES PROPERTIES(PropertyID),
                        Amount DECIMAL(18,2),
                        PaymentDate DATETIME,
                        DueDate DATETIME,
                        Status NVARCHAR(50) DEFAULT 'Pending',
                        TransactionID NVARCHAR(100),
                        PaymentMethod NVARCHAR(50)
                    );
                    
                    /* FIX for existing table missing PropertyID */
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PAYMENTS' AND COLUMN_NAME = 'PropertyID')
                    BEGIN
                        ALTER TABLE PAYMENTS ADD PropertyID INT FOREIGN KEY REFERENCES PROPERTIES(PropertyID);
                    END

                    /* FIX for existing table missing TenantID */
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PAYMENTS' AND COLUMN_NAME = 'TenantID')
                    BEGIN
                        ALTER TABLE PAYMENTS ADD TenantID INT FOREIGN KEY REFERENCES USERS(UserID);
                    END

                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='REVIEWS' AND xtype='U')
                    CREATE TABLE REVIEWS (
                        ReviewID INT PRIMARY KEY IDENTITY,
                        PropertyID INT FOREIGN KEY REFERENCES PROPERTIES(PropertyID),
                        TenantID INT FOREIGN KEY REFERENCES USERS(UserID),
                        Rating INT,
                        Comment NVARCHAR(MAX),
                        Reply NVARCHAR(MAX),
                        CreatedAt DATETIME DEFAULT GETDATE(),
                        IsResolved BIT DEFAULT 0
                    );
                ";

                using (var cmd = new SqlCommand(script, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
