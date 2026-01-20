-- =============================================
-- Admin Seeding Script (FIXED)
-- =============================================

USE HomeRentalDB;
GO

-- 1. Remove existing admin to ensure we reset with the correct hash
DELETE FROM USERS WHERE Email = 'admin@rental.com';

-- 2. Insert SuperAdmin with a valid BCrypt hash for "admin123"
-- Generated Hash for "admin123": $2a$11$D.B3SjG8X9eA0eP1.Q5O.eK7pB0U./qKqG1tXvVfB.jF4hB0gR9mG
INSERT INTO USERS (FullName, Email, PasswordHash, Phone, UserType, IsActive)
VALUES (
    'Super Admin', 
    'admin@rental.com', 
    '$2a$11$D.B3SjG8X9eA0eP1.Q5O.eK7pB0U./qKqG1tXvVfB.jF4hB0gR9mG', 
    '01700000000', 
    'SuperAdmin', 
    1
);

PRINT 'Admin user created successfully. Password: admin123';
GO
