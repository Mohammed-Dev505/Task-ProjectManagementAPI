# ✅ Task & Project Management API – ASP.NET Core Web API

A **clean, structured, and production-oriented Task & Project Management REST API**
built with **ASP.NET Core Web API**.

This project demonstrates real-world backend architecture, secure authentication, user-based data isolation, and global exception handling.

---

## 🎯 Project Objective

This project was built to demonstrate:

- Clean backend architecture with separation of concerns
- Service-based business logic
- JWT Authentication & Role-based Authorization
- User-based data isolation
- Global Exception Handling with custom exceptions
- Entity Framework Core best practices
- RESTful API design

---

## 🧱 Architecture & Design Principles

The project follows a **Clean Architecture–inspired approach**:

- Controllers are **thin** — no business logic, no try-catch blocks
- Business logic lives in **Services**
- **DTOs** are used for all API contracts
- Entities are never exposed directly
- EF Core used with async queries and `AsNoTracking`
- userId extracted from JWT Token Claims — never from QueryString
- Global Exception Handling via custom Middleware

---

## 🛠 Technologies

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- JWT Bearer Authentication
- AutoMapper
- LINQ
- Global Exception Handling Middleware
- RESTful API Design

---

## 📦 Functional Scope

### 🔐 Authentication & Security
- User Registration with automatic **User** role assignment
- User Login returns a **JWT Token** with Claims
- All endpoints protected with `[Authorize]`
- userId extracted from JWT Token Claims
- Users can only access their own Projects and Tasks

---

### 📁 Project Management
- Create / Update / Delete projects
- Retrieve all projects for the authenticated user
- Retrieve project by ID
- Each project belongs to one user

---

### ✅ Task Management
- Create / Update / Delete tasks
- Update task status independently
- Retrieve tasks by project
- Retrieve task by ID
- Tasks are linked to projects
- Task priorities: Low / Medium / High
- Task statuses: Todo / InProgress / Done / Blocked

---

## 🚨 Global Exception Handling

The API uses a custom **Exception Middleware** that intercepts all unhandled exceptions and returns a unified error response.

### Custom Exceptions

| Exception | HTTP Status | When Used |
|-----------|-------------|-----------|
| `NotFoundException` | 404 | Project or Task not found |
| `BadRequestException` | 400 | Invalid input or ID mismatch |
| `UnauthorizedException` | 401 | Unauthorized access attempt |

### Unified Error Response

```json
{
  "statusCode": 404,
  "message": "Project with ID 3 not found"
}
```

### Benefits
- Controllers are clean — no try-catch blocks
- Consistent error responses across all endpoints
- Centralized error logging

---

## 📑 DTO Strategy

Each operation uses **dedicated DTOs**:

- **Create DTOs** — for creating new records
- **Update DTOs** — for modifying existing records
- **Response DTOs** — for API responses

---

## 🔑 API Endpoints

### Auth
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | /api/auth/register | Register new user | ❌ |
| POST | /api/auth/login | Login and get JWT Token | ❌ |
| POST | /api/auth/addrole | Assign role to user | ✅ Admin |

### Projects
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | /api/projects | Get all user projects | ✅ |
| GET | /api/projects/{id} | Get project by ID | ✅ |
| POST | /api/projects | Create project | ✅ |
| PUT | /api/projects/{id} | Update project | ✅ |
| DELETE | /api/projects/{id} | Delete project | ✅ |

### Tasks
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | /api/tasks/{id} | Get task by ID | ✅ |
| GET | /api/tasks/project/{projectId} | Get tasks by project | ✅ |
| POST | /api/tasks | Create task | ✅ |
| PUT | /api/tasks/{id} | Update task | ✅ |
| PUT | /api/tasks/{id}/status | Update task status | ✅ |
| DELETE | /api/tasks/{id} | Delete task | ✅ |

---

## 🚀 Getting Started

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   ```

2. **Configure settings** using `appsettings.Example.json` as reference:
   ```json
   {
     "ConnectionStrings": {
       "DbContext": "YOUR_CONNECTION_STRING_HERE"
     },
     "JWT": {
       "Key": "YOUR_SECRET_KEY_MIN_32_CHARACTERS",
       "Issuer": "TaskManagementAPI",
       "Audience": "TaskManagementUsers",
       "DurationInDays": 7
     }
   }
   ```

3. **Apply migrations**
   ```bash
   Update-Database
   ```

4. **Run the application** and open Swagger UI

5. **Test the API:**
   ```
   Register → Login → Copy Token → Authorize in Swagger → Test Endpoints
   ```

---

## 🔒 Security

- Secure password hashing via ASP.NET Identity
- JWT Token authentication
- All endpoints protected with `[Authorize]`
- userId from Token Claims — never from QueryString
- User-based data isolation
- Global Exception Handling prevents stack trace exposure

---

## 🎯 Purpose

This project was built as a **backend portfolio project** to demonstrate:

- Real-world API structure
- Clean architecture thinking
- Secure JWT authentication
- User-based data isolation
- Global Exception Handling
- Readiness for Junior Backend Developer roles

---

## 🧑‍💻 Author

**Mohammad Al-Mohammad**
Backend Developer – ASP.NET Core
