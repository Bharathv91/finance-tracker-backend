# ?? FIXING THE FOREIGN KEY ERROR

## Problem
When you tried to create an Expense, you got a Foreign Key constraint error:
```
The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Expenses_Accounts_AccountId"
```

## Root Cause
You sent an `AccountId` or `CategoryId` that **doesn't exist** in the database, or the ID format was invalid.

---

## ? SOLUTION

I've added two new helper endpoints to your API:

### **1. GET /api/expenses/accounts**
Returns all valid accounts you can use:

**Response Example:**
```json
[
  {
    "id": "e4b25c83-cbe5-4830-a3a3-1843d06a99db",
    "name": "Cash Wallet",
    "balance": 5000.00
  },
  {
    "id": "353a1e2a-4ded-4a74-aa35-9ae3ee088b75",
    "name": "Bank - Checking",
    "balance": 20000.00
  }
]
```

### **2. GET /api/expenses/categories**
Returns all valid categories you can use:

**Response Example:**
```json
[
  {
    "id": "d1feba3c-2609-4bf5-89bf-27727c333586",
    "name": "Groceries"
  },
  {
    "id": "1f64f015-4a68-4b00-b4df-2bf625ce929c",
    "name": "Transport"
  },
  {
    "id": "cd243f2b-7760-4cfb-94b0-daaab5d9c489",
    "name": "Utilities"
  }
]
```

---

## ?? HOW TO CREATE AN EXPENSE CORRECTLY

### **Step 1: Get the valid IDs**
1. In Swagger, find: **GET /api/expenses/accounts**
2. Click "Try it out" ? "Execute"
3. Copy one of the `id` values
4. Do the same for **GET /api/expenses/categories**
5. Copy one of the `id` values

### **Step 2: Create the Expense**
1. Find: **POST /api/expenses**
2. Click "Try it out"
3. Use this format for the request body:

```json
{
  "title": "Buy groceries",
  "amount": 50.50,
  "date": "2025-11-30T18:00:00.000Z",
  "categoryId": "d1feba3c-2609-4bf5-89bf-27727c333586",
  "accountId": "e4b25c83-cbe5-4830-a3a3-1843d06a99db",
  "notes": "Weekly shopping"
}
```

**Important:**
- Replace `categoryId` with an ID from GET /api/expenses/categories
- Replace `accountId` with an ID from GET /api/expenses/accounts
- Use ISO 8601 format for `date`: `YYYY-MM-DDTHH:mm:ss.sssZ`
- `categoryId` and `accountId` can be `null` if you want

### **Step 3: Submit**
- Click "Execute"
- You should get a **201 Created** response ?

---

## ?? COMPLETE WORKING EXAMPLE

### Get Accounts:
```
GET /api/expenses/accounts
Response: 200 OK
```

Copy the first account's ID.

### Get Categories:
```
GET /api/expenses/categories
Response: 200 OK
```

Copy the first category's ID.

### Create Expense:
```
POST /api/expenses

Request Body:
{
  "title": "Electricity Bill",
  "amount": 1200.00,
  "date": "2025-11-30T00:00:00Z",
  "categoryId": "d1feba3c-2609-4bf5-89bf-27727c333586",
  "accountId": "e4b25c83-cbe5-4830-a3a3-1843d06a99db",
  "notes": "November electricity"
}

Response: 201 Created
```

---

## ?? WHAT CHANGED IN THE CODE

I added validation to the **POST /api/expenses** endpoint:

1. **Check if AccountId exists** (if provided)
2. **Check if CategoryId exists** (if provided)
3. **Return helpful error message** if either doesn't exist

This way, you get a 400 Bad Request with a clear message instead of a cryptic 500 error.

---

## ? EXAMPLE: VALID IDs FROM YOUR DATABASE

**Accounts:**
- `e4b25c83-cbe5-4830-a3a3-1843d06a99db` (Cash Wallet)
- `353a1e2a-4ded-4a74-aa35-9ae3ee088b75` (Bank - Checking)

**Categories:**
- `d1feba3c-2609-4bf5-89bf-27727c333586` (Groceries)
- `1f64f015-4a68-4b00-b4df-2bf625ce929c` (Transport)
- `cd243f2b-7760-4cfb-94b0-daaab5d9c489` (Utilities)

---

## ?? STEP-BY-STEP IN SWAGGER

1. **Restart the API** (F5) so it picks up the changes
2. **In Swagger, scroll down to find:**
   - `GET /api/expenses/accounts`
   - `GET /api/expenses/categories`
3. **Click each one and execute** to see the IDs
4. **Use those IDs when creating** an Expense with POST
5. **Submit the POST request**
6. **Success!** ??

---

## ? WHAT YOU SHOULD SEE NOW

**Before:** 500 Error with Foreign Key violation
**After:** 201 Created with your new Expense

---

## ?? NOTES

- If you set `categoryId` or `accountId` to `null`, the expense will still be created
- The endpoints now validate that the Account/Category exists before saving
- All existing seeded data is still there in the database

---

**Now try again with the correct IDs!** ??
