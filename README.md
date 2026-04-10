# ✅ Task & Project Management API – ASP.NET Core Web API

A **clean, structured, and production-oriented Task & Project Management REST API**
built with **ASP.NET Core Web API**.

---

## 🎯 Project Objective

This project was built to demonstrate:

- Clean backend architecture with separation of concerns
- Service-based business logic
- JWT Authentication & Role-based Authorization
- User-based data isolation
- Pagination with filtering and search
- Global Exception Handling Middleware
- Entity Framework Core best practices

---

## 🧱 Architecture & Design Principles

- Controllers are **thin** — no business logic, no try-catch blocks
- Business logic lives in **Services**
- **DTOs** are used for all API contracts
- Entities are never exposed directly
- EF Core with async queries and `AsNoTracking`
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
- LINQ & async/await
- Pagination with Filtering and Search
- Global Exception Handling Middleware
- RESTful API Design
- Git & GitHub

---

## 📦 Functional Scope

### 🔐 Authentication & Security
- User Registration with automatic **User** role assignment
- User Login returns a **JWT Token** with Claims
- All endpoints protected with `[Authorize]`
- Users access only their own projects and tasks

### 📁 Project Management
- Create / Update / Delete projects
- Retrieve all projects with **Pagination, Search, and Status Filter**
- Retrieve project by ID
- Each project belongs to the authenticated user

### ✅ Task Management
- Create / Update / Delete tasks
- Update task status independently
- Retrieve tasks by project with **Pagination, Search, Priority Filter, and Status Filter**
- Task priorities: Low / Medium / High
- Task statuses: Todo / InProgress / Done / Blocked

---

## 🚨 Global Exception Handling

Custom **Exception Middleware** returns unified error responses.

| Exception | HTTP Status | When Used |
|-----------|-------------|-----------|
| `NotFoundException` | 404 | Project or Task not found |
| `BadRequestException` | 400 | Invalid input or ID mismatch |
| `UnauthorizedException` | 401 | Unauthorized access attempt |

```json
{
  "statusCode": 404,
  "message": "Project with ID 3 not found"
}
```

---

## 📄 Pagination

All list endpoints support pagination with filtering and search.

```
GET /api/projects?pageNumber=1&pageSize=10&search=ecommerce&status=Active
GET /api/tasks/project/1?pageNumber=1&pageSize=10&priority=High&status=InProgress
```

**Response structure:**
```json
{
  "data": [...],
  "pageNumber": 1,
  "pageSize": 10,
  "totalCount": 25,
  "totalPages": 3,
  "hasPreviousPage": false,
  "hasNextPage": true
}
```

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
| GET | /api/projects | Get all user projects (paginated) | ✅ |
| GET | /api/projects/{id} | Get project by ID | ✅ |
| POST | /api/projects | Create project | ✅ |
| PUT | /api/projects/{id} | Update project | ✅ |
| DELETE | /api/projects/{id} | Delete project | ✅ |

### Tasks
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | /api/tasks/{id} | Get task by ID | ✅ |
| GET | /api/tasks/project/{projectId} | Get tasks by project (paginated) | ✅ |
| POST | /api/tasks | Create task | ✅ |
| PUT | /api/tasks/{id} | Update task | ✅ |
| PUT | /api/tasks/{id}/status | Update task status | ✅ |
| DELETE | /api/tasks/{id} | Delete task | ✅ |

---

## 🚀 Getting Started

1. Clone the repository
2. Configure `appsettings.json` using `appsettings.Example.json` as reference
3. Apply migrations: `Update-Database`
4. Run and open Swagger UI
5. Register → Login → Authorize → Test Endpoints

---

## 🔒 Security

- JWT Token authentication
- userId from JWT Claims — never from QueryString
- User-based data isolation
- Global Exception Handling prevents stack trace exposure

---

## 🧑‍💻 Author

**Mohammad Al-Mohammad**
Backend Developer – ASP.NET Core
