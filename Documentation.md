***

# 📘 RentalSystemUI - Complete Technical Documentation

**Version:** 1.0.0  
**Framework:** .NET 8.0 (Windows Forms)  
**Architecture:** Layered Application (UI -> Logic -> Data)  
**UI System:** AntdUI (Flat Design)  

---

## 🏗️ 1. Project Architecture Overview

This project does not follow the traditional "Drag and Drop" WinForms style. It implements modern software design patterns to ensure scalability and security.

### **The "Single-Window" Philosophy (SPA)**
Instead of opening and closing multiple Windows (`Form2`, `Form3`), which causes flickering and poor UX, this application acts like a **Single Page Application (SPA)**.
*   **Technique:** We use `System.Windows.Forms.Panel` as containers.
*   **Mechanism:** To change screens, we toggle `.Visible = true/false` or use `BringToFront()`.
*   **Benefit:** The application feels faster and smoother, like a web app.

### **The "Secure Configuration" Pattern**
*   **Standard WinForms:** Usually stores connection strings in `App.config` (Visible to hackers).
*   **RentalSystemUI:** Stores secrets in a **`.env`** file. This file is **never** committed to GitHub (via `.gitignore`), keeping credentials safe.

---

## 📂 2. Folder Structure & Class Responsibilities

### 📂 **`root/`**
*   **`Program.cs`**: The application bootstrap.
    *   **Responsibilities:**
        1.  Loads Environment variables (`DotNetEnv.Env.Load()`).
        2.  Sets Global Localization (English).
        3.  Runs the startup form (`RentAllSearch` or `Form1`).

### 📂 **`Controllers/` (The Brains)**
This folder contains logic that handles data and external APIs. The UI Forms call these classes; they never talk to SQL directly.

#### 📄 **`DatabaseHelper.cs`**
*   **`_connectionString`**: Private variable loaded from `.env`.
*   **`GetConnection()`**: Returns a secure SQL connection object.
*   **`ExecuteQuery(string query, SqlParameter[] params)`**:
    *   Used for **SELECT** operations.
    *   Returns a `DataTable` (Rows/Columns).
    *   *Usage:* Getting property list, getting user details.
*   **`ExecuteNonQuery(string query, SqlParameter[] params)`**:
    *   Used for **INSERT / UPDATE / DELETE**.
    *   Returns `int` (Number of rows affected).
    *   *Usage:* Registering users, Booking a property.
*   **`ValidateUser(email, password)`**:
    *   Fetches the stored **BCrypt Hash** from the DB.
    *   Uses `BCrypt.Net.BCrypt.Verify()` to compare inputs securely.
    *   *Security Note:* We never store plain text passwords.

#### 📄 **`EmailHelper.cs`**
*   **`SendOtp(email, otp)`**:
    *   Fetches `RESEND_API_KEY` from `.env`.
    *   Constructs an HTML email.
    *   Sends via **Resend.com API**.

---

### 📂 **`Forms/` (The User Interface)**

#### 📄 **`Form1.cs` (Authentication Hub)**
Handles Login, Signup, and Role Selection.

*   **Variables:**
    *   `pnlLogin` / `pnlSignup`: The two main views.
    *   `selectedRole`: A string ("Tenant" or "Landlord") set by clicking the toggle buttons.
    *   `chkAgree`: Must be checked to proceed.
*   **Key Logic Flow (Signup):**
    1.  User clicks "Create Account".
    2.  `BtnSignup_Click` validates inputs (No blanks).
    3.  Calls `db.UserExists()` to prevent duplicates.
    4.  Calls `EmailHelper.SendOtp()` to email the user.
    5.  Opens `VerifyForm` (Modal Popup).
    6.  If OTP matches, calls `db.RegisterUser()` to save to SQL.

#### 📄 **`RentAllSearch.cs` (The Dashboard)**
The main browsing interface.

*   **UI Components:**
    *   `flowListings`: A `FlowLayoutPanel` that automatically arranges items in a grid.
    *   `pnlDetailsHost`: An empty full-screen panel used to "embed" the Details page when a card is clicked.
*   **Methods:**
    *   `LoadRealData()`: Queries SQL (`SELECT * FROM PROPERTIES`). Loops through rows.
    *   `AddProperty(...)`: This is a **UI Generator**. It creates a `Panel`, `PictureBox`, and `Labels` via code (programmatically) for every single house found in the database.
    *   `OpenDetailsPage(int id)`: Instantiates `PropertyDetails`, sets it to `TopLevel = false` (embedding), and adds it to `pnlDetailsHost`.

#### 📄 **`PropertyDetails.cs` (Specific View)**
Displays full info for one specific house.

*   **Constructor:** Accepts `int propertyId`.
*   **`LoadPropertyData()`**:
    *   Runs SQL Query: `SELECT * FROM PROPERTIES WHERE ID = @id`.
    *   Runs SQL Query: `SELECT * FROM PROPERTY_IMAGES WHERE PropertyID = @id`.
    *   Populates the Main PictureBox and the 4 thumbnails.
*   **`BackRequested` Event**:
    *   Since this form is embedded inside `RentAllSearch`, it cannot "Close" itself normally.
    *   It raises an event `BackRequested?.Invoke()`.
    *   `RentAllSearch` listens for this event and hides the panel.

#### 📄 **`VerifyForm.cs` (The Utility Modal)**
A reusable popup for inputting data.

*   **Features:**
    *   **Timer:** Counts down 60 seconds for OTP.
    *   **Resend:** Link becomes active after 60s.
    *   **Properties:** `InputValue` (Public string) allows the parent form to retrieve what the user typed.

---

## 🗄️ 3. Database Schema (SQL)

To run this project, your SQL Server must have these exact tables.

### **Table: USERS**
| Column | Type | Purpose |
| :--- | :--- | :--- |
| `UserID` | INT (PK) | Unique ID |
| `FullName` | NVARCHAR | User's name |
| `Email` | NVARCHAR | **Unique** email address |
| `Phone` | NVARCHAR | User contact |
| `PasswordHash` | NVARCHAR | **BCrypt** Encrypted string |
| `UserType` | NVARCHAR | 'Tenant' or 'Landlord' |

### **Table: PROPERTIES**
| Column | Type | Purpose |
| :--- | :--- | :--- |
| `PropertyID` | INT (PK) | Unique ID |
| `LandlordID` | INT (FK) | Links to USERS table |
| `Title` | NVARCHAR | House Headline |
| `RentAmount` | DECIMAL | Price per month |
| `Status` | NVARCHAR | 'Available' or 'Rented' |

### **Table: PROPERTY_IMAGES**
| Column | Type | Purpose |
| :--- | :--- | :--- |
| `ImageID` | INT (PK) | Unique ID |
| `PropertyID` | INT (FK) | Links to PROPERTIES table |
| `ImagePath` | NVARCHAR | **Absolute File Path** on disk (e.g., `C:\Assets\img1.png`) |

---

## ⚠️ 4. Troubleshooting & "Gotchas"

### **1. Namespace Ambiguity (`CS0104`)**
*   **Problem:** The code gets confused between `AntdUI.Panel` and `System.Windows.Forms.Panel`.
*   **Solution:** Always be explicit.
    *   ❌ `Panel myPanel = new Panel();`
    *   ✅ `System.Windows.Forms.Panel myPanel = new System.Windows.Forms.Panel();`

### **2. The "File Locked" Build Error**
*   **Problem:** "Could not copy... file is locked by RentalSystemUI (1234)".
*   **Cause:** You closed a window using `.Hide()`, but the app is still running in the background.
*   **Fix:** Open Terminal in VS and run `taskkill /F /IM RentalSystemUI.exe`.

### **3. Database Connection Fails**
*   **Cause:** The `.env` file is not in the `bin/Debug` folder.
*   **Fix:** Right-click `.env` in Solution Explorer -> Properties -> **Copy to Output Directory: Copy Always**.

### **4. Visual Studio Designer Crashes**
*   **Cause:** The Designer tries to run code like `db.Connect()` when you are just trying to view the form.
*   **Fix:** Ensure logic is inside the Constructor or `Load` event, and always check for nulls (`if (pictureBox1 != null)`).

---

## 🛠️ 5. How to Extend This Project

If you want to add a **"Booking History"** page:

1.  **Database:** Create a `BOOKINGS` table in SQL.
2.  **Controller:** Add `GetBookings(int userId)` method to `DatabaseHelper`.
3.  **UI:**
    *   Create `BookingHistory.cs` (Form).
    *   Use a `FlowLayoutPanel` loop (just like `RentAllSearch`) to generate cards.
    *   Call `db.GetBookings(currentUserId)`.

---

*This document serves as the architectural blueprint for the RentalSystemUI application.*
