-- 1. Insert a sample Landlord (Needed for Foreign Key)
INSERT INTO USERS (FullName, Email, PasswordHash, Phone, UserType, IsActive)
VALUES ('Mohammad Shihab Hossain', 'shihab@aiub.edu', 'hashed_password_here', '01700000000', 'Landlord', 1);

-- Get the ID of the user we just created
DECLARE @LandlordID INT = SCOPE_IDENTITY();

-- 2. Insert a sample Property
INSERT INTO PROPERTIES (LandlordID, Title, Description, Address, City, RentAmount, Status)
VALUES (@LandlordID, 'Sunset Apartments, Unit 4B', 'Modern 2-bedroom apartment with a great view.', '123 Kuril, Dhaka', 'Dhaka', 1265.00, 'Available');

-- Get the ID of the property we just created
DECLARE @PropertyID INT = SCOPE_IDENTITY();

-- 3. Insert the 4 Property Images using your specific file paths
INSERT INTO PROPERTY_IMAGES (PropertyID, ImagePath)
VALUES 
(@PropertyID, 'C:\Users\shiha\source\repos\RentalSystemUI\Assets\Properties_Pic\Washroom.png'),
(@PropertyID, 'C:\Users\shiha\source\repos\RentalSystemUI\Assets\Properties_Pic\Bedroom.png'),
(@PropertyID, 'C:\Users\shiha\source\repos\RentalSystemUI\Assets\Properties_Pic\Coridoor.png'),
(@PropertyID, 'C:\Users\shiha\source\repos\RentalSystemUI\Assets\Properties_Pic\Kitchen.png');
