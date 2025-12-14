# ?? MIGRATION REFERENCE CARD

## ? THE COMMANDS

```powershell
# COMMAND 1: Create migration files
Add-Migration InitialCreate -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API -OutputDir Migrations

# COMMAND 2: Create database and tables
Update-Database -Project FinanceTracker.Infrastructure -StartupProject FinanceTracker.API
```

---

## ?? WHERE TO RUN

**Visual Studio:**
- Tools ? NuGet Package Manager ? Package Manager Console

---

## ? BEFORE YOU START

- [ ] SQL Server running
- [ ] FinanceTracker.API set as Startup Project (bold in Solution Explorer)
- [ ] Build succeeded
- [ ] Connection string correct in appsettings.json

---

## ?? TIMING

| Step | Time | What Happens |
|------|------|--------------|
| Command 1 | ~10 sec | Migration files created |
| Command 2 | ~15 sec | Database created |
| Verify | ~5 sec | Check SSMS |
| **Total** | **~30 sec** | **Done!** ? |

---

## ?? EXPECTED OUTPUT

### After Command 1:
```
Build succeeded
To undo this action, use Remove-Migration.
```

### After Command 2:
```
Build succeeded
Applying migration '20240119143256_InitialCreate'.
Done.
```

---

## ?? WHAT GETS CREATED

### Folders:
```
FinanceTracker.Infrastructure/Migrations/
??? [TIMESTAMP]_InitialCreate.cs
??? AppDbContextModelSnapshot.cs
```

### Database:
```
FinanceTrackerDb/
??? Accounts
??? Categories
??? Expenses
??? __EFMigrationsHistory
```

---

## ?? VERIFY IN SSMS

1. Open SQL Server Management Studio
2. Connect to: localhost
3. Expand: Databases ? FinanceTrackerDb ?
4. Expand: Tables
5. See: Accounts, Categories, Expenses ?

---

## ? COMMON ERRORS & FIXES

| Error | Fix |
|-------|-----|
| "Cannot open server" | Start SQL Server (Windows Services) |
| "Login failed" | Check password in appsettings.json |
| "Design-time factory" | Set FinanceTracker.API as Startup Project |
| "Build failed" | Check error message, syntax error likely |
| "Already applied" | ? Success! Check SSMS |

---

## ?? STEP-BY-STEP

1. Open Package Manager Console
   - Tools ? NuGet Package Manager ? Package Manager Console

2. Copy-paste Command 1
   - Right-click ? Paste
   - Press ENTER
   - Wait for "Build succeeded"

3. Copy-paste Command 2
   - Right-click ? Paste
   - Press ENTER
 - Wait for "Done."

4. Verify in SSMS
   - Open SQL Server Management Studio
   - Check for FinanceTrackerDb and tables

5. Done! ?

---

## ?? CONFIGURATION

| Setting | Value |
|---------|-------|
| **Server** | localhost |
| **Database** | FinanceTrackerDb |
| **User** | sa |
| **Password** | btapphym91 |
| **DbContext** | FinanceTracker.Infrastructure\Persistence\AppDbContext.cs |
| **Migrations Folder** | FinanceTracker.Infrastructure\Migrations\ |
| **Startup Project** | FinanceTracker.API |

---

## ?? AFTER MIGRATION

```bash
# Commit to Git
git add FinanceTracker.Infrastructure/Migrations/
git commit -m "feat: add InitialCreate migration"
git push
```

---

## ?? GUIDES AVAILABLE

- EXECUTE_NOW.md - Ready to go (2 min)
- DETAILED_STEP_BY_STEP_GUIDE.md - Full guide (10 min)
- FINAL_CHECKLIST.md - Pre-flight check (8 min)
- QUICK_MIGRATION_REFERENCE.md - Quick lookup (5 min)
- MIGRATION_SUMMARY.md - Complete summary (10 min)
- MIGRATION_GUIDES_INDEX.md - Guide index (5 min)

---

## ? KEY POINTS

? **Just 2 commands needed**
? **Takes ~30-40 seconds**
? **Highly unlikely to fail**
? **Easy to verify**
? **If fails, easy to fix**

---

## ?? YOU'RE READY!

Run the commands and your database will be created! ??

---

## ?? NEED HELP?

? See: **DETAILED_STEP_BY_STEP_GUIDE.md**
? Use: **FINAL_CHECKLIST.md**
? Reference: **MIGRATION_SUMMARY.md**

---

**Happy migrating! ?**
