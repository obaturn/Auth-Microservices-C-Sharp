# Authentication Microservice

A simple authentication system built with .NET, following the Onion Architecture and Repository Pattern.

## Architecture

This project follows the **Onion Architecture** with clear separation of concerns:

- **Domain Layer** (`Auth.Domain`): Core business entities and interfaces.
- **Application Layer** (`Auth.Application`): Business logic, services, and DTOs.
- **Infrastructure Layer** (`Auth.Infrastructure`): Data access, repositories, and database migrations.
- **Presentation Layer** (`Auth.API`): ASP.NET Core Web API controllers and configuration.

## Features

- User Registration with secure password hashing (BCrypt)
- User Login with JWT token generation
- Token Validation middleware for protecting endpoints
- PostgreSQL database with Entity Framework Core

## Prerequisites

- .NET 8.0 SDK
- PostgreSQL database
- Git

## Setup

1. **Clone the repository**:
   ```
   git clone <repository-url>
   cd AuthMicroservice
   ```

2. **Configure Database**:
   - Create a PostgreSQL database (e.g., `AuthSystem`).
   - Update `Auth.API/.env` with your database credentials:
     ```
     ConnectionStrings__DefaultConnection=Host=localhost;Database=AuthSystem;Username=postgres;Password=yourpassword
     ```

3. **Restore Packages**:
   ```
   dotnet restore
   ```

4. **Run Migrations**:
   ```
   cd Auth.Infrastructure
   dotnet ef database update -s ../Auth.API -p .
   cd ..
   ```

5. **Run the Application**:
   ```
   cd Auth.API
   dotnet run
   ```

   The API will be available at `http://localhost:5090`.

## API Endpoints

### Public Endpoints

- **POST /api/Auth/register**
  - Register a new user.
  - Body: `{"username": "string", "email": "string", "password": "string"}`

- **POST /api/Auth/login**
  - Authenticate user and get JWT token.
  - Body: `{"username": "string", "password": "string"}`
  - Response: `{"token": "jwt-token", "expiresAt": "datetime", "username": "string", "email": "string"}`

### Protected Endpoints

- **GET /api/Auth/profile**
  - Get user profile (requires JWT token).
  - Header: `Authorization: Bearer <jwt-token>`
  - Response: `{"username": "string", "email": "string"}`

## Testing

### Swagger UI
- Access `http://localhost:5090/swagger` for interactive API documentation.
- Use the "Authorize" button to set JWT tokens for protected endpoints.

### Postman
- Import the collection from Swagger or manually create requests.
- For protected endpoints, add `Authorization: Bearer <token>` header.

## Project Structure

```
AuthMicroservice/
├── Auth.API/                 # Presentation layer
│   ├── Controllers/
│   ├── Services/
│   ├── Program.cs
│   └── appsettings.json
├── Auth.Application/         # Application layer
│   ├── Services/
│   └── Dto/
├── Auth.Domain/              # Domain layer
│   ├── Entities/
│   └── Repositories/
├── Auth.Infrastructure/      # Infrastructure layer
│   ├── Data/
│   ├── Repositories/
│   └── Migrations/
├── AuthMicroservice.sln
└── README.md
```

## Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- BCrypt for password hashing
- Swagger/OpenAPI

## Security

- Passwords are hashed using BCrypt.
- JWT tokens are signed with HMAC-SHA256.
- Tokens expire after 60 minutes (configurable).

## Contributing

1. Fork the repository.
2. Create a feature branch.
3. Commit changes.
4. Push to the branch.
5. Create a Pull Request.

## License

This project is for educational purposes.