
---

# ✅ What’s Already Perfect

Your prompt has:

* ✔ Clear role definition (Senior .NET Architect)
* ✔ Clear current problem statement
* ✔ Clear target architecture (N-Tier)
* ✔ Clear folder structure
* ✔ Clear rules (no DataTable, no SQL in Forms, etc.)
* ✔ Clear step-by-step execution
* ✔ Clear constraints (don’t touch Designer, keep AntdUI, keep BCrypt, etc.)
* ✔ Clear schema reference
* ✔ Clear migration plan

This is **much better** than what most people write.

---

# ⚠️ Small Risks in Current Prompt

There are only **3 small risks**:

1. The AI might:

   * Change namespaces
   * Rename your Forms
   * Slightly change UI logic
   * Over-refactor and break flow

2. The AI might:

   * Over-engineer (add interfaces, DI containers, etc.)

3. The AI might:

   * Forget your **existing helpers** (EmailHelper, DotNetEnv usage)

---

# 🛠️ How to Improve It (Minimal Changes)

I’ll give you an **IMPROVED VERSION** of your prompt with **extra safety rails**.

You can copy this and use it instead of the original.

---

# 🧠 ✅ IMPROVED MASTER PROMPT (FINAL VERSION)

> This is your original prompt, but hardened.

````markdown
# 🚀 Task: Refactor WinForms Project to N-Tier Clean Architecture

**Role:** You are a Senior .NET C# Architect.
**Project:** `RentalSystemUI` (Windows Forms .NET 8).
**Context:** The application is fully working, but the UI layer (`Forms`) currently contains SQL queries, business logic, and database access code.

**Goal:** Refactor the codebase to follow a professional **Model–Repository–Service** architecture WITHOUT breaking:
- Any existing UI
- Any navigation flow
- Any AntdUI usage
- Any form names, control names, or events

This is a **refactor, not a rewrite**.

---

## 🏗️ 1. Target Architecture

### **A. `Models/` (Domain Layer)**
Create POCO classes:

* `User.cs` (UserID, FullName, Email, Phone, PasswordHash, UserType)
* `Property.cs` (PropertyID, Title, Address, City, RentAmount, Description, Status, LandlordID)
* `PropertyImage.cs` (ImageID, PropertyID, ImagePath)

Rules:
- Only properties + optional simple computed properties
- No SQL, no logic, no services

---

### **B. `Data/` (Repository Layer)**
Handle all raw SQL only.

* `Database.cs`: 
  - Loads `.env` using DotNetEnv
  - Provides `protected SqlConnection GetConnection()`

* `UserRepository.cs`:
  - `GetByEmail(string email)`
  - `Insert(User user)`
  - `Exists(string email, string phone)`

* `PropertyRepository.cs`:
  - `GetAllAvailable()`
  - `GetById(int id)`
  - `GetImagesByPropertyId(int id)`

Rules:
- Use `Microsoft.Data.SqlClient`
- Return `Model` or `List<Model>` only
- NEVER return `DataTable`
- NEVER show MessageBox here

---

### **C. `Services/` (Business Logic Layer)**

* `AuthService.cs`:
  - `Login(email, password)` → returns `User?`
  - `Register(User user, plainPassword)` → returns `bool`

* `PropertyService.cs`:
  - `GetSearchProperties()`
  - `GetPropertyDetails(int id)`

Rules:
- Use BCrypt here
- Do validation here
- Coordinate repositories here
- No UI code

---

### **D. `Forms/` (Presentation Layer)**

Refactor only:

- `Form1.cs`
- `RentAllSearch.cs`
- `PropertyDetails.cs`

Rules:
- ❌ No SQL
- ❌ No SqlConnection
- ❌ No SqlCommand
- ❌ No DataTable
- ✅ Only call Services
- ❗ Do NOT change `.Designer.cs` files
- ❗ Do NOT rename Forms, controls, or events
- ❗ Keep UI behavior EXACTLY the same

---

## 🗄️ Database Schema

```sql
CREATE TABLE USERS (
  UserID INT PK,
  FullName NVARCHAR,
  Email NVARCHAR,
  PasswordHash NVARCHAR,
  Phone NVARCHAR,
  UserType NVARCHAR
);

CREATE TABLE PROPERTIES (
  PropertyID INT PK,
  Title NVARCHAR,
  Address NVARCHAR,
  City NVARCHAR,
  RentAmount DECIMAL,
  Status NVARCHAR,
  Description NVARCHAR
);

CREATE TABLE PROPERTY_IMAGES (
  ImageID INT PK,
  PropertyID INT FK,
  ImagePath NVARCHAR
);
````

---

## ⚠️ VERY IMPORTANT CONSTRAINTS

1. This is a **refactor**, not a redesign.
2. Do NOT introduce:

   * Dependency Injection frameworks
   * Interfaces
   * MVVM
   * Web patterns
3. Keep it simple and compatible with WinForms.
4. Keep using:

   * AntdUI
   * BCrypt.Net
   * DotNetEnv
   * Resend Email Helper
5. Preserve:

   * App flow
   * Navigation logic
   * Event names
   * Form interactions

---

## 🧭 Step-by-step Output Order

1. Generate **Models**
2. Generate **Database.cs**
3. Generate **Repositories**
4. Generate **Services**
5. Show refactored:

   * `Form1.cs`
   * `RentAllSearch.cs`
   * `PropertyDetails.cs`

Start now with:
👉 **Step 1: Models**
👉 **Step 2: Database.cs**

```

---

# 🏆 Verdict

> Your original prompt = **very good**  
> This improved version = **industry-grade, safe, controlled refactor prompt**

---

# ⚠️ Important Advice

When using this:

- Run it **in chunks**
- Don’t paste 1 million lines at once
- Review every file before replacing

---

# 🎯 Final Answer

> ✅ Your prompt is already excellent.  
> ✅ With the small safety improvements above, it becomes **almost impossible for the AI to destroy your project**.

---

If you want, **I can now act as that AI agent** and start with:

> Step 1: Generating your Models  
> Step 2: Generating Database.cs
```
