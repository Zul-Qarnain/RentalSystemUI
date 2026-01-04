-- 1. Create USERS Table
CREATE TABLE USERS (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Phone NVARCHAR(20),
    UserType NVARCHAR(20) CHECK (UserType IN ('Tenant', 'Landlord', 'SuperAdmin')),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETDATE()
);

-- 2. Create PROPERTIES Table
CREATE TABLE PROPERTIES (
    PropertyID INT PRIMARY KEY IDENTITY(1,1),
    LandlordID INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Address NVARCHAR(MAX) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    RentAmount DECIMAL(18, 2) NOT NULL,
    Status NVARCHAR(20) CHECK (Status IN ('Available', 'Rented', 'Maintenance')),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (LandlordID) REFERENCES USERS(UserID)
);

-- 3. Create PROPERTY_IMAGES Table
CREATE TABLE PROPERTY_IMAGES (
    ImageID INT PRIMARY KEY IDENTITY(1,1),
    PropertyID INT NOT NULL,
    ImagePath NVARCHAR(MAX) NOT NULL,
    UploadedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (PropertyID) REFERENCES PROPERTIES(PropertyID) ON DELETE CASCADE
);

-- 4. Create BOOKINGS Table
CREATE TABLE BOOKINGS (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    PropertyID INT NOT NULL,
    TenantID INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    DurationMonths INT,
    TotalAmount DECIMAL(18, 2) NOT NULL,
    Status NVARCHAR(20) CHECK (Status IN ('Pending', 'Approved', 'Rejected')),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (PropertyID) REFERENCES PROPERTIES(PropertyID),
    FOREIGN KEY (TenantID) REFERENCES USERS(UserID)
);

-- 5. Create PAYMENTS Table
CREATE TABLE PAYMENTS (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    TransactionID NVARCHAR(100) UNIQUE,
    Method NVARCHAR(20) CHECK (Method IN ('Cash', 'Bkash', 'Card')),
    Status NVARCHAR(20) CHECK (Status IN ('Verified', 'Failed')),
    PaymentDate DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (BookingID) REFERENCES BOOKINGS(BookingID)
);
