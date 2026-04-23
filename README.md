# 📚 Library Management System (.NET + EF Core)

A backend-driven application built with ASP.NET Core and Entity Framework Core to manage library operations such as books, users, and transactions. This project demonstrates database design, ORM usage, and clean architecture practices.



## 🚀 Project Structure

```
LibrarySystem/
│── Controllers/
│── Models/
│── Data/
│── Migrations/
│── Services/
│── Program.cs
│── appsettings.json
```

## ⚙️ Prerequisites

Make sure you have the following installed:

* .NET SDK (6 or later)
* SQL Server / LocalDB
* Visual Studio / VS Code
* Entity Framework Core Tools

## 📥 Clone the Repository

```bash id="clone_repo"
git clone https://github.com/your-username/library-management-system.git
cd library-management-system
```

## 🔧 Configure the Connection String

Open `appsettings.json` and update your database connection:

```json id="conn_string"
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=LibrarySystem;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

## 🛠️ Apply Migrations & Create Database

Run the following commands in **Package Manager Console**:

```powershell id="ef_commands"
Add-Migration InitialCreate
Update-Database
```

👉 This will:

* Generate database schema
* Create tables automatically

## ▶️ Run the Application

```bash id="run_app"
dotnet run
```

Or press **F5** in Visual Studio.

# 🗄️ Database Schema (Overview)

The system consists of multiple related entities:

* **Users / Members** → store user details
* **Books** → manage book records
* **Transactions** → issue/return tracking
* **Parties / Vendors** → external entities
* **PurchaseItems / Quotations** → procurement records

### 🔗 Relationships:

* One-to-Many (User → Transactions)
* One-to-Many (Party → PurchaseItems)
* Foreign keys enforced using EF Core

# 📘 EF Core Commands Reference

| Command              | Description          |
| -------------------- | -------------------- |
| `Add-Migration Name` | Create new migration |
| `Update-Database`    | Apply migrations     |
| `Remove-Migration`   | Undo last migration  |
| `Get-Migrations`     | List all migrations  |
| `Script-Migration`   | Generate SQL script  |

# 💻 Sample Code

## ✔ Model Example

```csharp id="model_sample"
public class PurchaseItem
{
    public int Id { get; set; }

    public int PartyId { get; set; }
    public Party Party { get; set; }

    public string ItemName { get; set; }
    public decimal Price { get; set; }
}
```

## ✔ DbContext Configuration

```csharp id="dbcontext_sample"
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<PurchaseItem>()
        .HasOne(p => p.Party)
        .WithMany()
        .HasForeignKey(p => p.PartyId);
}
```

## ✔ Controller Example

```csharp id="controller_sample"
[HttpGet]
public async Task<IActionResult> GetItems()
{
    var items = await _context.PurchaseItems
        .Include(p => p.Party)
        .ToListAsync();

    return Ok(items);
}
```

# 🤝 Contributing

Contributions are welcome!

Steps:

1. Fork the repository
2. Create a new branch (`feature/your-feature`)
3. Commit your changes
4. Push to your branch
5. Open a Pull Request

# 👨‍💻 Author

**Daim Ali**

BS Computer Science Student

Focused on .NET Development, Backend Engineering, and Problem Solving
