# ?? QUICK START: Migration Commands

## You are HERE ? Ready to Run Migrations ?

---

## TWO COMMANDS TO RUN (Copy & Paste into Package Manager Console)

### 1?? ADD MIGRATION (Creates migration files)
```powershell
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
```

**After pressing ENTER:**
- Wait for build to complete
- ? You'll see: `Build succeeded`
- Check: New files appear in `FinanceTracker.Infrastructure\Migrations\`

---

### 2?? UPDATE DATABASE (Creates tables in SQL Server)
```powershell
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```

**After pressing ENTER:**
- Wait for migration to apply
- ? You'll see: `Applying migration '20240119..._InitialCreate'. Done.`
- Check: Open SQL Server Management Studio ? FinanceTrackerDb should exist with tables

---

## HOW TO OPEN PACKAGE MANAGER CONSOLE

**Visual Studio Menu:** Tools ? NuGet Package Manager ? Package Manager Console

PowerShell window appears at bottom of Visual Studio.

---

## WHAT GETS CREATED

### In Your Project:
```
FinanceTracker.Infrastructure\
??? Migrations\
    ??? [TIMESTAMP]_InitialCreate.cs (the migration)
    ??? AppDbContextModelSnapshot.cs (schema snapshot)
```

### In SQL Server:
```
FinanceTrackerDb (new database)
??? dbo.Accounts (table)
??? dbo.Categories (table)
??? dbo.Expenses (table)
??? dbo.__EFMigrationsHistory (EF Core tracking)
```

---

## TABLES SCHEMA THAT WILL BE CREATED

### Accounts Table
```sql
CREATE TABLE dbo.Accounts (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Balance DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    ModifiedAt DATETIME2 NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
)
```

### Categories Table
```sql
CREATE TABLE dbo.Categories (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    ModifiedAt DATETIME2 NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
)
```

### Expenses Table
```sql
CREATE TABLE dbo.Expenses (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Date DATETIME2 NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CategoryId UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES Categories(Id),
 AccountId UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES Accounts(Id),
    CreatedAt DATETIME2 NOT NULL,
    ModifiedAt DATETIME2 NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
)
```

---

## VERIFY IN SQL SERVER (After Update-Database)

**Open SQL Server Management Studio:**
1. Connect to: `localhost` (or `(local)`)
2. Expand: Databases ? FinanceTrackerDb
3. Expand: Tables
4. Should see: Accounts, Categories, Expenses ?

---

## WHAT HAPPENS WHEN YOU RUN YOUR API

After migrations are applied, when you start the API:

1. **Program.cs** runs `db.Database.Migrate();`
   - Checks for pending migrations
   - Applies any new migrations automatically

2. **DbSeeder.SeedAsync()** runs
   - Adds 3 sample Categories (Groceries, Transport, Utilities)
   - Adds 2 sample Accounts (Cash Wallet, Bank - Checking)
   - Adds 3 sample Expenses with real data

---

## ?? BEFORE YOU START

- [ ] SQL Server is running
- [ ] Visual Studio solution is open
- [ ] FinanceTracker.API is set as Startup Project
- [ ] Build succeeded
- [ ] You know your SA password for SQL Server

---

## ?? EXPECTED TIMELINE

| Step | Time | What Happens |
|------|------|--------------|
| Add Migration | 5-10 sec | Migration files generated |
| Update Database | 10-20 sec | Tables created in SQL Server |
| Verify | 2-5 sec | Open SSMS and see tables |
| **TOTAL** | **~30 seconds** | **Database is ready!** ? |

---

## COPY-PASTE COMMANDS BELOW

### Command 1 (Run first):
```
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
```

### Command 2 (Run second):
```
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```

---

## ?? Done!

Your database is now ready. You can:
- ? Run the API
- ? Call the /api/expenses endpoints
- ? Create, read, update, delete expenses
- ? See tables in SQL Server

**Happy coding! ??**
