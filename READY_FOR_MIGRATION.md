# ? Pre-Migration Checklist

## System Requirements
- [x] Visual Studio 2022+ installed
- [x] SQL Server 2019+ installed and running
- [x] .NET 9 SDK installed
- [x] Your project builds successfully ?

## Configuration Verified
- [x] Connection String in `appsettings.json`: `Server=localhost;Database=FinanceTrackerDb;User Id=sa;Password=btapphym91;`
- [x] DbContext configured in `Program.cs`: `builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(conn));`
- [x] All required NuGet packages installed:
  - ? Microsoft.EntityFrameworkCore 9.0.0
  - ? Microsoft.EntityFrameworkCore.SqlServer 9.0.0
  - ? Microsoft.EntityFrameworkCore.Tools 9.0.0
- [x] Migrations folder created: `FinanceTracker.Infrastructure\Migrations\`
- [x] Startup Project set to: `FinanceTracker.API`

## Entities Ready
- [x] `BaseEntity` (with Id, CreatedAt, ModifiedAt, IsDeleted)
- [x] `Expense` entity with properties
- [x] `Category` entity with properties
- [x] `Account` entity with properties
- [x] DbContext with DbSets for all entities

---

## NOW READY TO RUN MIGRATIONS!

### ?? Before You Start:
1. **Make sure SQL Server is RUNNING**
 - Windows Service: "SQL Server (SQLEXPRESS)" or your instance name
   
2. **Set FinanceTracker.API as Startup Project**
   - In Visual Studio, right-click `FinanceTracker.API` ? Set as Startup Project
   - It should appear **bold** in Solution Explorer

3. **Open Package Manager Console**
   - Tools ? NuGet Package Manager ? Package Manager Console

---

## COMMAND #1: Add Migration

**Copy and paste into Package Manager Console:**

```powershell
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
```

**Press ENTER and wait...**

### Expected Output:
```
Build succeeded
Build time: 0.523s
```

### What Gets Created:
- `FinanceTracker.Infrastructure\Migrations\[TIMESTAMP]_InitialCreate.cs`
- `FinanceTracker.Infrastructure\Migrations\AppDbContextModelSnapshot.cs`

---

## COMMAND #2: Update Database

**After Command #1 completes, copy and paste this:**

```powershell
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```

**Press ENTER and wait...**

### Expected Output:
```
Build succeeded
Build time: 0.234s
Applying migration '20240119123456_InitialCreate'.
Done.
```

### What Gets Created:
- FinanceTrackerDb database
- Accounts table
- Categories table
- Expenses table
- __EFMigrationsHistory table (EF Core tracking table)

---

## VERIFICATION: Check SQL Server

### Using SQL Server Management Studio (SSMS):

1. **Open SSMS** on your computer
2. **Server name:** `localhost` or `(local)` or `.\`
3. **Authentication:** Windows Authentication (or SQL Server if configured)
4. **Connect**
5. **Expand:** Databases ? Look for **FinanceTrackerDb**
6. **Expand:** FinanceTrackerDb ? Tables
   - Should see: `dbo.Accounts`, `dbo.Categories`, `dbo.Expenses`

### Or Run SQL Query:

```sql
-- Check if database exists
SELECT name FROM sys.databases WHERE name = 'FinanceTrackerDb';

-- Check if tables exist
USE FinanceTrackerDb;
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo';

-- Check migration history
SELECT * FROM dbo.__EFMigrationsHistory;
```

---

## After Successful Migration

? Your database is ready!

**When you run the API:**
1. Program.cs will check for pending migrations
2. `db.Database.Migrate();` will apply any new migrations
3. `DbSeeder.SeedAsync(db);` will add sample data:
   - 3 Categories: Groceries, Transport, Utilities
   - 2 Accounts: Cash Wallet, Bank - Checking
   - 3 Expenses with sample data

---

## Troubleshooting

### ? Error: "Design-time factory not found"
**Fix:** Right-click `FinanceTracker.API` ? Set as Startup Project

### ? Error: "Cannot open server 'localhost'"
**Fix:** Start SQL Server from Windows Services or SQL Server Configuration Manager

### ? Error: "Login failed for user 'sa'"
**Fix:** Check password in `appsettings.json` matches your SQL Server SA account

### ? Error: "Access denied"
**Fix:** Run Visual Studio as Administrator

### ? Error: "Migration already applied"
**Fix:** ? This is SUCCESS! Means it already ran. Check SSMS to verify tables exist.

---

## Contact Points

**If something goes wrong:**

1. **Check the Output window:** View ? Output
2. **Read the error message carefully** - it usually tells you exactly what's wrong
3. **Google the error code** - most EF Core errors are well-documented
4. **Verify:**
   - SQL Server is running
   - Connection string is correct
   - SA password matches
   - SQL Server allows remote connections (TrustServerCertificate=True helps)

---

## You're All Set! ??

Just run those two commands in Package Manager Console and your database will be fully created with all tables!

