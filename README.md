# ASP.NET Core Identity + Bearer Token Authentication

This is a simple .NET 8 project demonstrating how to use ASP.NET Core Identity with Bearer token authentication (JWT).

## Features

- ASP.NET Core Identity with EF Core
- User registration and login
- JWT-based authentication
- Secure endpoints
- Swagger UI for testing

## Setup

1. Update your `appsettings.json` with your SQL Server connection string and JWT settings.
2. Run EF migrations:

```bash
dotnet ef migrations add Init
dotnet ef database update
