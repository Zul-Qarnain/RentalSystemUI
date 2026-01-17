using System;
using Microsoft.Data.SqlClient;

namespace RentalSystemUI.Data
{
    public class DatabaseInitializer : Database
    {
        private void EnsureDatabaseExists()
        {
            string connString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") 
                                ?? throw new InvalidOperationException("DB_CONNECTION_STRING is missing in .env layer");

            var builder = new SqlConnectionStringBuilder(connString);
            var dbName = builder.InitialCatalog;
            builder.InitialCatalog = "master";

            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand($"IF DB_ID('{dbName}') IS NULL CREATE DATABASE [{dbName}];", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EnsureTablesExist()
        {
            EnsureDatabaseExists();

            using (var conn = GetConnection())
            {
                conn.Open();

                string script = @"
                    -- USERS
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='USERS' AND xtype='U')
                    CREATE TABLE USERS (
                        UserID INT IDENTITY(1,1) PRIMARY KEY,
                        FullName NVARCHAR(255) NOT NULL,
                        Email NVARCHAR(255) UNIQUE NOT NULL,
                        PasswordHash NVARCHAR(MAX) NOT NULL,
                        Phone NVARCHAR(20),
                        UserType NVARCHAR(20) CHECK (UserType IN ('Tenant', 'Landlord', 'SuperAdmin')),
                        IsActive BIT DEFAULT 1,
                        CreatedAt DATETIME2 DEFAULT GETDATE()
                    );

                    -- PROPERTIES
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PROPERTIES' AND xtype='U')
                    CREATE TABLE PROPERTIES (
                        PropertyID INT IDENTITY(1,1) PRIMARY KEY,
                        LandlordID INT NOT NULL,
                        Title NVARCHAR(255) NOT NULL,
                        Description NVARCHAR(MAX),
                        Address NVARCHAR(MAX) NOT NULL,
                        City NVARCHAR(100) NOT NULL,
                        RentAmount DECIMAL(18, 2) NOT NULL,
                        Status NVARCHAR(20) CHECK (Status IN ('Available', 'Rented', 'Maintenance')),
                        AvailabilityStatus BIT DEFAULT 1,
                        Rooms INT,
                        Kitchen INT,
                        WashRoom INT,
                        IsPetAllowed BIT,
                        IsAC BIT,
                        CreatedAt DATETIME2 DEFAULT GETDATE(),
                        CONSTRAINT FK_PROPERTIES_USERS FOREIGN KEY (LandlordID) REFERENCES USERS(UserID)
                    );

                    -- PROPERTY_IMAGES
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PROPERTY_IMAGES' AND xtype='U')
                    CREATE TABLE PROPERTY_IMAGES (
                        ImageID INT IDENTITY(1,1) PRIMARY KEY,
                        PropertyID INT NOT NULL,
                        ImagePath NVARCHAR(MAX) NOT NULL,
                        UploadedAt DATETIME2 DEFAULT GETDATE(),
                        CONSTRAINT FK_PROPERTY_IMAGES_PROPERTIES FOREIGN KEY (PropertyID) REFERENCES PROPERTIES(PropertyID) ON DELETE CASCADE
                    );

                    -- BOOKINGS
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BOOKINGS' AND xtype='U')
                    CREATE TABLE BOOKINGS (
                        BookingID INT IDENTITY(1,1) PRIMARY KEY,
                        PropertyID INT NOT NULL,
                        TenantID INT NOT NULL,
                        StartDate DATE NOT NULL,
                        EndDate DATE NOT NULL,
                        DurationMonths INT,
                        TotalAmount DECIMAL(18, 2) NOT NULL,
                        Status NVARCHAR(20) CHECK (Status IN ('Pending', 'Approved', 'Rejected')),
                        CreatedAt DATETIME2 DEFAULT GETDATE(),
                        CONSTRAINT FK_BOOKINGS_PROPERTIES FOREIGN KEY (PropertyID) REFERENCES PROPERTIES(PropertyID),
                        CONSTRAINT FK_BOOKINGS_USERS FOREIGN KEY (TenantID) REFERENCES USERS(UserID)
                    );

                    -- PAYMENTS
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PAYMENTS' AND xtype='U')
                    CREATE TABLE PAYMENTS (
                        PaymentID INT IDENTITY(1,1) PRIMARY KEY,
                        BookingID INT NOT NULL,
                        Amount DECIMAL(18, 2) NOT NULL,
                        TransactionID NVARCHAR(100) UNIQUE,
                        Method NVARCHAR(20) CHECK (Method IN ('Cash', 'Bkash', 'Card')),
                        Status NVARCHAR(20) CHECK (Status IN ('Verified', 'Failed')),
                        PaymentDate DATETIME2 DEFAULT GETDATE(),
                        CONSTRAINT FK_PAYMENTS_BOOKINGS FOREIGN KEY (BookingID) REFERENCES BOOKINGS(BookingID)
                    );

                    -- REVIEWS
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='REVIEWS' AND xtype='U')
                    CREATE TABLE REVIEWS (
                        ReviewID INT IDENTITY(1,1) PRIMARY KEY,
                        PropertyID INT NOT NULL,
                        TenantID INT NOT NULL,
                        Rating INT CHECK (Rating BETWEEN 1 AND 5),
                        Comment NVARCHAR(MAX),
                        CreatedAt DATETIME2 DEFAULT GETDATE(),
                        CONSTRAINT FK_REVIEWS_PROPERTIES FOREIGN KEY (PropertyID) REFERENCES PROPERTIES(PropertyID) ON DELETE CASCADE,
                        CONSTRAINT FK_REVIEWS_USERS FOREIGN KEY (TenantID) REFERENCES USERS(UserID)
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
