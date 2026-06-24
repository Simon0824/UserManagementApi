# UserManagementApi
REST API project built in Clean Architecture and CQRS pattern with postgreSQL db operations for user management 

## How to Run
1. Clone the repository
   ```bash
   git clone https://github.com/Simon0824/UserManagementApi
   
2. Add JWT secret to user-secrets:
   ```bash
   dotnet user-secrets set "Jwt:Secret" "Your_Secret__Long_Token"
   
3. Run the project:
   ```bash
   dotnet run
   
4. Open Swagger UI:
   http://localhost:{port}/swagger/index.html
