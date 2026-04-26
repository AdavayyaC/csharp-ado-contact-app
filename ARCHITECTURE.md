# 🏗️ Architecture & High-Level Design

## Contact Manager — ADO.NET WinForms Application

---

## 1. System Overview

The Contact Manager is a desktop application built using the **Windows Forms** UI framework with **ADO.NET** for database access. It follows a **layered architecture** with clear separation between UI, Business Logic, and Data Access layers.

```
┌─────────────────────────────────────────────────────────┐
│                    PRESENTATION LAYER                    │
│                  (Windows Forms UI)                      │
│                                                         │
│   FormBase.cs          FormActivityLog.cs               │
│   ├── FormConnected    (Activity Log Viewer)            │
│   └── FormDisconnected                                  │
├─────────────────────────────────────────────────────────┤
│                   BUSINESS LOGIC LAYER                   │
│              (Validation, Search, Logging)               │
│                                                         │
│   • Input Validation    • Duplicate Detection           │
│   • Search/Filtering    • Favorite Management           │
│   • Activity Logging    • Contact Sorting               │
├─────────────────────────────────────────────────────────┤
│                   DATA ACCESS LAYER                      │
│                  (ADO.NET + SQLite)                      │
│                                                         │
│   Connected Mode         Disconnected Mode              │
│   ├── SqliteConnection   ├── DataTable (in-memory)      │
│   ├── SqliteCommand      ├── DataRow operations         │
│   └── SqliteDataReader   └── Batch sync on close        │
├─────────────────────────────────────────────────────────┤
│                    DATABASE LAYER                        │
│                   (SQLite File DB)                       │
│                                                         │
│   ContactDB.db                                          │
│   ├── Contacts table                                    │
│   └── ActivityLog table                                 │
└─────────────────────────────────────────────────────────┘
```

---

## 2. Design Patterns Used

### 2.1 Template Method Pattern (Core Pattern)

The most important design pattern in this project. `FormBase` defines the **skeleton** of the algorithm, and child classes override specific steps.

```
        ┌─────────────────┐
        │    FormBase      │  ← Abstract base class
        │  (Template)      │
        ├─────────────────┤
        │ + LoadData()     │  ← virtual methods
        │ + AddContact()   │     (define the skeleton)
        │ + UpdateContact()│
        │ + DeleteContact()│
        │ + ToggleFav()    │
        │ + CheckDuplicate│
        └───────┬─────────┘
                │ inherits
        ┌───────┴────────────────────┐
        │                            │
┌───────┴────────┐    ┌──────────────┴───┐
│ FormConnected   │    │ FormDisconnected  │
│ (Live queries)  │    │ (In-memory ops)   │
├────────────────┤    ├──────────────────┤
│ Override:       │    │ Override:         │
│ LoadData() →    │    │ LoadData() →      │
│   SELECT * ...  │    │   DataTable.Load()│
│ AddContact() →  │    │ AddContact() →    │
│   INSERT INTO.. │    │   DataTable.Add() │
│ UpdateContact()→│    │ UpdateContact() → │
│   UPDATE SET .. │    │   DataRow[]=..    │
│ DeleteContact()→│    │ DeleteContact() → │
│   DELETE FROM.. │    │   DataRow.Delete()│
└────────────────┘    └──────────────────┘
```

**Why this pattern?**
- Shared UI and validation logic in `FormBase` (written once)
- Database-specific logic is isolated in child classes
- Easy to add a third mode (e.g., REST API mode) without modifying `FormBase`

### 2.2 POCO Pattern (Plain Old CLR Object)

`Contact.cs` is a simple data container with no framework dependencies.

```csharp
public class Contact
{
    public int contactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    // ... properties only, no methods or DB logic
}
```

### 2.3 Separation of Concerns

| Layer | Responsibility | Files |
|-------|---------------|-------|
| **Model** | Data structure only | `Contact.cs` |
| **UI + Logic** | Form layout, events, validation | `FormBase.cs`, `FormBase.Designer.cs` |
| **Data Access** | SQL queries, DB operations | `FormConnected.cs`, `FormDisconnected.cs` |
| **Config** | Connection settings | `App.config` |

---

## 3. ADO.NET Architecture Deep Dive

### 3.1 Connected Architecture

```
     User clicks "Add"
            │
            ▼
    ┌──────────────┐
    │ ValidateInput │ ← Check required fields, 10-digit phone
    └──────┬───────┘
           │ valid
           ▼
    ┌──────────────────┐
    │ CheckDuplicate()  │ ← SELECT COUNT(*) WHERE name/phone match
    └──────┬───────────┘
           │ no duplicate (or user forces)
           ▼
    ┌──────────────────┐
    │ Open Connection   │ ← new SqliteConnection(connStr)
    │ connection.Open() │
    └──────┬───────────┘
           │
           ▼
    ┌──────────────────┐
    │ Create Command    │ ← INSERT INTO Contacts VALUES(...)
    │ Add Parameters    │ ← @FirstName, @LastName, ... (prevents SQL injection)
    └──────┬───────────┘
           │
           ▼
    ┌──────────────────┐
    │ ExecuteNonQuery() │ ← Sends SQL to database, returns rows affected
    └──────┬───────────┘
           │
           ▼
    ┌──────────────────┐
    │ Close Connection  │ ← connection.Close() inside using() block
    └──────┬───────────┘
           │
           ▼
    ┌──────────────────┐
    │ LogActivity()     │ ← INSERT INTO ActivityLog (action, name, timestamp)
    └──────┬───────────┘
           │
           ▼
    ┌──────────────────┐
    │ Reload & Display  │ ← Re-query all contacts, refresh ListBox
    └──────────────────┘
```

**Key ADO.NET Objects:**

| Object | Role | Example |
|--------|------|---------|
| `SqliteConnection` | Manages connection to SQLite DB | `new SqliteConnection("Data Source=ContactDB.db")` |
| `SqliteCommand` | Holds SQL statement to execute | `cmd.CommandText = "SELECT * FROM Contacts"` |
| `SqliteDataReader` | Reads query results row-by-row (forward-only) | `while (reader.Read()) { ... }` |
| `SqliteParameter` | Parameterized values (prevents SQL injection) | `cmd.Parameters.AddWithValue("@Name", value)` |

### 3.2 Disconnected Architecture

```
     App Starts
         │
         ▼
  ┌──────────────────┐
  │ Open Connection   │ ← One-time connection
  │ SELECT * FROM ... │
  └──────┬───────────┘
         │
         ▼
  ┌──────────────────┐
  │ DataTable.Load()  │ ← ALL rows loaded into memory
  │ (in-memory copy)  │
  └──────┬───────────┘
         │
         ▼
  ┌──────────────────────────────────────────────┐
  │            IN-MEMORY OPERATIONS               │
  │                                                │
  │  Add    → dataTable.Rows.Add(newRow)          │
  │  Update → dataRow["FirstName"] = "New Value"  │
  │  Delete → dataRow.Delete()                    │
  │  Search → dataTable.Select("filter")          │
  │                                                │
  │  ⚡ No database calls during these operations! │
  └──────────────────────┬─────────────────────────┘
                         │
                   App Closing
                         │
                         ▼
  ┌──────────────────────────────────┐
  │ OnClosing() — Sync ALL changes   │
  │                                  │
  │ For each DataRow:                │
  │   Added    → INSERT INTO ...     │
  │   Modified → UPDATE SET ...      │
  │   Deleted  → DELETE FROM ...     │
  └──────────────────────────────────┘
```

**Key ADO.NET Objects:**

| Object | Role |
|--------|------|
| `DataTable` | In-memory representation of a database table |
| `DataRow` | Single row in a DataTable, tracks its own state (Added/Modified/Deleted) |
| `DataRowState` | Enum that tells you what changed (so you know what SQL to run) |

---

## 4. Class Diagram

```
┌────────────────────────────────────────────────────────────┐
│                         Contact                            │
│                     (Model/Contact.cs)                      │
├────────────────────────────────────────────────────────────┤
│ + contactId : int                                          │
│ + FirstName : string                                       │
│ + LastName : string                                        │
│ + Email : string                                           │
│ + PhoneNumber : string                                     │
│ + Address : string                                         │
│ + WebAddress : string                                      │
│ + Notes : string                                           │
│ + Category : string        ← NEW: Contact group            │
│ + IsFavorite : bool        ← NEW: Favorite flag            │
│ + FullName : string [get]  ← Computed: "⭐ Name [Group]"   │
└────────────────────────────────────────────────────────────┘

┌────────────────────────────────────────────────────────────┐
│                        FormBase                             │
│                    (UI/FormBase.cs)                          │
├────────────────────────────────────────────────────────────┤
│ # contactList : List<Contact>                              │
│ # connectionString : string                                │
│ + STATUS : string [set]                                    │
├────────────────────────────────────────────────────────────┤
│ - FormBase_Load()           ← Init DB + load data          │
│ - ShowWelcomeMessage()      ← Startup splash               │
│ - EnsureDatabaseExists()    ← Create tables if missing     │
│ - CheckConnection()         ← Test DB connectivity         │
│ # LoadData() [virtual]      ← Override in child classes     │
│ # GetContactById() [virtual]                                │
│ # AddContact() [virtual]                                    │
│ # UpdateContact() [virtual]                                 │
│ # DeleteContact() [virtual]                                 │
│ # ToggleFavorite() [virtual]                                │
│ # CheckDuplicate() [virtual]                                │
│ # LogActivity()             ← Write to ActivityLog table    │
│ # FillContactList()         ← Bind list to ListBox          │
│ - ValidateInput()           ← Name + 10-digit phone check   │
│ - textBoxSearch_TextChanged ← Real-time search filter        │
│ - buttonNew_Click           ← Validate → Duplicate → Add    │
│ - buttonDelete_Click        ← Confirm with name → Delete    │
│ - buttonFavorite_Click      ← Toggle ⭐ status               │
│ - buttonViewLogs_Click      ← Open FormActivityLog           │
└──────────────┬─────────────────────────┬───────────────────┘
               │                         │
      ┌────────┴────────┐      ┌─────────┴────────┐
      │ FormConnected    │      │ FormDisconnected  │
      │ (Connected Mode) │      │ (Disconnected)    │
      ├─────────────────┤      ├──────────────────┤
      │ Overrides:       │      │ Overrides:        │
      │ LoadData()       │      │ LoadData()        │
      │ GetContactById() │      │ GetContactById()  │
      │ AddContact()     │      │ AddContact()      │
      │ UpdateContact()  │      │ UpdateContact()   │
      │ DeleteContact()  │      │ DeleteContact()   │
      │ ToggleFavorite() │      │ ToggleFavorite()  │
      │ CheckDuplicate() │      │ CheckDuplicate()  │
      └─────────────────┘      │ OnClosing()       │
                                │ ← Sync to DB      │
                                └──────────────────┘

┌────────────────────────────────────────────────────────────┐
│                     FormActivityLog                         │
│                 (UI/FormActivityLog.cs)                      │
├────────────────────────────────────────────────────────────┤
│ - dataGridView : DataGridView                              │
├────────────────────────────────────────────────────────────┤
│ - LoadLogs()        ← SELECT from ActivityLog + stats       │
│ - ClearLogs()       ← DELETE all logs with confirmation     │
│ Color coding:  🟢ADD  🔵UPDATE  🔴DELETE  🟡FAVORITE       │
└────────────────────────────────────────────────────────────┘
```

---

## 5. Data Flow Diagrams

### 5.1 Add Contact Flow

```
  User                    FormBase                FormConnected           SQLite DB
   │                         │                         │                     │
   │─── Fill form fields ───▶│                         │                     │
   │─── Click "Add" ───────▶│                         │                     │
   │                         │── ValidateInput() ─────▶│                     │
   │                         │   (name + phone check)  │                     │
   │                         │                         │                     │
   │                         │── CheckDuplicate() ────▶│── SELECT COUNT ───▶│
   │                         │                         │◀── count result ───│
   │                         │                         │                     │
   │◀── Duplicate warning ──│  (if found)             │                     │
   │─── "Yes, add anyway" ─▶│                         │                     │
   │                         │                         │                     │
   │                         │── AddContact() ────────▶│── INSERT INTO ────▶│
   │                         │                         │◀── success ────────│
   │                         │                         │                     │
   │                         │── LogActivity("ADD") ──▶│── INSERT log ─────▶│
   │                         │                         │                     │
   │                         │── LoadData() ──────────▶│── SELECT * ───────▶│
   │                         │                         │◀── all contacts ───│
   │                         │── FillContactList() ───▶│                     │
   │◀── Updated list ───────│                         │                     │
```

### 5.2 Search Flow

```
  User types "Ami"          FormBase                   In-Memory List
   │                          │                             │
   │─── KeyPress event ──────▶│                             │
   │                          │── Filter contactList ──────▶│
   │                          │   .Where(c =>               │
   │                          │     c.FullName.Contains("ami") │
   │                          │     || c.Phone.Contains("ami") │
   │                          │     || c.Email.Contains("ami") │
   │                          │     || c.Category.Contains("ami"))│
   │                          │◀── filtered list ───────────│
   │◀── Display matches ─────│                             │
   │    "⭐ Amit Patil [Work]"│                             │
```

---

## 6. Database ER Diagram

```
┌──────────────────────────┐         ┌──────────────────────────┐
│        Contacts           │         │       ActivityLog         │
├──────────────────────────┤         ├──────────────────────────┤
│ PK  ContactId  INTEGER   │         │ PK  LogId     INTEGER    │
│     FirstName  TEXT       │◄────────│     ContactName TEXT     │
│     LastName   TEXT       │  (logged│     Action     TEXT      │
│     Email      TEXT       │   name) │     Details    TEXT      │
│     PhoneNumber TEXT      │         │     Timestamp  TEXT      │
│     Address    TEXT       │         └──────────────────────────┘
│     WebAddress TEXT       │
│     Notes      TEXT       │
│     Category   TEXT       │  ← "Friend" | "Family" | "Work" | ...
│     IsFavorite INTEGER    │  ← 0 or 1
└──────────────────────────┘
```

---

## 7. Technology Stack

| Layer | Technology | Version |
|-------|-----------|---------|
| **Language** | C# | 12.0 |
| **Framework** | .NET | 8.0 |
| **UI Framework** | Windows Forms (WinForms) | .NET 8 |
| **Database** | SQLite | 3.x (embedded) |
| **ADO.NET Provider** | Microsoft.Data.Sqlite | 8.0.11 |
| **Configuration** | System.Configuration.ConfigurationManager | 8.0.0 |
| **IDE** | Visual Studio Code / Visual Studio | Any |

---

## 8. Security Considerations

| Threat | Mitigation |
|--------|-----------|
| **SQL Injection** | All queries use **parameterized parameters** (`@FirstName`, etc.) — never string concatenation |
| **Input Validation** | Required fields enforced, phone number restricted to digits only |
| **Data Loss** | Connected mode saves immediately; Delete requires name-based confirmation |
| **Audit Trail** | ActivityLog table records every change with timestamps |

---

## 9. Future Enhancements

| Enhancement | Description | Complexity |
|-------------|-------------|------------|
| Export to CSV/Excel | Save contacts to a file | Easy |
| Import from CSV | Bulk add contacts from file | Easy |
| Dark Mode | Toggle light/dark UI theme | Medium |
| Login System | Username/password authentication | Medium |
| Dashboard | Statistics with charts | Medium |
| Backup & Restore | Save/load database snapshots | Medium |
| REST API Backend | Replace SQLite with a web API | Advanced |

---

## 10. Key Learning Outcomes

By studying this project, you will understand:

1. **ADO.NET Connected Mode** — Live database queries with `Command` + `DataReader`
2. **ADO.NET Disconnected Mode** — In-memory `DataTable` with batch sync
3. **Parameterized Queries** — Preventing SQL injection attacks
4. **Template Method Pattern** — Shared logic in base class, specific logic in children
5. **WinForms Architecture** — Events, Controls, Designer files, multi-form apps
6. **SQLite** — Embedded database that requires no server setup
7. **Input Validation** — Real-time field validation in desktop apps
8. **Audit Logging** — Tracking all data changes with timestamps

---

*Document Version: 1.0 | Last Updated: April 2026*
