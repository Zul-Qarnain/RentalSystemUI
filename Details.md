**RentalSystemUI** project. This document contains every detail required for an AI or a human developer to understand, repair, or extend the system without prior knowledge.

---

# 📘 RentalSystemUI: Master Technical Documentation & Architecture Analysis

**Version:** 1.2.0 (Stable)  
**Framework:** .NET 8.0 (Windows Forms)  
**Architecture:** Modular Monolith with MVC-style separation  
**UI System:** AntdUI (Flat/Modern Design)  
**Database:** Microsoft SQL Server  

---

## 1. Project Overview & Problem Solved

**RentalSystemUI** is a modern desktop application designed to bridge the gap between Landlords and Tenants.

*   **The Problem:** Traditional rental software is often ugly, clunky, and uses outdated Windows 98-style interfaces. Managing properties and bookings usually requires complex web portals.
*   **The Solution:** A high-performance, native Windows application that mimics the look and feel of modern web apps (like Airbnb) using **AntdUI** for styling. It features secure authentication, role-based access, and a seamless "Single Page Application" (SPA) navigation style within a desktop window.

---

## 2. File & Folder Structure (The Architecture)

The project was refactored from a flat list of files into a clean, modular structure.

### 📂 **Root Directory**
*   **`Program.cs`**: The application entry point.
    *   **Responsibility:** Initializes Visual Studio configuration, loads Environment Variables (`DotNetEnv`), sets Localization (English), and launches the initial form (`Form1`).
*   **`.env`**: (Git-Ignored) Stores secrets.
    *   **Contents:** `DB_CONNECTION` string and `RESEND_API_KEY`.
    *   **Critical:** Must be set to "Copy to Output Directory: Copy Always".
*   **`RentalSystemUI.csproj`**: Project configuration file defining dependencies (.NET 8, AntdUI, SQLClient, etc.).

### 📂 **Forms/** (The View Layer)
Contains all UI logic. Note that we use **Panel Swapping** rather than opening many windows.

*   **`Form1.cs` / `.Designer.cs`**: The **Authentication Hub**.
    *   **UI:** Split-screen layout. Left side is a static brand panel with a transparent overlay. Right side swaps between `pnlLogin` and `pnlSignup`.
    *   **Logic:** Handles Login, Registration, OTP Verification, and Forgot Password.
*   **`RentAllSearch.cs` / `.Designer.cs`**: The **Main Dashboard**.
    *   **UI:** Contains a top navigation bar, a side filter menu, and a central `FlowLayoutPanel`.
    *   **Logic:** Fetches properties from the DB and dynamically generates UI Cards (Image + Text) for each property.
    *   **Embedding:** Contains `pnlDetailsHost` which acts as a container to show property details without opening a new window.
*   **`PropertyDetails.cs` / `.Designer.cs`**: The **Single Property View**.
    *   **UI:** Scrollable layout with a large hero image, thumbnail gallery, description, and a "Booking Card" on the right.
    *   **Logic:** Fetches specific property data and images by ID. Raises an event (`BackRequested`) to close itself.
*   **`VerifyForm.cs` / `.Designer.cs`**: A **Reusable Modal**.
    *   **UI:** A clean popup replacing `Microsoft.VisualBasic.InputBox`.
    *   **Logic:** customizable for Email entry, OTP entry (with countdown timer), or Password entry.

### 📂 **Controllers/** (The Logic/Service Layer)
Separates business logic from the UI.

*   **`DatabaseHelper.cs`**: The **Data Access Layer (DAL)**.
    *   **Responsibility:** Manages `SqlConnection`. Contains methods for `ExecuteQuery` (Reading) and `ExecuteNonQuery` (Writing). Handles `BCrypt` password hashing and validation.
*   **`EmailHelper.cs`**: The **Notification Service**.
    *   **Responsibility:** specific wrapper for the **Resend API**. Sends HTML-formatted OTP emails asynchronously.

### 📂 **Assets/**
*   Stores static resources (images for placeholders, icons) used by the application.

---

## 3. Key Variables, Classes & Components Explanation

### **In `Form1.cs`**
*   `pnlLogin`, `pnlSignup`: `System.Windows.Forms.Panel` objects. We toggle `.Visible` to switch views.
*   `txtSignupName`, `txtSignupPhone`: `AntdUI.Input` controls. Used for capturing user data.
*   `btnRoleTenant`, `btnRoleLandlord`: Buttons that act as a toggle switch.
*   `selectedRole`: String variable. Updates when role buttons are clicked. Defaults to "Tenant".
*   `AttachDragEvents(Control c)`: A helper method that hooks into MouseDown/Move/Up events to allow the borderless window to be dragged by the user.

### **In `DatabaseHelper.cs`**
*   `_connectionString`: Loaded securely from `.env`.
*   `ValidateUser(email, password)`: Retrieves the hash from the DB and uses `BCrypt.Verify()` to compare. Returns `true/false` + `role` + `name`.
*   `RegisterUser(...)`: Hashes the password using `BCrypt.HashPassword()` before inserting into SQL.

### **In `RentAllSearch.cs`**
*   `flowListings`: A `FlowLayoutPanel`. This is a container that automatically arranges child controls (Property Cards) in a grid.
*   `AddProperty(...)`: A **UI Generator Method**. It creates a new `AntdUI.Panel` and `PictureBox` via code for every database row found.

---

## 4. Tech Stack & Library Interconnections

1.  **AntdUI (UI Library):**
    *   **Purpose:** Provides the "Flat", rounded, modern look (Buttons, Inputs, DatePickers).
    *   **Connection:** Used in all Designer files. Requires explicit namespace resolution (`AntdUI.Button`) to avoid conflict with WinForms.
2.  **Microsoft.Data.SqlClient:**
    *   **Purpose:** The driver to talk to SQL Server.
    *   **Connection:** Used inside `DatabaseHelper.cs`.
3.  **BCrypt.Net-Next:**
    *   **Purpose:** Industry-standard password security.
    *   **Connection:** Used in `DatabaseHelper` during Registration (Hash) and Login (Verify).
4.  **Resend:**
    *   **Purpose:** Sending emails via API (bypass SMTP complexity).
    *   **Connection:** Used in `EmailHelper.cs`.
5.  **DotNetEnv:**
    *   **Purpose:** Loads configuration from text files to Environment variables.
    *   **Connection:** Called in `Program.cs` start-up and `DatabaseHelper` constructor.

---

## 5. Execution Flow (Step-by-Step)

1.  **Startup:** `Program.Main()` -> Loads `.env` -> Runs `Form1`.
2.  **Authentication:**
    *   User clicks "Sign Up" -> `pnlLogin` hides, `pnlSignup` shows.
    *   User fills form -> Click Submit.
    *   **Validation:** App checks for empty fields & Database Duplicates (`db.UserExists`).
    *   **Verification:** App generates random 6-digit OTP -> Sends via `EmailHelper`.
    *   **Modal:** `VerifyForm` opens -> User enters code.
    *   **Success:** If code matches -> `db.RegisterUser` writes to SQL -> Redirect to Login.
3.  **Login:**
    *   User enters creds -> `db.ValidateUser` checks hash.
    *   Success -> `Form1` hides -> `RentAllSearch` opens.
4.  **Browsing:**
    *   `RentAllSearch` constructor calls `LoadRealData()`.
    *   `db.ExecuteQuery` fetches properties with `Status='Available'`.
    *   Loop creates UI cards in `flowListings`.
5.  **Details & Booking:**
    *   User clicks a card -> `OpenDetailsPage(ID)` is called.
    *   `PropertyDetails` form is instantiated -> Fetches images/text for that specific ID.
    *   Form is embedded into `pnlDetailsHost` (Overlay).
    *   User clicks "Close" -> `BackRequested` event fires -> Overlay hides.

---

## 6. Logic Decisions & Design Choices (Why we did this)

*   **Namespace Ambiguity (`CS0104`):** AntdUI and WinForms share names (`Panel`, `Label`).
    *   *Decision:* We explicitly type `System.Windows.Forms.Panel` in the code to prevent compiler confusion.
*   **The "File Locked" Error:** The app was running in the background because `this.Hide()` doesn't kill the process.
    *   *Decision:* Added logic to `Form1.cs` so that when the Search window closes, it kills the Login window (`this.Close()`), terminating the app properly.
*   **Designer vs. Code:**
    *   *Decision:* Initially, we built the Signup page via code. This made it hard to edit. We refactored it back into `Form1.Designer.cs` so visual editing is possible, but kept the logic separate.
*   **Demo Mode:** Since university PCs often lack SQL Server/Admin rights.
    *   *Decision:* We prepared a "Self-Contained" build logic and a "Mock Data" switch in the code to allow the UI to function without a database for presentations.

---

## 7. Critical Errors & Solutions Log

| Error | Cause | Solution |
| :--- | :--- | :--- |
| **CS0104 Ambiguous Reference** | AntdUI.Panel vs WinForms.Panel | Explicitly typed `System.Windows.Forms.Panel` in variable declarations. |
| **File Locked / Copy Failed** | App running in background (Zombie process) | Used `taskkill /F /IM RentalSystemUI.exe` and fixed closing logic. |
| **Null Reference (Constructor)** | Variables declared but not init | Used `null!` (e.g., `private Panel p = null!;`) to satisfy the compiler. |
| **CS0103 Name does not exist** | Designer file looking for deleted event methods | Re-added dummy methods (e.g., `label6_Click`) to `Form1.cs` or cleaned `Designer.cs`. |
| **Chinese Date Picker** | AntdUI default localization | Added `AntdUI.Localization.Set("en-US")` (or removed date picker in older versions). |

---

## 8. Configuration & Database Schema

### **.env File**
```ini
DB_CONNECTION="Data Source=.\SQLEXPRESS;Initial Catalog=RentalDB;Integrated Security=True;TrustServerCertificate=True"
RESEND_API_KEY="re_12345_YOUR_KEY"
```

### **SQL Schema**
*   **USERS:** `UserID`, `FullName`, `Email`, `PasswordHash`, `UserType` ('Tenant'/'Landlord'), `Phone`.
*   **PROPERTIES:** `PropertyID`, `Title`, `Address`, `City`, `RentAmount`, `Status`, `Description`.
*   **PROPERTY_IMAGES:** `ImageID`, `PropertyID`, `ImagePath` (Stores local path like `C:\Assets\img.png`).

---

## 9. Current Limitations & Future Roadmap

### **Current Limitations**
1.  **Booking is Visual Only:** Clicking "Book Now" shows a success message but does not save to the database yet.
2.  **Images are Local Paths:** The database stores `C:\...`. If the app moves to another PC, images break. (Solution: Store images in `Assets` folder relative to the EXE).
3.  **No Landlord Dashboard:** Landlords cannot yet add properties via the UI.

### **Future Roadmap (To-Do)**
1.  **Implement Booking:** Create `BOOKINGS` table and insert rows when "Book Now" is clicked.
2.  **Property Upload:** Create a form for Landlords to upload images and text.
3.  **Payment Integration:** Replace the UI stub with Stripe or PayPal SDK.

---

## 10. How to Run & Build (From Scratch)

If a new developer takes over:

1.  **Clone Repo:** `git clone <url>`.
2.  **Restore Packages:** Run `dotnet restore` or let VS restore NuGet packages.
3.  **Setup Database:** Run the SQL script (Schema) on a local SQL Server.
4.  **Create .env:** Create a `.env` file in the root with the connection string. **Set properties to "Copy Always".**
5.  **Run:** Press F5.

---

*This documentation covers the entire lifecycle, architecture, and codebase of RentalSystemUI.*