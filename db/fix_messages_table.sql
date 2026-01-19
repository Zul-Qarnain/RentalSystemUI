-- Run this script to fix the MESSAGES table schema mismatch
-- Execute in SQL Server Management Studio against HomeRentalDB

USE HomeRentalDB;
GO

-- Step 1: Rename MessageText to Content if it exists
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('MESSAGES') AND name = 'MessageText')
BEGIN
    EXEC sp_rename 'MESSAGES.MessageText', 'Content', 'COLUMN';
    PRINT 'Renamed MessageText to Content';
END
GO

-- Step 2: Add IsRead column if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('MESSAGES') AND name = 'IsRead')
BEGIN
    ALTER TABLE MESSAGES ADD IsRead BIT DEFAULT 0;
    PRINT 'Added IsRead column';
END
GO

-- Step 3: Add BookingID column if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('MESSAGES') AND name = 'BookingID')
BEGIN
    ALTER TABLE MESSAGES ADD BookingID INT NULL;
    PRINT 'Added BookingID column';
END
GO

-- Verify the schema
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'MESSAGES'
ORDER BY ORDINAL_POSITION;
GO
