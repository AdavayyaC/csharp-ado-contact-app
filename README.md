# 📖 Contact Manager — ADO.NET WinForms Application

A full-featured **Contact Management System** built with **C# WinForms** and **ADO.NET** using **SQLite** as the database engine. This project demonstrates both **Connected** and **Disconnected** ADO.NET architectures with real-world CRUD operations, input validation, and activity logging.

> 🎓 **Built as part of 8th Semester — C# and .NET Course Project**

---

## 🖼️ Screenshots

| Main Application | Activity Log |
|:---:|:---:|
| Contact list with groups & favorites | Color-coded audit trail |

---

## ✨ Features

### Core CRUD Operations
- ✅ **Add** new contacts with validation
- ✅ **Update** existing contact details
- ✅ **Delete** contacts with name-based confirmation dialog
- ✅ **View** contact details by selecting from the list

### Contact Groups (Categories)
- 📂 Organize contacts into: **General, Friend, Family, Work, College, Other**
- 📂 Group name displayed alongside contact name in the list
- 📂 Search/filter contacts by group name

### Favorite Contacts ⭐
- ⭐ Mark/unmark contacts as favorites with one click
- ⭐ Favorites are **automatically sorted to the top** of the list
- ⭐ Visual indicator (⭐ emoji) in the contact list

### Input Validation
- 🔒 **First Name** — Required field
- 🔒 **Phone Number** — Required, must be exactly **10 digits**
- 🔒 Phone field blocks non-digit characters in real-time
- 🔒 Clear validation error messages with guidance

### Duplicate Detection
- ⚠️ Warns before adding a contact with the **same name or phone number**
- ⚠️ User can still force-add if desired

### Activity Log (Audit Trail) 📋
- 📋 Logs every **ADD, UPDATE, DELETE, FAVORITE** action
- 📋 Timestamped entries stored in a separate `ActivityLog` table
- 📋 Color-coded viewer: 🟢 Add | 🔵 Update | 🔴 Delete | 🟡 Favorite
- 📋 Summary statistics (total actions, adds, updates, deletes)
- 📋 Clear all logs option

### Search
- 🔍 **Real-time search** — filters contacts as you type
- 🔍 Searches across name, phone number, email, and group

### UI/UX
- 🎨 Modern blue header with app branding
- 🎨 Color-coded action buttons (Green, Blue, Red, Gold, Gray)
- 🎨 Placeholder text in all input fields
- 🎨 Status bar with contextual messages
- 🎨 Contact counter with favorite count
- 🎨 Welcome splash screen on startup

---

## 🛠️ Prerequisites

| Tool | Version | Purpose |
|------|---------|---------|
| **.NET SDK** | 8.0+ | Build & run the application |
| **Git** | Any | Clone the repository |

> **No database server needed!** SQLite runs as a local file — zero configuration.

### Check Prerequisites

```powershell
dotnet --version    # Should show 8.0.x or higher
git --version       # Should show git version 2.x
```

---

## 🚀 Getting Started

### 1. Clone the Repository

```powershell
git clone <your-repo-url>
cd csharp-ado-contact-app
```

### 2. Restore Dependencies

```powershell
dotnet restore
```

### 3. Build the Project

```powershell
dotnet build
```

### 4. Run the Application

```powershell
dotnet run
```

> 📦 The SQLite database (`ContactDB.db`) is **created automatically** on first run with 5 sample contacts.

---

## 📁 Project Structure

```
csharp-ado-contact-app/
│
├── 📄 Program.cs                    # Application entry point
├── 📄 App.config                    # Database connection configuration
├── 📄 csharp-ado-contact.csproj     # Project file (.NET 8 WinForms)
├── 📄 README.md                     # This file
├── 📄 ARCHITECTURE.md               # Architecture & design document
├── 📄 setup_database.sql            # SQL reference script
│
├── 📁 Model/
│   └── 📄 Contact.cs                # Contact data entity (POCO)
│
├── 📁 UI/
│   ├── 📄 FormBase.cs               # Base form — UI logic, validation, search
│   ├── 📄 FormBase.Designer.cs      # WinForms layout (auto-generated)
│   ├── 📄 FormConnected.cs          # Connected ADO.NET mode
│   ├── 📄 FormDisconnected.cs       # Disconnected ADO.NET mode
│   └── 📄 FormActivityLog.cs        # Activity log viewer
│
└── 📁 bin/Debug/net8.0-windows/
    └── 📄 ContactDB.db              # SQLite database (auto-created)
```

---

## 🔄 ADO.NET Modes Explained

This project implements **two distinct ADO.NET architectures** to demonstrate the difference:

### 🟢 Connected Mode (`FormConnected.cs`)

```
[User Action] → [Open Connection] → [Execute SQL] → [Close Connection] → [Display Result]
```

- Opens a **new database connection for every operation**
- Executes SQL directly using `SqliteCommand` + `SqliteDataReader`
- Changes are **saved immediately** to the database
- **Best for:** Real-time applications where data freshness is critical

### 🔵 Disconnected Mode (`FormDisconnected.cs`)

```
[Startup] → [Load ALL data into DataTable] → [Work in Memory] → [Save ALL on App Close]
```

- Loads data once into an **in-memory `DataTable`**
- All CRUD operations happen **entirely in memory**
- Changes are synced to the database **only when the app closes** (`OnClosing`)
- **Best for:** Batch operations, offline-capable applications

### How to Switch Modes

Edit `Program.cs` line 22:

```csharp
// Connected mode (default) — saves immediately
Application.Run(new FormConnected());

// Disconnected mode — saves on close
// Application.Run(new FormDisconnected());
```

---

## 📦 NuGet Packages Used

| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.Data.Sqlite` | 8.0.11 | SQLite ADO.NET provider |
| `System.Configuration.ConfigurationManager` | 8.0.0 | Read `App.config` settings |

---

## 🗄️ Database Schema

### Contacts Table

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `ContactId` | INTEGER | PRIMARY KEY, AUTOINCREMENT | Unique ID |
| `FirstName` | TEXT | NOT NULL | Contact's first name |
| `LastName` | TEXT | NOT NULL, DEFAULT '' | Contact's last name |
| `Email` | TEXT | NOT NULL, DEFAULT '' | Email address |
| `PhoneNumber` | TEXT | NOT NULL | 10-digit phone number |
| `Address` | TEXT | NOT NULL, DEFAULT '' | Physical address |
| `WebAddress` | TEXT | NOT NULL, DEFAULT '' | Website URL |
| `Notes` | TEXT | NOT NULL, DEFAULT '' | Additional notes |
| `Category` | TEXT | NOT NULL, DEFAULT 'General' | Group/category |
| `IsFavorite` | INTEGER | NOT NULL, DEFAULT 0 | 1 = favorite, 0 = normal |

### ActivityLog Table

| Column | Type | Description |
|--------|------|-------------|
| `LogId` | INTEGER | PRIMARY KEY, AUTOINCREMENT |
| `Action` | TEXT | ADD, UPDATE, DELETE, FAVORITE, UNFAVORITE |
| `ContactName` | TEXT | Name of the affected contact |
| `Details` | TEXT | Additional details (phone, group) |
| `Timestamp` | TEXT | Date/time of the action (yyyy-MM-dd HH:mm:ss) |

---

## 🧪 How to Test

| Test Case | Steps | Expected Result |
|-----------|-------|-----------------|
| Add contact | Fill name + phone → Click Add | Contact appears in list |
| Validation | Leave name empty → Click Add | Warning: "First Name is required!" |
| Phone validation | Enter 5 digits → Click Add | Warning: "Must be 10 digits!" |
| Phone digits only | Type "abc" in phone field | Characters are blocked |
| Delete confirmation | Select contact → Click Delete | Dialog shows contact name |
| Favorite | Select contact → Click ⭐ Fav | Contact moves to top with ⭐ |
| Duplicate check | Add contact with existing phone | Warning: "Duplicate found!" |
| Search | Type "Amit" in search box | List filters to matching contacts |
| Activity log | Click Activity Log button | Shows all actions with timestamps |

---

## 🧑‍💻 Author
- **Adavayya** —  8th Semester, C# and .NET Course
- **Karthik k** — 8th Semester, C# and .NET Course
- **Vijayalakshmi** — 8th Semester, C# and .NET Course
 - **Antigravity Tool** — the real MVP. We just sat there looking important while it did 90% of the coding 😄

Honestly, it didn’t “assist” the workflow… it *was* the workflow 🚀

If everything runs perfectly, all credit goes to Antigravity.
If something breaks… suddenly it’s “team effort” and “learning experience” 😅

---

## 📄 License

This project is for educational purposes as part of university coursework.
