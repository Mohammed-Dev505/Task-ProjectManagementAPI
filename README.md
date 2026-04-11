# 🛒 E-Commerce API – ASP.NET Core Web API

A **clean, structured, and production-oriented E-Commerce REST API** built with **ASP.NET Core Web API**.

---

## 🎯 Project Objective

This project was built to demonstrate:

- Clean backend architecture with separation of concerns
- Service-based business logic
- JWT Authentication & Role-based Authorization
- Entity Framework Core relationships (One-to-Many & Many-to-Many)
- Real-world e-commerce domain modeling
- Pagination with filtering and search
- Global Exception Handling Middleware
- User-based data isolation

---

## 🧱 Architecture & Design Principles

- Controllers are **thin** — no business logic, no try-catch blocks
- Business logic lives in **Services**
- **DTOs** are used for all API contracts
- Entities are never exposed directly
- EF Core with async queries and `AsNoTracking`
- JWT Claims used for user identification
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
- Role-based access: **Admin** and **User**
- userId extracted from JWT Token Claims

### 📂 Category Management
- Create / Update / Delete categories
- Retrieve all categories with **Pagination and Search**

### 📦 Product Management
- Create / Update / Delete products
- Retrieve all products with **Pagination, Search, Category Filter, and Price Filter**
- Filter products by category
- `IsAvailable` flag for product availability

### 🛒 Cart System
- Each user has one cart (One-to-One)
- Add / Update / Remove items from cart
- Clear entire cart
- Cart items linked to real-time product prices

### 📋 Order System
- Create orders from selected items
- Each order stores item price **at time of purchase**
- Auto-calculate `TotalPrice`
- Update order status: Pending → Shipped → Delivered → Cancelled
- Cart cleared automatically after order creation
- Retrieve orders with **Pagination**

### ⭐ Review System
- Add / Update / Delete reviews
- Retrieve reviews by product with **Pagination**
- Retrieve reviews by user with **Pagination**

---

## 🚨 Global Exception Handling

Custom **Exception Middleware** returns unified error responses.

| Exception | HTTP Status | When Used |
|-----------|-------------|-----------|
| `NotFoundException` | 404 | Resource not found |
| `BadRequestException` | 400 | Invalid input or unavailable product |
| `UnauthorizedException` | 401 | Unauthorized access attempt |

```json
{
  "statusCode": 404,
  "message": "Product with ID 5 not found"
}
```

---

## 📄 Pagination

All list endpoints support pagination with filtering and search.

```
GET /api/product?pageNumber=1&pageSize=10&search=phone&categoryId=2
GET /api/order?pageNumber=1&pageSize=10
GET /api/review?pageNumber=1&pageSize=10
```

**Response structure:**
```json
{
  "data": [...],
  "pageNumber": 1,
  "pageSize": 10,
  "totalCount": 100,
  "totalPages": 10,
  "hasPreviousPage": false,
  "hasNextPage": true
}
```

---

## 🗃 Database Design

| Entity | Relationships |
|--------|---------------|
| User | One-to-One → Cart, One-to-Many → Orders, Reviews |
| Category | One-to-Many → Products |
| Product | One-to-Many → Reviews, CartItems, OrderItems |
| Cart | One-to-Many → CartItems |
| CartItem | Junction: Cart ↔ Product |
| Order | One-to-Many → OrderItems |
| OrderItem | Junction: Order ↔ Product (stores price at purchase) |
| Review | Many → User, Many → Product |

---

## 🔑 API Endpoints

### Auth
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | /api/auth/register | Register new user | ❌ |
| POST | /api/auth/login | Login and get JWT Token | ❌ |
| POST | /api/auth/addrole | Assign role to user | ✅ Admin |

### Category
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | /api/category | Get all categories (paginated) | ✅ |
| GET | /api/category/{id} | Get category by ID | ✅ |
| POST | /api/category | Create category | ✅ |
| PUT | /api/category/{id} | Update category | ✅ |
| DELETE | /api/category/{id} | Delete category | ✅ |

### Product
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | /api/product | Get all products (paginated + filters) | ✅ |
| GET | /api/product/{id} | Get product by ID | ✅ |
| GET | /api/product/category/{categoryId} | Get by category (paginated) | ✅ |
| POST | /api/product | Create product | ✅ |
| PUT | /api/product/{id} | Update product | ✅ |
| DELETE | /api/product/{id} | Delete product | ✅ |

### Cart
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | /api/cart | Get user cart | ✅ |
| POST | /api/cart | Add item to cart | ✅ |
| PUT | /api/cart | Update item quantity | ✅ |
| DELETE | /api/cart/removeitem | Remove item | ✅ |
| DELETE | /api/cart/clearcart | Clear entire cart | ✅ |

### Order
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | /api/order | Get user orders (paginated) | ✅ |
| GET | /api/order/{id} | Get order by ID | ✅ |
| POST | /api/order | Create order | ✅ |
| PUT | /api/order/{id} | Update order status | ✅ |

### Review
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | /api/review | Get reviews by user (paginated) | ✅ |
| GET | /api/review/{id} | Get reviews by product (paginated) | ✅ |
| POST | /api/review | Add review | ✅ |
| PUT | /api/review | Update review | ✅ |
| DELETE | /api/review/{id} | Delete review | ✅ |

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
- Role-based access control
- userId from JWT Claims — never from QueryString
- User-based data isolation
- Global Exception Handling prevents stack trace exposure

---

## 🧑‍💻 Author

**Mohammad Al-Mohammad**
Backend Developer – ASP.NET Core