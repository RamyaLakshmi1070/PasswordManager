# Personal Password Manager - Backend API

## Overview

This is the backend API for the Personal Password Manager project. It provides secure storage and retrieval of passwords using a SQL Server database. The API is built with ASP.NET Core 6 and uses Entity Framework Core with the Database-First approach.

---

## Technologies Used

- ASP.NET Core 6
- C#
- Entity Framework Core (Database-First)
- SQL Server (Docker or local instance)
- Swagger (API Documentation)
- xUnit (Unit Testing)

---

## Features

- Store passwords in encrypted format (Base64)
- Perform CRUD operations on password records
- Optional decryption of passwords on retrieval
- Swagger-based API documentation
- Unit tests for repository methods

---

## API Endpoints

| Method | Endpoint            | Description                                         |
|--------|---------------------|-----------------------------------------------------|
| POST   | /passwords          | Add a new encrypted password                        |
| GET    | /passwords          | Retrieve all stored passwords (encrypted)           |
| GET    | /passwords/{id}     | Retrieve a specific password (optionally decrypted) |
| PUT    | /passwords/{id}     | Update an existing password                         |
| DELETE | /passwords/{id}     | Delete a password record                            |

---
#Backend Code Structure:

## API Structure 
/PasswordManager.API
├── Controllers/
│   └── PasswordsController.cs     # API endpoints (CRUD + decrypt)
├── Program.cs                     # App startup, DI, CORS, Swagger
├── appsettings.json               # Connection strings & logging
├── Dockerfile                     # Container build configuration
├── docker-compose.yml            # Orchestration with SQL Server  
└── PasswordManager.API.csproj     # Project file

## Data and Repository Layer

/PasswordManager.Data
├── Models/                        # EF Core database models (scaffolded)
│   └── PasswordManagerDbContext.cs
├── Repository/
│   └── PasswordManagerRepository.cs  # CRUD and encryption logic
├── Helper/
│   └── PasswordEncryptionHelper.cs   # Base64 encrypt/decrypt
└── PasswordManager.Data.csproj    # Project file

## Xunit 

/PasswordManager.Test
├── PasswordManagerRepositoryTests.cs  # In-memory tests covering CRUD & encrypt/decrypt
└── PasswordManager.Test.csproj

---
# Personal Password Manager - Frontend Application

## Overview

This is the frontend application for the Personal Password Manager project. It provides a user interface built with Angular for managing passwords stored securely in the backend API. Users can add, update, delete, and view passwords with optional decryption.

---

## Technologies Used

- Angular 15
- TypeScript
- Angular CLI
- Bootstrap 5S
- angular-notifier (for toast notifications)
- RxJS & HTTPClient for API calls

---

## Features

- List all stored passwords in a responsive table layout
- Masked password display by default
- Decrypt password on demand via button click
- Add new passwords via a form row
- Inline editing of existing passwords
- Delete confirmation prompt
- All notifications via toast messages
- Simple and intuitive single-page interface

---

## UI Behavior

- Passwords are displayed as `••••••` by default.
- Clicking the "eye" icon triggers a GET request to the API and displays the decrypted password.
- "Edit" icon converts the row to editable fields (app, category, username, password).
- On "Save", the changes are sent to the API via PUT request.
- On "Delete", a confirmation prompt appears before making a DELETE request.
- Clicking "+" shows an empty row to add a new password using a POST request.
- All actions show success or error messages via toast notifications.

## Frontend Code Structure

/PasswordManager.Frontend
├── src/
│   ├── app/
│   │   ├── password.component.ts      # UI logic (table, CRUD, validation)
│   │   ├── password.component.html    # Table layout and inline editing
│   │   ├── password.component.css     # Component-specific styles
│   │   └── password.service.ts        # HTTP calls to backend API
│   ├── environments/
│   │   └── environment.ts            # API URL configuration
│   └── index.html, main.ts, styles.css etc.
├── angular.json                     # Angular CLI config
├── package.json                     # Dependencies & scripts
└── tsconfig.json                    # TypeScript configuration


