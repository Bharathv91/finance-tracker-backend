# ?? STEP-BY-STEP VISUAL GUIDE

## STEP 1: Verify SQL Server is Running

### Windows 11/10:
1. Press `Win + R`
2. Type: `services.msc`
3. Look for: `SQL Server (SQLEXPRESS)` or `SQL Server ([YOUR_INSTANCE])`
4. Status should be: **Running** (green arrow icon)
5. If not running: Right-click ? Start

---

## STEP 2: Set FinanceTracker.API as Startup Project

### In Visual Studio:
1. Look at **Solution Explorer** (right side of VS)
2. Find: `FinanceTracker.API` project
3. If text is **NOT bold**, right-click it
4. Select: **Set as Startup Project**
5. Now it should appear **bold** in Solution Explorer ?

---

## STEP 3: Open Package Manager Console

### In Visual Studio:
1. Click menu: **Tools**
2. Hover over: **NuGet Package Manager**
3. Click: **Package Manager Console**
4. A PowerShell window should appear at the **bottom** of Visual Studio
5. Default directory will be one of your projects

---

## STEP 4: Navigate to Correct Project (Optional)

The console might show:
```
PM> 
```

You don't necessarily need to change directory, but if you want:

```powershell
cd FinanceTracker.Infrastructure
```

But honestly, you **don't need to** - the `-Project` parameter handles it.

---

## STEP 5: Run Add-Migration Command

### In the Package Manager Console:

**Type or PASTE this command:**
```powershell
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
```

### Then:
1. Press **ENTER**
2. Wait... (console will show building)
3. Look for: ? `Build succeeded`
4. Should complete in 5-10 seconds

### What you should see:
```
PM> Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations
Build started...
Build succeeded.
To undo this action, use Remove-Migration.
PM>
```

### What gets created:
- Go to Solution Explorer
- Expand: `FinanceTracker.Infrastructure`
- Expand: `Migrations` folder (new!)
- You should see:
  - `[TIMESTAMP]_InitialCreate.cs` ? The migration file
  - `AppDbContextModelSnapshot.cs` ? Schema snapshot

---

## STEP 6: Run Update-Database Command

### In the same Package Manager Console:

**Type or PASTE this command:**
```powershell
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```

### Then:
1. Press **ENTER**
2. Wait... (console will show building and migration)
3. Look for: ? `Done.`
4. Should complete in 10-20 seconds

### What you should see:
```
PM> Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
Build started...
Build succeeded.
Applying migration '20240119143256_InitialCreate'.
Done.
PM>
```

---

## STEP 7: Verify in SQL Server Management Studio

### Open SSMS:
1. Start: **SQL Server Management Studio** (search in Windows)
2. Server name: `localhost` (or `(local)` or `.\`)
3. Authentication: **Windows Authentication**
4. Click: **Connect**

### Navigate to Database:
1. Expand: **Databases** (in left panel)
2. Look for: **FinanceTrackerDb** ? NEW DATABASE! ?
3. Expand: **FinanceTrackerDb**
4. Expand: **Tables** (in left panel)
5. Should see:
   - `dbo.Accounts` ? NEW TABLE ?
   - `dbo.Categories` ? NEW TABLE ?
   - `dbo.Expenses` ? NEW TABLE ?
   - `dbo.__EFMigrationsHistory` ? EF Core tracking ?

---

## STEP 8: Query to Double-Check

### In SSMS, run this SQL:

```sql
-- Check Accounts table
SELECT * FROM FinanceTrackerDb.dbo.Accounts;

-- Check Categories table
SELECT * FROM FinanceTrackerDb.dbo.Categories;

-- Check Expenses table
SELECT * FROM FinanceTrackerDb.dbo.Expenses;

-- Check migration history
SELECT * FROM FinanceTrackerDb.dbo.__EFMigrationsHistory;
```

### Expected Results:
- All tables exist (empty now)
- Last query should show 1 row: migration name `InitialCreate`

---

## TROUBLESHOOTING

### ? I don't see Package Manager Console

**Fix:**
1. Tools ? NuGet Package Manager ? Package Manager Console
2. Or use shortcut: `Ctrl+Alt+A` in Visual Studio

---

### ? Build failed in Add-Migration

**Most likely causes:**
1. A syntax error in your entities
2. FinanceTracker.API is not set as Startup Project
3. Missing NuGet packages

**Fix:**
1. Check the error message carefully
2. Set FinanceTracker.API as Startup Project
3. Run: `dotnet build` from command line to see full errors

---

### ? "Cannot open server 'localhost'"

**SQL Server is not running!**

**Fix:**
1. Windows key ? Services
2. Find: `SQL Server (SQLEXPRESS)`
3. Right-click ? **Start**
4. Try the Update-Database command again

---

### ? "Login failed for user 'sa'"

**Wrong password!**

**Fix:**
1. Update password in `FinanceTracker.API\appsettings.json`
2. Connection string should be: `Server=localhost;Database=FinanceTrackerDb;User Id=sa;Password=YOUR_PASSWORD;`
3. Replace `YOUR_PASSWORD` with actual SA password
4. Save file
5. Try Update-Database again

---

### ? "The migration 'InitialCreate' has already been applied"

**This is GOOD! ? Means migration already ran!**

**Verification:**
1. Open SSMS
2. Check FinanceTrackerDb exists with tables
3. You're done! Database is ready!

---

### ? Tables don't appear in SSMS

**Wait a moment, then:**
1. Right-click `FinanceTrackerDb` ? Refresh
2. Right-click `Tables` ? Refresh
3. Tables should appear

**If still not showing:**
1. Verify Update-Database showed: "Done."
2. Check for error messages in Package Manager Console
3. Try closing and reopening SSMS

---

## SUCCESS CHECKLIST

- [ ] Package Manager Console opened
- [ ] `Add-Migration` command completed with "Build succeeded"
- [ ] Migration files created in `FinanceTracker.Infrastructure\Migrations\`
- [ ] `Update-Database` command completed with "Done."
- [ ] SSMS shows FinanceTrackerDb database
- [ ] SSMS shows Accounts, Categories, Expenses tables
- [ ] Tables are empty (no data yet - seeding happens on API start)

? **ALL CHECKED? YOUR DATABASE IS READY!**

---

## NEXT STEPS

1. **Run the API:**
   - Press `F5` or click Debug ? Start Debugging
   - API will automatically seed sample data

2. **Test the endpoints:**
   - Open browser: `https://localhost:5001/swagger`
   - Swagger UI will show your API endpoints
   - Try creating an expense!

3. **Check seeded data:**
   - In SSMS, run: `SELECT * FROM FinanceTrackerDb.dbo.Expenses;`
   - You should see 3 sample expenses

---

## IMPORTANT NOTES

?? **Connection String Security:**
Your password is stored in `appsettings.json` in plain text.
For production, use **User Secrets** or **Azure Key Vault**.
For now (development), this is fine.

?? **Database Backups:**
Before running migrations on shared/production databases, always backup first!

?? **Migrations are version control:**
Always commit your migration files (`*.cs`) to Git!
Never commit the database itself.

---

## QUICK REFERENCE

| Action | Command |
|--------|---------|
| Create migration | `Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations` |
| Apply migration | `Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API` |
| Revert migration | `Update-Database -Migration 0 -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API` |
| See pending | `Get-Migration -Project FinanceTracker.Infrastructure` |

---

## YOU'RE ALL SET! ??

Just follow the steps above and your database will be created.
Good luck! ??
