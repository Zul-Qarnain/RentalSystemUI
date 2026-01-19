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
                        Status NVARCHAR(20) CHECK (Status IN ('Pending', 'Approved', 'Rejected', 'Terminated')),
                        CreatedAt DATETIME2 DEFAULT GETDATE(),
                        CONSTRAINT FK_BOOKINGS_PROPERTIES FOREIGN KEY (PropertyID) REFERENCES PROPERTIES(PropertyID),
                        CONSTRAINT FK_BOOKINGS_USERS FOREIGN KEY (TenantID) REFERENCES USERS(UserID)
                    );

                    -- Ensure BOOKINGS status constraint includes 'Terminated' (for existing DBs)
                    DECLARE @ckBookingsStatus sysname;
                    SELECT @ckBookingsStatus = cc.name
                    FROM sys.check_constraints cc
                    WHERE cc.parent_object_id = OBJECT_ID('BOOKINGS')
                      AND cc.definition LIKE '%Status%IN%Pending%Approved%Rejected%'
                      AND cc.definition NOT LIKE '%Terminated%';

                    IF @ckBookingsStatus IS NOT NULL
                    BEGIN
                        DECLARE @dropSql NVARCHAR(MAX) = 'ALTER TABLE BOOKINGS DROP CONSTRAINT ' + QUOTENAME(@ckBookingsStatus);
                        EXEC(@dropSql);
                        ALTER TABLE BOOKINGS ADD CONSTRAINT CK_BOOKINGS_Status CHECK (Status IN ('Pending','Approved','Rejected','Terminated'));
                    END

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
                        Reply NVARCHAR(MAX),
                        IsResolved BIT DEFAULT 0,
                        CreatedAt DATETIME2 DEFAULT GETDATE(),
                        CONSTRAINT FK_REVIEWS_PROPERTIES FOREIGN KEY (PropertyID) REFERENCES PROPERTIES(PropertyID) ON DELETE CASCADE,
                        CONSTRAINT FK_REVIEWS_USERS FOREIGN KEY (TenantID) REFERENCES USERS(UserID)
                    );

                    -- Add Reply and IsResolved columns to REVIEWS if they don't exist
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('REVIEWS') AND name = 'Reply')
                        ALTER TABLE REVIEWS ADD Reply NVARCHAR(MAX);
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('REVIEWS') AND name = 'IsResolved')
                        ALTER TABLE REVIEWS ADD IsResolved BIT DEFAULT 0;

                    -- MESSAGES (for tenant-landlord communication)
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MESSAGES' AND xtype='U')
                    CREATE TABLE MESSAGES (
                        MessageID INT IDENTITY(1,1) PRIMARY KEY,
                        SenderID INT NOT NULL,
                        ReceiverID INT NOT NULL,
                        PropertyID INT NOT NULL,
                        BookingID INT,
                        Content NVARCHAR(MAX) NOT NULL,
                        IsRead BIT DEFAULT 0,
                        CreatedAt DATETIME2 DEFAULT GETDATE(),
                        CONSTRAINT FK_MESSAGES_SENDER FOREIGN KEY (SenderID) REFERENCES USERS(UserID),
                        CONSTRAINT FK_MESSAGES_RECEIVER FOREIGN KEY (ReceiverID) REFERENCES USERS(UserID),
                        CONSTRAINT FK_MESSAGES_PROPERTY FOREIGN KEY (PropertyID) REFERENCES PROPERTIES(PropertyID)
                    );

                    -- MIGRATION: Fix MESSAGES table if it has old column names
                    IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('MESSAGES') AND name = 'MessageText')
                    BEGIN
                        EXEC sp_rename 'MESSAGES.MessageText', 'Content', 'COLUMN';
                    END
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('MESSAGES') AND name = 'IsRead')
                        ALTER TABLE MESSAGES ADD IsRead BIT DEFAULT 0;
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('MESSAGES') AND name = 'BookingID')
                        ALTER TABLE MESSAGES ADD BookingID INT NULL;

                    -- NOTIFICATIONS
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='NOTIFICATIONS' AND xtype='U')
                    CREATE TABLE NOTIFICATIONS (
                        NotificationID INT IDENTITY(1,1) PRIMARY KEY,
                        UserID INT NOT NULL,
                        Title NVARCHAR(200) NOT NULL,
                        Message NVARCHAR(MAX) NOT NULL,
                        IsRead BIT DEFAULT 0,
                        CreatedAt DATETIME2 DEFAULT GETDATE(),
                        CONSTRAINT FK_NOTIFICATIONS_USERS FOREIGN KEY (UserID) REFERENCES USERS(UserID) ON DELETE CASCADE
                    );
                ";

                using (var cmd = new SqlCommand(script, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // Safety: create NOTIFICATIONS even if previous script revisions were applied without it
                using (var cmd = new SqlCommand(@"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='NOTIFICATIONS' AND xtype='U')
                    CREATE TABLE NOTIFICATIONS (
                        NotificationID INT IDENTITY(1,1) PRIMARY KEY,
                        UserID INT NOT NULL,
                        Title NVARCHAR(200) NOT NULL,
                        Message NVARCHAR(MAX) NOT NULL,
                        IsRead BIT DEFAULT 0,
                        CreatedAt DATETIME2 DEFAULT GETDATE(),
                        CONSTRAINT FK_NOTIFICATIONS_USERS FOREIGN KEY (UserID) REFERENCES USERS(UserID) ON DELETE CASCADE
                    );", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
