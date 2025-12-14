# ? FINAL PRE-MIGRATION CHECKLIST

## System Preparation

- [ ] **SQL Server is Running**
  - Press: Win + R
  - Type: `services.msc`
  - Find: SQL Server (SQLEXPRESS)
  - Status: **Running** (green checkmark)
  
- [ ] **Visual Studio is Open**
  - With your FinanceTracker solution loaded
  
- [ ] **FinanceTracker.API is Startup Project**
  - Solution Explorer ? Right-click FinanceTracker.API
  - Select: Set as Startup Project
  - Verify: Name appears **bold** in Solution Explorer

---

## Project Configuration Verified

- [ ] **Solution Builds Successfully**
  - Build ? Build Solution
  - Result: ? Build succeeded

- [ ] **NuGet Packages Installed**
  - Microsoft.EntityFrameworkCore 9.0.0 ?
  - Microsoft.EntityFrameworkCore.SqlServer 9.0.0 ?
  - Microsoft.EntityFrameworkCore.Tools 9.0.0 ?

- [ ] **DbContext Configured**
  - File: FinanceTracker.Infrastructure\Persistence\AppDbContext.cs ?
  - Has 3 DbSets: Expenses, Categories, Accounts ?

- [ ] **Startup Configuration Set**
  - File: FinanceTracker.API\Program.cs ?
  - DbContext registered ?
  - Connection string loaded ?

- [ ] **Connection String Valid**
  - File: FinanceTracker.API\appsettings.json ?
  - Server: localhost ?
  - Database: FinanceTrackerDb ?
  - User: sa ?
  - Password: Correct ?

---

## Entities Are Ready

- [ ] **BaseEntity**
  - Location: FinanceTracker.Domain\Entities\BaseEntity.cs ?
  - Properties: Id, CreatedAt, ModifiedAt, IsDeleted ?

- [ ] **Expense Entity**
  - Location: FinanceTracker.Domain\Entities\Expense.cs ?
  - Properties: Title, Amount, Date, CategoryId, AccountId, Notes ?

- [ ] **Category Entity**
  - Location: FinanceTracker.Domain\Entities\Category.cs ?
  - Properties: Name ?

- [ ] **Account Entity**
  - Location: FinanceTracker.Domain\Entities\Account.cs ?
  - Properties: Name, Balance ?

---

## Migrations Folder

- [ ] **Migrations Folder Exists**
  - Path: FinanceTracker.Infrastructure\Migrations\ ?
  - Contains: Only .gitkeep file ?

---

## SQL Server Access

- [ ] **Can Connect to SQL Server**
  - Hostname: localhost ?
  - Port: 1433 (default) ?
  - SA Account: Accessible ?
  - Password: Correct ?

---

## Documentation

- [ ] **Read Quick Reference**
  - File: QUICK_MIGRATION_REFERENCE.md ?

- [ ] **Understand Two Commands**
  - Add-Migration ?
  - Update-Database ?

---

## NOW READY TO PROCEED

If all checkboxes above are checked ?, you're ready!

---

## STEP-BY-STEP EXECUTION

### 1. Open Package Manager Console

- In Visual Studio
- Menu: Tools ? NuGet Package Manager ? Package Manager Console
- PowerShell window appears at bottom

---

### 2. Run Command #1 (Add Migration)

**Copy this entire command:**
```powershell
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
```

**In Package Manager Console:**
1. Right-click ? Paste
2. Press ENTER
3. Wait for build to complete
4. Look for: ? `Build succeeded`
5. Check: Migration files created in Migrations folder

---

### 3. Run Command #2 (Update Database)

**Copy this entire command:**
```powershell
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```

**In Package Manager Console:**
1. Right-click ? Paste
2. Press ENTER
3. Wait for migration to apply
4. Look for: ? `Done.`
5. Check: Database created in SQL Server

---

### 4. Verify in SQL Server

**Open SQL Server Management Studio:**
1. Start ? SQL Server Management Studio
2. Server name: localhost
3. Authentication: Windows Authentication
4. Connect

**Check Database:**
1. Expand: Databases
2. Find: FinanceTrackerDb ?
3. Expand: FinanceTrackerDb
4. Expand: Tables
5. Verify:
   - dbo.Accounts ?
 - dbo.Categories ?
   - dbo.Expenses ?
   - dbo.__EFMigrationsHistory ?

---

## SUCCESS INDICATORS

After all commands complete, you should see:

? **In Visual Studio:**
- New files in: FinanceTracker.Infrastructure\Migrations\
  - `[TIMESTAMP]_InitialCreate.cs`
  - `AppDbContextModelSnapshot.cs`

? **In Package Manager Console:**
```
Build succeeded
Applying migration '20240119143256_InitialCreate'.
Done.
```

? **In SQL Server:**
- Database: FinanceTrackerDb (exists)
- Tables: Accounts, Categories, Expenses (all exist and empty)
- Migration History: Shows InitialCreate

---

## IF SOMETHING GOES WRONG

### ? Build Failed
- Check error message in Package Manager Console
- See: DETAILED_STEP_BY_STEP_GUIDE.md ? Troubleshooting

### ? SQL Server Not Found
- Verify SQL Server is running (Windows Services)
- Check hostname in connection string

### ? Login Failed
- Verify SA password in appsettings.json
- Try connecting manually in SSMS first

### ? Migration History Error
- Database might already have migrations
- Check if tables already exist
- May need to clean up and start fresh

---

## RECOVERY: If You Need to Start Over

**Remove the migration files:**
1. Delete files from: FinanceTracker.Infrastructure\Migrations\
2. Keep only: .gitkeep file

**In SQL Server:**
1. If database exists and is empty: Can just recreate
2. If database has data: Backup first, then drop and recreate

**Then start fresh:**
1. Run Add-Migration again
2. Run Update-Database again

---

## FINAL CHECKLIST BEFORE RUNNING

- [ ] SQL Server running (Windows Services shows "Running")
- [ ] Visual Studio open with solution loaded
- [ ] FinanceTracker.API set as Startup Project (bold in Solution Explorer)
- [ ] Solution builds successfully (Build ? Build Solution ? Succeeded)
- [ ] appsettings.json has correct connection string
- [ ] SA password is correct
- [ ] Package Manager Console ready

? **ALL CHECKED? ? Execute the commands!**

---

## TIME ESTIMATE

| Step | Time |
|------|------|
| Open Package Manager Console | 10 sec |
| Run Add-Migration | 5-10 sec |
| Run Update-Database | 10-20 sec |
| Verify in SSMS | 5-10 sec |
| **TOTAL** | **30-50 seconds** |

---

## SUCCESS MESSAGE

When everything works, you'll see:

**Package Manager Console:**
```
PM> Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
Build succeeded
To undo this action, use Remove-Migration.

PM> Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
Build succeeded
Applying migration '20240119143256_InitialCreate'.
Done.
PM> 
```

**SQL Server Management Studio:**
- FinanceTrackerDb exists with tables: Accounts, Categories, Expenses

---

## NEXT STEPS AFTER SUCCESS

1. **Start the API** (F5 in Visual Studio)
2. **Open Swagger** (https://localhost:5001/swagger)
3. **Test endpoints** (Create an expense)
4. **Check database** (Query tables in SSMS)
5. **Verify seeded data** (Should have 3 sample expenses)

---

## COPY-PASTE COMMANDS

### Ready to copy for Command #1:
```
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
```

### Ready to copy for Command #2:
```
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```

---

## YOU'RE READY! ??

All systems are go. Execute the commands and your database will be created!

**Questions?** See the detailed guides:
- QUICK_MIGRATION_REFERENCE.md
- DETAILED_STEP_BY_STEP_GUIDE.md
- PMC_MIGRATION_STEPS.md
