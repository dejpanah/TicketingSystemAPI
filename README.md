Offline Ticketing System API
A simplified offline ticketing system built with ASP.NET Core 8 Web API for handling internal support requests within an organization. 
This system allows employees to create tickets and admins to manage them with full CRUD operations and statistics.

🌟 Features
JWT Authentication with role-based access control (Employee/Admin)
SQLite Database for local storage with no external dependencies
Full Ticket Management - Create, Read, Update, Delete tickets
Role-based Authorization - Employees can create/view their tickets, Admins have full access
Ticket Statistics - View ticket counts by status
Swagger UI for API documentation and testing
Secure Password Hashing using BCrypt


🛠️ Technologies Used
ASP.NET Core 8
Entity Framework Core
SQLite
JWT Authentication
BCrypt.Net for password hashing
Swagger/OpenAPI for API documentation


🚀 Getting Started
Prerequisites
.NET 8 SDK

Installation

1.Clone the repository:
git clone https://github.com/yourusername/offline-ticketing-system.git
cd offline-ticketing-system

2.Restore dependencies:
dotnet restore

3.Apply database migrations:
dotnet ef database update

4.Run the application:
dotnet run

The API will be available at: https://localhost:7044/swagger/index.html

🔐 Default Users
The application is pre-seeded with two users for testing:

Admin User
Email: admin@example.com
Password: Admin123!

Employee User
Email: employee@example.com
Password: Employee123!

📡 API Endpoints
Authentication
POST /api/auth/login - Login with email and password to receive JWT token
Ticket Management
POST /api/tickets - Create a new ticket (Employee only)
GET /api/tickets/my - List tickets created by the current user (Employee)
GET /api/tickets - List all tickets (Admin only)
GET /api/tickets/{id} - Get a specific ticket's details (Creator or assigned Admin)
PUT /api/tickets/{id} - Update ticket status and assignment (Admin only)
DELETE /api/tickets/{id} - Delete a ticket (Admin only)
GET /api/tickets/stats - Show ticket counts by status (Admin only)

🧪 Example Usage
1. Login as Admin
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@example.com","password":"Admin123!"}'

2.Create a Ticket (as Employee)
curl -X POST https://localhost:5001/api/tickets \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{"title":"Printer Issue","description":"Office printer is not responding","priority":"High"}'

3. View All Tickets (Admin)
curl -X GET https://localhost:5001/api/tickets \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"

4.Update Ticket Status (Admin)
curl -X PUT https://localhost:5001/api/tickets/TICKET_ID \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{"status":"InProgress","assignedToUserId":"ADMIN_USER_ID"}'

  Note: This is a backend-only API. No frontend is included as per the requirements.
