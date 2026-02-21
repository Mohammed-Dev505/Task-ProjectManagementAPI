# Task Management System API

A clean and simple **ASP.NET Core Web API** for managing projects and tasks.
This project is designed as a practical backend portfolio project focusing on
clean architecture, service-based design, and proper use of Entity Framework Core.

---

## 🚀 Features

- User Registration & Login (ASP.NET Identity)
- Project Management (Create, Update, Delete, Get)
- Task Management (Create, Update, Delete, Change Status)
- Tasks linked to Projects
- User-based data isolation (each user accesses only their own data)
- Clean separation between Controllers, Services, DTOs, and Entities

---

## 🧱 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- AutoMapper
- LINQ
- RESTful API Design

---

## 📂 Project Structure

---

## 🧠 Design Decisions

- **DTOs** are used to avoid exposing database entities directly.
- **Service Layer** contains all business logic.
- **Controllers** are thin and only handle HTTP requests.
- **EF Core** is used with async queries for better performance.
- **UserId-based authorization** ensures data isolation between users.

---

## 🔐 Authentication

- User registration and login implemented using **ASP.NET Identity**
- Passwords are securely hashed
- Each resource (Project / Task) is associated with a specific user
- Users can only access their own projects and tasks

---

## 📌 API Endpoints (Sample)

### Projects
- `GET /api/projects/getall`
- `GET /api/projects/getbyid`
- `POST /api/projects/create`
- `PUT /api/projects/update`
- `DELETE /api/projects/delete`

### Tasks
- `GET /api/tasks/getbyid`
- `GET /api/tasks/getbyprojectid`
- `POST /api/tasks/create`
- `PUT /api/tasks/update`
- `PUT /api/tasks/updatestatus`
- `DELETE /api/tasks/delete`

---

## ⚙️ Getting Started

1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run database migrations:
   ```bash
   update-database

   🎯 Purpose of This Project
This project was built as a backend portfolio project to demonstrate:
Real-world API structure
Proper use of Entity Framework Core
Clean code practices
Service-based architecture
Readiness for Junior Backend Developer roles

🧑‍💻 Author
Mohammad Al-Mohammad
Backend Developer – ASP.NET Core