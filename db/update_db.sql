USE HomeRentalDB;
GO

--------------------------------------------------
-- 1) UPDATE BOOKINGS: Add 'Cancelled' status
--------------------------------------------------

DECLARE @bookingConstraint NVARCHAR(200);

SELECT @bookingConstraint = dc.name
FROM sys.check_constraints dc
JOIN sys.tables t ON dc.parent_object_id = t.object_id
WHERE t.name = 'BOOKINGS' AND dc.definition LIKE '%Pending%';

IF @bookingConstraint IS NOT NULL
BEGIN
    PRINT 'Dropping old BOOKINGS status constraint...';
    EXEC('ALTER TABLE BOOKINGS DROP CONSTRAINT ' + @bookingConstraint);
END
GO

ALTER TABLE BOOKINGS
ADD CONSTRAINT CK_BOOKINGS_STATUS
CHECK (Status IN ('Pending', 'Approved', 'Rejected', 'Cancelled'));
GO

--------------------------------------------------
-- 2) UPDATE PAYMENTS: Add 'Refunded' status
--------------------------------------------------

DECLARE @paymentConstraint NVARCHAR(200);

SELECT @paymentConstraint = dc.name
FROM sys.check_constraints dc
JOIN sys.tables t ON dc.parent_object_id = t.object_id
WHERE t.name = 'PAYMENTS' AND dc.definition LIKE '%Verified%';

IF @paymentConstraint IS NOT NULL
BEGIN
    PRINT 'Dropping old PAYMENTS status constraint...';
    EXEC('ALTER TABLE PAYMENTS DROP CONSTRAINT ' + @paymentConstraint);
END
GO

ALTER TABLE PAYMENTS
ADD CONSTRAINT CK_PAYMENTS_STATUS
CHECK (Status IN ('Verified', 'Failed', 'Refunded'));
GO

--------------------------------------------------
-- 3) CREATE REFUND_REQUESTS TABLE (NEW)
--------------------------------------------------

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'REFUND_REQUESTS')
BEGIN
    PRINT 'Creating REFUND_REQUESTS table...';

    CREATE TABLE REFUND_REQUESTS (
        RefundRequestID INT IDENTITY(1,1) PRIMARY KEY,
        BookingID INT NOT NULL,
        TenantID INT NOT NULL,
        Reason NVARCHAR(MAX),
        Status NVARCHAR(20) CHECK (Status IN ('Pending', 'Approved', 'Rejected')) DEFAULT 'Pending',
        RequestedAt DATETIME2 DEFAULT GETDATE(),

        CONSTRAINT FK_REFUND_BOOKING FOREIGN KEY (BookingID) REFERENCES BOOKINGS(BookingID),
        CONSTRAINT FK_REFUND_TENANT FOREIGN KEY (TenantID) REFERENCES USERS(UserID)
    );

    CREATE INDEX IDX_REFUND_BOOKING ON REFUND_REQUESTS(BookingID);
END
ELSE
BEGIN
    PRINT 'REFUND_REQUESTS table already exists. Skipping...';
END
GO

--------------------------------------------------
-- 4) OPTIONAL: Ensure PROPERTIES has default status
--------------------------------------------------

IF NOT EXISTS (
    SELECT * FROM sys.default_constraints dc
    JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
    JOIN sys.tables t ON t.object_id = c.object_id
    WHERE t.name = 'PROPERTIES' AND c.name = 'Status'
)
BEGIN
    PRINT 'Adding default value to PROPERTIES.Status...';

    ALTER TABLE PROPERTIES
    ADD CONSTRAINT DF_PROPERTIES_STATUS DEFAULT 'Available' FOR Status;
END
GO

--------------------------------------------------
-- DONE
--------------------------------------------------

PRINT 'Database update completed successfully!';
GO
