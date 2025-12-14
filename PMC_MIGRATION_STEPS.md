# Step-by-Step: Running Migrations via Package Manager Console

## STEP 1: Open Package Manager Console

1. In Visual Studio, go to: **Tools ? NuGet Package Manager ? Package Manager Console**
2. You should see a PowerShell window at the bottom of Visual Studio

---

## STEP 2: Add Migration (Create Migration File)

**Copy and paste this exact command into the Package Manager Console:**

```powershell
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
```

### What to expect:
- ? The command will run and complete in a few seconds
- ? You'll see output like: `Build succeeded`
- ? **New files will appear** in `FinanceTracker.Infrastructure\Migrations\` folder:
  - `[TIMESTAMP]_InitialCreate.cs` (the actual migration)
  - `AppDbContextModelSnapshot.cs` (schema snapshot)

### If you see errors:
- **"Design-time factory not found"** ? Make sure FinanceTracker.API is set as Startup Project
- **"Cannot find migration history table"** ? This is OK, it means first time, proceed to Step 3

---

## STEP 3: Update Database (Apply Migration)

**After the migration file is created, run this command:**

```powershell
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```

### What happens:
1. ? Connects to your SQL Server at `localhost`
2. ? Creates database `FinanceTrackerDb` (if not exists)
3. ? Creates all tables:
   - `Accounts`
   - `Categories`
   - `Expenses`
4. ? Records the migration in `__EFMigrationsHistory` table

### Expected output:
```
Build succeeded
Applying migration '20240119123456_InitialCreate'.
Done.
```

### If you see errors:
- **"Cannot open server 'localhost'"** ? SQL Server is not running, start it
- **"Login failed"** ? Check password in `appsettings.json`
- **"TrustServerCertificate"** ? Already set to True in your config ?

---

## STEP 4: Verify Database Creation

### Option A: Using SQL Server Management Studio (SSMS)

1. Open **SQL Server Management Studio**
2. Connect to: `localhost` (or `(local)` or `.\`)
3. Expand **Databases** ? You should see **FinanceTrackerDb**
4. Expand **FinanceTrackerDb ? Tables** ? You should see:
   - `dbo.Accounts`
   - `dbo.Categories`
   - `dbo.Expenses`
   - `dbo.__EFMigrationsHistory`

### Option B: Using PowerShell Command

Run this query to verify migration was applied:

```sql
SELECT * FROM FinanceTrackerDb.dbo.__EFMigrationsHistory;
```

You should see one row with migration name: `InitialCreate`

---

## Quick Reference

| Step | Command | Expected Result |
|------|---------|-----------------|
| 1 | Open Package Manager Console | PowerShell window appears |
| 2 | `Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations` | Migration files created in `Migrations\` folder |
| 3 | `Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API` | Tables created in database |
| 4 | Verify in SSMS | See `FinanceTrackerDb` with all tables |

---

## Troubleshooting Common Issues

### Issue: "The migration 'InitialCreate' has already been applied to the database."
**Solution:** Migration already applied ? Check SSMS to confirm tables exist

### Issue: "There is already an open transaction"
**Solution:** Close any other connections to the database and try again

### Issue: "Cannot open database 'FinanceTrackerDb'"
**Solution:** This is normal on first run. Make sure you have db_owner or db_ddladmin permissions

### Issue: "Timeout expired"
**Solution:** Your SQL Server might be busy. Wait a moment and try again

---

## Your Configuration Details

**Connection String** (from appsettings.json):
```
Server=localhost;Database=FinanceTrackerDb;User Id=sa;Password=btapphym91;TrustServerCertificate=True;
```

**DbContext Location:**
- `FinanceTracker.Infrastructure\Persistence\AppDbContext.cs`

**Entities (will create tables):**
- `Expense` ? `Expenses` table
- `Category` ? `Categories` table
- `Account` ? `Accounts` table
- `BaseEntity` properties ? `Id`, `CreatedAt`, `ModifiedAt`, `IsDeleted` columns

**Migration Output Folder:**
- `FinanceTracker.Infrastructure\Migrations\`

---

## After Migration is Complete

Your application in `Program.cs` will:
1. ? Apply any pending migrations automatically on startup
2. ? Seed sample data via `DbSeeder.SeedAsync()`
3. ? Everything ready to use!

---

## Important Notes

?? **Make sure:**
- [ ] Visual Studio is open with your solution
- [ ] FinanceTracker.API is set as the **Startup Project** (bold in Solution Explorer)
- [ ] SQL Server is running and accessible
- [ ] You have network access to `localhost:1433` (default SQL Server port)
- [ ] Build is successful before running migration commands

? **All checks passed? Ready to run the commands!**

---

## Copy-Paste Ready Commands

### For Add Migration:
```powershell
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
```

### For Update Database:
```powershell
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```
