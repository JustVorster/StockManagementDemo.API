
CMS Stock Management Backend (ASP.NET Core 8 Web API)
=====================================================

This is the backend service for the CMS Stock Management system. It provides secure API endpoints for managing stock vehicles, images, accessories, and user authentication.

Tech Stack
----------
- ASP.NET Core 8 Web API
- Entity Framework Core (Code First)
- SQL Server / LocalDB
- JWT Authentication
- Swagger/OpenAPI

Requirements
------------
- .NET 8 SDK
- SQL Server LocalDB or Express
- EF Core Tools:
  dotnet tool install --global dotnet-ef

Setup & Run
-----------
1. Clone & Navigate

   git clone <https://github.com/JustVorster/StockManagementDemo.API>
   cd StockManagementDemo.API

2. Restore Dependencies

   dotnet restore

3. Apply Migrations & Create DB

   dotnet ef database update

4. Run API

   dotnet run

5. Open Swagger

   https://localhost:7140/swagger

Authentication
--------------
- JWT-based auth
- Use /api/auth/register and /api/auth/login
- Token is returned on login and must be sent as Authorization: Bearer <token>

Test user (auto-seeded):
Username: demo
Password: Demo1234!

Seeding
-------
If the database is empty, it will auto-seed:
- 2 Stock Items with Accessories
- 1 Test User

This logic lives inside Program.cs.

Database Structure
------------------
- StockItems
- Accessories (linked by StockItemId)
- Images (byte array stored in DB, max 3 per stock item)
- Users (auth via hashed password)

API Endpoints
-------------
| Endpoint                | Method | Auth | Description                      |
|------------------------|--------|------|----------------------------------|
| /api/auth/register     | POST   | ❌   | Register new user                |
| /api/auth/login        | POST   | ❌   | Authenticate + return JWT        |
| /api/stockitems        | GET    | ✅   | Get all stock items              |
| /api/stockitems/{id}   | GET    | ✅   | Get stock item by ID             |
| /api/stockitems        | POST   | ✅   | Create stock item                |
| /api/stockitems/{id}   | PUT    | ✅   | Update stock item                |
| /api/stockitems/{id}   | DELETE | ✅   | Delete stock item                |
| /api/images/upload     | POST   | ✅   | Upload image to stock item       |

Environment Variables
---------------------
In appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockDemoDb;Integrated Security=True;"
},
"Jwt": {
    "Key": "yWk5g1a2yTcZp9Tq68S93zGJk26WeCr6Cp0xq92z0Og=",
    "Issuer": "StockManagementAPI",
    "Audience": "StockManagementClient"
},

Reset DB (Optional)
-------------------
dotnet ef database drop
dotnet ef database update


