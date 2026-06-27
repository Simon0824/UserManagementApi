# UserManagementApi
REST API project built in Clean Architecture and CQRS pattern with postgreSQL db operations for user management 

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
