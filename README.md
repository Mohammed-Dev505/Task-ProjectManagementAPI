# 🗂️ Task & Project Management System – ASP.NET Core Web API

A **clean, structured, and production-oriented Task & Project Management REST API** built with **ASP.NET Core Web API (.NET 8)** following strict **Clean Architecture** principles and enterprise design patterns.

---

## 🎯 Project Objective

This project was built to demonstrate advanced backend engineering capabilities, including:

* **Decoupled Architecture:** Clean separation of concerns across 4 distinct layers (Domain, Application, Infrastructure, Web API).
* **User-Based Data Isolation:** Secure resource filtering based on JWT identity claims ensuring users access only their own data workspace.
* **Auto-Provisioning System:** Automated seeding infrastructure that configures default roles and a master administrator upon the initial boot phase.
* **Database Resiliency:** Advanced relational schema mapping with explicit cascade restriction constraints.

---

## 🧱 Architecture & Design Principles

The solution is split into **4 decoupled layers** adhering strictly to Clean Architecture guidelines:

* **Domain Layer:** Core enterprise entities (`Project`, `TaskItem`, and Identity `User`), fully isolated from database frameworks.
* **Application Layer:** Contains request DTOs, custom Exception abstractions, AutoMapper profile definitions, and FluentValidation rules.
* **Infrastructure Layer:** Handles Entity Framework Core database context configurations (`AppDbContext`), data tracking, and identity mechanics.
* **Web API Layer:** Coordinates thin API controllers, routing structures, and global application middleware components.

### 💎 Advanced Architectural Decisions:

* **Restrictive Delete Policy:** Crucial database relationships are configured explicitly using `DeleteBehavior.Restrict`. When a project or a user container is dropped, its associated records are tightly guarded against structural fragmentation.
* **String Enum Conversions:** Task properties such as `Status` and `Priority` leverage fluent conversion strategies (`HasConversion<string>()`) inside the database context to register status values as strings instead of unreadable enum integers, streamlining database reporting.
* **Serialization Safety:** API responses employ `JsonStringEnumConverter` transformations directly inside client contracts to ensure readable data delivery (e.g., "High", "InProgress") to frontend systems.

---

## 🛠️ Technologies

* **Framework:** ASP.NET Core Web API (.NET 8)
* **Database & ORM:** Microsoft SQL Server + Entity Framework Core (Code-First)
* **Security Subsystem:** ASP.NET Core Identity + JWT Bearer Token Authentication
* **Mapping & Validation:** AutoMapper & FluentValidation
* **API Documentation:** Swagger / OpenAPI with Authorization Header Token Support
* **Error Management:** Centralized Custom Global Exception Handling Middleware

---

## 🚀 Key Technical Features

### 🔐 1. Self-Provisioning Identity & Security Models

* **Automatic Role Setup:** During initial pipeline configuration, the application utilizes specialized database seeding mechanisms (`ContextSeed`) to dynamically inject fundamental application roles (`Admin`, `Manager`, `User`) and generate a root administrative user automatically.
* **Data Isolation:** Endpoints securely capture the `userId` claim directly out of authenticated incoming cryptographic tokens rather than looking it up via the QueryString, preventing cross-user data tampering.

### 🧩 2. Extension Methods Pipeline

To keep `Program.cs` remarkably clean, manageable, and decoupled, all external dependencies are initialized using explicit configuration methods:

| Extension Method | Service Registration Responsibility |
|------------------|------------------------------------|
| `AddDatabase` | Binds database context pipelines and configurations |
| `AddIdentityServices` | Initializes underlying ASP.NET Core Identity frameworks |
| `AddJwtAuthentication` | Sets up JWT claim validation parameters and encryption settings |
| `AddApplicationServices` | Resolves core application workflows and service implementations |
| `AddValidationServices` | Attaches automated FluentValidation engines to payload inputs |
| `AddSwaggerService` | Pre-configures OpenAPI definitions with JWT input controls |

**Resulting Clean `Program.cs` Pipeline:**
```csharp
builder.Services.AddDatabase(builder.Configuration);
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddValidationServices();
builder.Services.AddSwaggerService();

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

## ✅ FluentValidation Strategy

All input payloads are strictly vetted with developer-friendly error messages before hitting the business layer:

| DTO | Validation Rules Applied |
|-----|--------------------------|
| `RegisterUserDto` | Username (3–50 chars), valid Email syntax, Password (min 6 chars), PhoneNumber required |
| `LoginDto` | Valid Email structure required, non-empty Password input block |
| `CreateProjectDto` | Project Name is mandatory (3–150 chars), Description set to optional constraint (max 500 chars) |
| `UpdateProjectDto` | Valid Project ID verification tracking, mandatory updated Name parameters |
| `CreateTaskDto` | Task Title required (3–100 chars), valid Priority enum state constraint, DueDate strictly in future |
| `UpdateTaskDto` | Valid Task ID verification, Title override parameters, adjustable future-bounded DueDate checks |
| `UpdateTaskStatusDto`| Valid Task ID, incoming state strictly constrained within defined TaskStatus enum boundaries |

---

## 🚨 Global Exception Handling

The centralized exception logging middleware intercept application-level validation errors globally, returning consistent JSON schemas:

| Exception | HTTP Status | When Used / Operational Rule |
|-----------|-------------|------------------------------|
| `NotFoundException` | 404 | Thrown whenever a targeted Project or Task identifier fails to register in database lookups |
| `BadRequestException` | 400 | Dispatched during operational business parameter mismatches or failed invariant data states |
| `UnauthorizedException` | 401 | Executed when an operation fails secure data tenancy ownership validation or user evaluation |

📄 Server-Side Pagination Schema

### Request

```http
GET /api/projects?pageNumber=1&pageSize=10&search=ecommerce

{
  "data": [...],
  "pageNumber": 1,
  "pageSize": 10,
  "totalCount": 25,
  "totalPages": 3,
  "hasPreviousPage": false,
  "hasNextPage": true
}

## 🔑 Core API Endpoints

### 🔐 Authentication

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Registers a new user account profile securely | ❌ No |
| POST | `/api/auth/login` | Validates active credentials & returns a secure JWT Token | ❌ No |

### 📁 Project Management

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/projects` | Get all user projects with calculated task summary counters | ✅ Yes |
| GET | `/api/projects/{id}` | Locates an isolated project entity parsing its details | ✅ Yes |
| POST | `/api/projects` | Provisions a fresh project tracking workspace context | ✅ Yes (Admin/Manager) |
| PUT | `/api/projects/{id}` | Updates metadata specifications bound to an active workspace | ✅ Yes (Admin/Manager) |
| DELETE | `/api/projects/{id}` | Wipes out a project container (Blocks if child tasks exist) | ✅ Yes (Admin) |

### 📝 Task Tracking Workspace

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/tasks/{id}` | Extracts complete metadata parameters mapped to a specific task | ✅ Yes |
| GET | `/api/tasks/project/{projectId}` | Extracts paginated tasks inside a specific project container | ✅ Yes |
| POST | `/api/tasks/project/{projectId}` | Appends an execution task directly into a parent project | ✅ Yes (Admin/Manager) |
| PUT | `/api/tasks/{id}` | Updates core descriptive configurations inside an active task | ✅ Yes (Admin/Manager) |
| PATCH | `/api/tasks/{id}/status` | Fast-tracks isolated updates modifying task pipeline visibility | ✅ Yes |
| DELETE | `/api/tasks/{id}` | Drops a selected task item reference permanently from database | ✅ Yes (Admin/Manager) |

⚙️ Local Installation & Setup
1. Verification of Local Connection Parameters
Review file declarations inside the main presentation host project directory (appsettings.json). Ensure configurations reference your local environment properties properly:
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\;Database=TaskProjectManagement_DB;Trusted_Connection=True;TrustServerCertificate=True;"
}

2. Database Migrations Deployment
Initialize the Package Manager Console (PMC) inside Visual Studio, direct your execution target to your Infrastructure project assembly location (Task_ProjectManagementAPI.Infrastructure), and execute:
# 1. Compile configurations into code migration sets
Add-Migration InitialProjectManagementSetup -Project Infrastructure

# 2. Push context schemas directly onto your active SQL database engine instance
Update-Database

3. Application Operational Execution
Launch your processing server directly via Visual Studio or invoke using CLI execution tasks:
dotnet run --project Task_ProjectManagementAPI.WebAPI

Open up your local browser route down to /swagger to examine, validate, and interact with your dynamic endpoints.

​🧑‍💻 Author
​Mohammad Al-Mohammad – Backend Developer – ASP.NET Core Specialist.