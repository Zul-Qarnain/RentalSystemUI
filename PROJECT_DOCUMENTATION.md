# Rental Management System - Technical Documentation

## 1. Project Overview
**Project Name**: Rental System UI  
**Technology Stack**:
- **Language**: C# (.NET Core / Framework)
- **UI Framework**: Windows Forms (WinForms) with `AntdUI` for modern styling.
- **Database**: Microsoft SQL Server (LocalDB / SQLEXPRESS).
- **Architecture**: Layered Architecture (UI -> Service -> Repository -> Database).
- **Authentication**: Custom Session Management with BCrypt Password Hashing.
- **External APIs**: Resend API is used for transactional email delivery and SMTP-based verification, including account verification, notifications, and system alerts.
**Purpose**:  
A desktop application to manage house rentals. It allows **Tenants** to browse and book properties, **Landlords** to list properties and manage bookings, and **SuperAdmins** to oversee users and transactions.

---

## 2. System Architecture
The project follows a **Layered Architecture** with **Service-Repository Pattern** characteristics.

### High-Level Layers:
1.  **Presentation Layer (UI)**:
    -   **Forms**: `Form1` (Login), `UserDashboard` (Tenant), `HomeownerDashboard` (Landlord), `AdminDashboard` (SuperAdmin).
    -   **Components**: Reusable sections like `DashboardSections` and `AdminDashboardSections`.
    -   **Controls**: Uses standard WinForms controls + `AntdUI` library for buttons, tables, and messages.

2.  **Business Logic Layer (Services)**:
    -   Contains the "Brain" of the application.
    -   Located in `Services/` folder (e.g., `AuthService`, `PropertyService`, `TenantService`).
    -   Validates rules (e.g., "End date must be after Start date", "Only Tenants can book").

3.  **Data Access Layer (Data / Repositories)**:
    -   Handles raw SQL connections using `ADO.NET` (`SqlConnection`, `SqlCommand`).
    -   Located in `Data/` folder (e.g., `UserRepository`, `Database.cs`).
    -   Executes CRUD operations mostly via direct interaction or helper methods.

4.  **Database**:
    -   SQL Server Relational Database (`HomeRentalDB`).

---

## 3. Folder Structure & Responsibilities

| Folder | Responsibility |
| :--- | :--- |
| **`Forms`** | Contains all Windows Forms (Screens). `Form1.cs` is the entry point (Login). Subfolders organize dashboard sections. |
| **`Services`** | Business logic classes. They call Repositories/Database and are called by Forms. |
| **`Data`** | Database connection logic (`Database.cs`), repositories (`UserRepository.cs`), and initialization (`DatabaseInitializer.cs`). |
| **`Models`** | POCO (Plain Old CLR Objects) classes representing DB tables (e.g., `User`, `PropertyModel`). |
| **`Assets`** | Stores images and icons used in the UI (`menu.png`, `logo.png`). |
| **`db`** | SQL Scripts for table creation (`create_table.sql`) and data seeding. |
| **`Helpers`** | Utility classes like `EmailHelper` for OTPs. |

---

## 4. Database Design (Schema)

### Key Tables & Relationships

1.  **USERS**
    -   **Purpose**: Stores all users (Tenant, Landlord, SuperAdmin).
    -   **Columns**: `UserID` (PK), `Email`, `PasswordHash`, `UserType`, `Phone`, `IsActive`.
    -   **Key Logic**: `UserType` determines if they see Tenant or Landlord dashboard.

2.  **PROPERTIES**
    -   **Purpose**: Stores rental listings.
    -   **Columns**: `PropertyID` (PK), `LandlordID` (FK -> USERS), `RentAmount`, `Status` ('Available', 'Rented'), `Address`.
    -   **Relationship**: One Landlord has Many Properties.

3.  **BOOKINGS**
    -   **Purpose**: Tracks rental agreements.
    -   **Columns**: `BookingID`, `PropertyID` (FK), `TenantID` (FK -> USERS), `Status` ('Pending', 'Approved'), `StartDate`, `TotalAmount`.
    -   **Relationship**: One Property can have Many Bookings (historically), but usually one active.

4.  **PAYMENTS**
    -   **Purpose**: Records financial transactions.
    -   **Columns**: `PaymentID`, `BookingID` (FK), `Amount`, `TransactionID`, `Method` ('Bkash', etc.), `Status`.
    -   **Relationship**: One Booking has Many Payments.

5.  **MESSAGES**
    -   **Purpose**: Chat betweeen Tenant and Landlord.
    -   **Columns**: `SenderID`, `ReceiverID`, `PropertyID`, `Content`, `IsRead`.

6.  **PROPERTY_IMAGES**
    -   **Purpose**: Multi-image support for properties.
    -   **Columns**: `ImageID`, `PropertyID` (FK), `ImagePath`.
    -   **Relationship**: One Property has Many Images.

---

## 5. UI Forms Breakdown

### 5.1 Login & Registration (`Form1.cs`)
-   **Purpose**: Entry point. Handles Login, Signup, and Password Reset.
-   **Panels**: `pnlLogin` (Default), `pnlSignup` (Hidden).
-   **Logic**:
    -   **Login**: Calls `AuthService.Login()`. If success, checks `UserType` -> Opens `AdminDashboard`, `HomeownerDashboard`, or `UserDashboard`.
    -   **Signup**: Validates inputs -> Sends OTP (via `EmailHelper` mock/real) -> Opens `VerifyForm` -> If OTP correct, calls `AuthService.Register()`.

### 5.2 Tenant Dashboard (`UserDashboard.cs`)
-   **Sidebar Menu**:
    1.  **Browse Homes**: Loads `RentAllSearch`. Search bar filters by City/Rent/Amenities.
    2.  **My Home**: Loads `MyRentals`. Shows active rented property.
    3.  **My Bookings**: Loads `TenantRequestsList`. Shows pending/rejected requests.
    4.  **Messages**: Loads `MessagesSection`. Chat with landlord.
    5.  **Payments**: Loads `TenantPaymentList`. History of payments.
-   **Key Logic**: Uses `NavigateTo(Form)` to switch content inside the main panel.

### 5.3 Landlord Dashboard (`HomeownerDashboard.cs`)
-   **Sidebar Menu**:
    1.  **Dashboard**: Stats (Total Income, Total Properties).
    2.  **My Properties**: Loads `MyProperties`. Grid of owned properties with "Add New" button.
    3.  **Bookings**: Loads `RequestList`. Buttons to **Approve** or **Reject** tenant requests.
    4.  **Financials**: Loads `PaymentList`. View incoming payments.
-   **Add Property**: Opens `AddPropertyForm`. Uploads images and saves to DB.

### 5.4 SuperAdmin Dashboard (`AdminDashboard.cs`)
-   **Menu**:
    1.  **User Management**: View all users, Delete users.
    2.  **Transactions**: View system-wide payment history.

### 5.5 Property Details (`PropertyDetails.cs`)
-   **Purpose**: Detailed view of a specific house.
-   **Buttons**:
    -   **Book Now**: Checks if user is Tenant. Checks dates. Calls `TenantService.CreateBooking()`.
-   **Features**: Displays Start/End date pickers. Calculates total cost dynamically.

### 5.6 Payment Form (`Payment.cs`)
-   **Purpose**: Process rent payment.
-   **Inputs**: Mobile Banking Provider (Bkash/Nagad), Sender Phone, Transaction ID.
-   **Integration**: Contains `InitSSLCommerz` method which posts data to the sandbox API using `HttpClient` for online payment simulation.

---

## 6. Key Workflows

### 6.1 Booking Flow
1.  **Search**: Tenant searches in `RentAllSearch`. Defaults to "Available" properties.
2.  **View**: Tenant clicks a property card -> `PropertyDetails` opens.
3.  **Request**: Tenant selects dates and clicks "Book Now".
    -   System creates a row in `BOOKINGS` with status `'Pending'`.
4.  **Approval**: Landlord sees request in `RequestList`.
    -   Click **Approve** -> updates `BOOKINGS` status to `'Approved'`.
    -   Updates `PROPERTIES` status to `'Rented'`.

### 6.2 Payment Flow
1.  **Initiation**: In `TenantPaymentList`, Tenant sees "Pay Now" for approved bookings.
2.  **Form**: Opens `Payment.cs`. Tenant enters Amount and Transaction ID.
3.  **Processing**:
    -   Code validates inputs.
    -   Calls `TenantService.CreatePaymentForBooking`.
    -   Updates `PAYMENTS` table.
4.  **Verification**: Landlord sees payment in `PaymentList`.

### 6.3 Authentication & Security
-   **Hashing**: Passwords are **never** stored in plain text. Uses `BCrypt.Net.BCrypt.HashPassword()`.
-   **Session**: `AppSession` static class holds the logged-in user object globally.
-   **Role-Based Access**:
    -   `Form1` routes based on `UserType`.
    -   `PropertyService.AddProperty` checks if current user is 'Landlord'.
    -   `PropertyDetails` "Book Now" disabled for non-Tenants.

---

## 7. CRUD Operations

| Operation | Implementation Class | SQL Command Example |
| :--- | :--- | :--- |
| **Create** | `PropertyService.AddProperty` | `INSERT INTO PROPERTIES (...) VALUES (...)` |
| **Read** | `PropertyService.GetPropertiesByLandlord` | `SELECT * FROM PROPERTIES WHERE LandlordID=@id` |
| **Update** | `PropertyService.UpdateProperty` | `UPDATE PROPERTIES SET RentAmount=@amt WHERE PropertyID=@id` |
| **Delete** | `PropertyService.DeleteProperty` | `DELETE FROM PROPERTIES WHERE PropertyID=@id` |

**Data Access Strategy**:
-   Connection String is loaded from `.env` file first, then Environment Variables.
-   Uses `using (SqlConnection ...)` blocks to ensure connections are closed automatically (Preventing memory leaks).

---

## 8. Dashboard Statistics
Calculated in `LandlordService` or `AdminService` using aggregate SQL queries:
-   **Total Properties**: `SELECT COUNT(*) FROM PROPERTIES WHERE LandlordID = @id`
-   **Total Earnings**: `SELECT SUM(Amount) FROM PAYMENTS p JOIN BOOKINGS b ON p.BookingID = b.BookingID JOIN PROPERTIES prop ON b.PropertyID = prop.PropertyID WHERE prop.LandlordID = @id`
-   **Active Tenants**: `SELECT COUNT(DISTINCT TenantID) FROM BOOKINGS WHERE Status = 'Approved'`

---

## 9. Common Debugging & Extension

**How to Debug**:
-   **Startup Crash**: Check `.env` file presence. Ensure `db/create_table.sql` ran correctly.
-   **Login Fails**: Check `USERS` table in DB. Check `admin_hash.txt` for generated hash match.
-   **Database Error**: Look at `DatabaseState.cs` or `Console.WriteLine` output in Debug window.

**How to Add a New Feature (e.g., "Favorite Properties"):**
1.  **Database**: Create `FAVORITES` table (UserID, PropertyID).
2.  **Model**: Create `FavoriteModel.cs`.
3.  **Repository**: Add `AddFavorite(uid, pid)` in `TenantRepository`.
4.  **UI**: Add "Heart" icon in `PropertyDetails` and call the repository method on click.

---

## 10. Viva Questions & Answers

**Q: Which design pattern is used?**
A: A mix of **Layered Architecture** (UI, Service, Data) and **Repository Pattern** (Data access logic is encapsulated in Repository classes like `UserRepository`).

**Q: How is the database connected?**
A: Using `Microsoft.Data.SqlClient`. The connection string is securely loaded from a `.env` file using the `DotNetEnv` library.

**Q: How do you handle password security?**
A: We use **BCrypt** hashing. Logic is in `AuthService`. We never store plain text passwords.

**Q: Explain the startup logic.**
A: `Program.cs` initializes the application. It first calls `DatabaseInitializer.EnsureTablesExist()` to create tables if they are missing, then launches `Form1`.

**Q: How does the messaging system work?**
A: It's a polling-based or event-refresh system. Messages are stored in the `MESSAGES` table. The `MessagesSection` UI fetches conversation history filtering by `SenderID` and `ReceiverID`.
