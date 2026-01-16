-- Insert Landlord
INSERT INTO USERS (FullName, Email, PasswordHash, Phone, UserType, IsActive)
VALUES ('Mohammad Shihab Hossain', 'shihab@aiub.edu', 'hashed_password_here', '01700000000', 'Landlord', 1);

-- Insert Tenant
INSERT INTO USERS (FullName, Email, PasswordHash, Phone, UserType, IsActive)
VALUES ('Demo Tenant', 'tenant@test.com', 'hashed_password_here', '01800000000', 'Tenant', 1);

-- Get Landlord ID
DECLARE @LandlordID INT = (SELECT UserID FROM USERS WHERE Email='shihab@aiub.edu');

-- Insert Property
INSERT INTO PROPERTIES
(LandlordID, Title, Description, Address, City, RentAmount, Status, Rooms, Kitchen, WashRoom, IsPetAllowed, IsAC)
VALUES
(@LandlordID, 'Sunset Apartments, Unit 4B', 'Modern 2-bedroom apartment with a great view.',
 '123 Kuril, Dhaka', 'Dhaka', 1265.00, 'Available', 2, 1, 1, 0, 1);

-- Get Property ID
DECLARE @PropertyID INT = SCOPE_IDENTITY();

-- Insert Images
INSERT INTO PROPERTY_IMAGES (PropertyID, ImagePath)
VALUES 
(@PropertyID, 'C:\Users\shiha\source\repos\RentalSystemUI\Assets\Properties_Pic\Washroom.png'),
(@PropertyID, 'C:\Users\shiha\source\repos\RentalSystemUI\Assets\Properties_Pic\Bedroom.png'),
(@PropertyID, 'C:\Users\shiha\source\repos\RentalSystemUI\Assets\Properties_Pic\Coridoor.png'),
(@PropertyID, 'C:\Users\shiha\source\repos\RentalSystemUI\Assets\Properties_Pic\Kitchen.png');
