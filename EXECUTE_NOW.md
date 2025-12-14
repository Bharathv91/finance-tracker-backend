# ?? MIGRATION EXECUTION QUICK GUIDE

## YOUR SITUATION

? **Project Status:** Ready for migrations
? **Build Status:** Successful
? **Configuration:** Complete
? **All Systems:** Go!

---

## ONLY 2 COMMANDS NEEDED

### Command #1: Create Migration File

**What to type in Package Manager Console:**
```powershell
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
```

**What happens:**
- EF Core analyzes your DbContext
- Creates migration file: `[TIMESTAMP]_InitialCreate.cs`
- Creates snapshot file: `AppDbContextModelSnapshot.cs`

**Expected output:**
```
Build succeeded
To undo this action, use Remove-Migration.
```

**Time:** ~10 seconds

---

### Command #2: Create Database & Tables

**What to type in Package Manager Console:**
```powershell
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```

**What happens:**
- Creates database: `FinanceTrackerDb`
- Creates tables: Accounts, Categories, Expenses
- Records migration in: `__EFMigrationsHistory`

**Expected output:**
```
Build succeeded
Applying migration '20240119123456_InitialCreate'.
Done.
```

**Time:** ~15 seconds

---

## HOW TO OPEN PACKAGE MANAGER CONSOLE

**In Visual Studio:**
1. Click: **Tools** menu
2. Hover: **NuGet Package Manager**
3. Click: **Package Manager Console**

**Result:** PowerShell window appears at bottom of Visual Studio

---

## STEP-BY-STEP EXECUTION

### Step 1: Open Console
- Tools ? NuGet Package Manager ? Package Manager Console ?

### Step 2: Copy Command #1
- Highlight: `Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations`
- Right-click ? Copy

### Step 3: Paste in Console
- Click in Package Manager Console
- Right-click ? Paste
- Press ENTER

### Step 4: Wait & Verify
- Wait for: "Build succeeded"
- Check: New files in `FinanceTracker.Infrastructure\Migrations\`

### Step 5: Copy Command #2
- Highlight: `Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API`
- Right-click ? Copy

### Step 6: Paste in Console
- Right-click ? Paste
- Press ENTER

### Step 7: Wait & Verify
- Wait for: "Done."
- Check: Database created in SQL Server

### Step 8: Final Verification
- Open SQL Server Management Studio
- Connect to: localhost
- See: FinanceTrackerDb with tables

---

## WHAT GETS CREATED

### Folders & Files
```
FinanceTracker.Infrastructure/
??? Migrations/
  ??? [TIMESTAMP]_InitialCreate.cs    ? The migration
    ??? AppDbContextModelSnapshot.cs    ? Schema snapshot
```

### Database & Tables
```
FinanceTrackerDb/
??? Accounts table
??? Categories table
??? Expenses table
??? __EFMigrationsHistory table
```

---

## VERIFICATION: How to Check Success

### Method 1: In Visual Studio
1. Expand: Solution Explorer
2. Go to: FinanceTracker.Infrastructure
3. Expand: Migrations folder
4. Should see 2 new files ?

### Method 2: In SQL Server Management Studio
1. Open SSMS
2. Connect to: localhost
3. Expand: Databases
4. See: FinanceTrackerDb
5. Expand ? Tables
6. See: Accounts, Categories, Expenses ?

### Method 3: Run SQL Query
```sql
SELECT TABLE_NAME FROM FinanceTrackerDb.INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = 'dbo'
```

Should return:
- Accounts
- Categories
- Expenses
- __EFMigrationsHistory

---

## COPY-PASTE READY COMMANDS

### Command #1 (Copy exactly):
```
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
```

### Command #2 (Copy exactly):
```
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```

---

## IMPORTANT REQUIREMENTS

? **Before you run:**
- [ ] SQL Server is running
- [ ] Visual Studio is open
- [ ] FinanceTracker.API is Startup Project (bold in Solution Explorer)
- [ ] Build succeeded
- [ ] You can connect to localhost:1433

? **All checked?** ? Ready to execute!

---

## TROUBLESHOOTING

### ? "Cannot open server 'localhost'"
**Fix:** SQL Server not running ? Start it from Windows Services

### ? "Login failed for user 'sa'"
**Fix:** Check password in appsettings.json matches your SQL Server SA account

### ? "Design-time factory not found"
**Fix:** FinanceTracker.API is not Startup Project ? Right-click it and "Set as Startup Project"

### ? "Build failed"
**Fix:** Read error message ? Usually syntax error in an entity file

### ? "Migration already applied"
**Fix:** Good news! ? Means it already ran ? Check SSMS for tables

---

## TIMELINE

```
You run Command #1
    ?
(5-10 seconds)
    ?
"Build succeeded" appears
    ?
You run Command #2
    ?
(10-20 seconds)
    ?
"Done." appears
    ?
YOU'RE DONE! ?
```

**Total time: 30-40 seconds**

---

## AFTER SUCCESSFUL MIGRATION

Your database is ready! Next:

1. **Run the API**
   - Press F5 or Debug ? Start Debugging
   
2. **Test with Swagger**
 - Open: https://localhost:5001/swagger
   
3. **Try endpoints**
   - Create an expense
   - List expenses
   - Update an expense
   
4. **Check database**
   - Open SSMS
   - Query: SELECT * FROM FinanceTrackerDb.dbo.Expenses;
   - Should see seeded data

---

## ONE MORE THING

?? **After migration succeeds:**
- Commit migration files to Git
- Never commit database itself
- Share migration files with team

```bash
git add FinanceTracker.Infrastructure/Migrations/
git commit -m "Add: InitialCreate migration"
git push
```

---

## READY? 

Just follow these steps:

1. Open Package Manager Console
2. Copy-paste Command #1 ? Press ENTER
3. Wait for success
4. Copy-paste Command #2 ? Press ENTER
5. Wait for success
6. Verify in SSMS

That's it! Your database will be created in ~40 seconds! ??

---

## QUICK LINKS TO GUIDES

- **Quick Reference:** QUICK_MIGRATION_REFERENCE.md
- **Detailed Steps:** DETAILED_STEP_BY_STEP_GUIDE.md
- **Full Guide:** PMC_MIGRATION_STEPS.md
- **Checklist:** FINAL_CHECKLIST.md
- **Summary:** MIGRATION_SUMMARY.md

---

**GO FORTH AND MIGRATE! ??**
