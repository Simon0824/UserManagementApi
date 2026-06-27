# UserManagementApi
REST API User Management module built with Clean Architecture, CQRS + MediatR and Entity Framework Core on PostgreSQL. Performance improved with Redis Distributed Caching.

## Features
- User registration and login with JWT Bearer authentication
- Role-based access control (Admin / Member)
- User banning system
- Password change functionality
- Protected admin endpoints (e.g. GetAllUsers)
- Input validation with FluentValidation
- Global exception handling with Problem Details
- Redis caching for better performance
- Docker support

## Technologies
- .NET 10 + ASP.NET Core
- Clean Architecture + CQRS (MediatR)
- Entity Framework Core + PostgreSQL
- Redis (Distributed Cache)
- JWT Authentication + ASP.NET Identity
- FluentValidation
- Docker + docker-compose
- Swagger UI with JWT support

## How to Run
1. Clone the repository
   ```bash
   git clone https://github.com/Simon0824/UserManagementApi
   cd UserManagementApi
   
2. Add JWT secret to user-secrets:
   ```bash
   dotnet user-secrets set "Jwt:Secret" "Your_Secret__Long_Token"
   
3. Run with Docker (recommended):
   ```bash
   docker-compose up --build
   
4. Or run locally:
   ```bash
   dotnet run --project src/UserManagementApi.Api

5. Open Swagger UI:
   ```bash
   http://localhost:8080/swagger

## Run live demo:
   ```bash
   https://usermanagementapi-fnb4f9hpcndzetgm.polandcentral-01.azurewebsites.net/swagger/index.html
