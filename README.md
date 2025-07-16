# 🧩 Microservice-Based Web API with ASP.NET Core 8

This project is a microservice-based Web API solution using **ASP.NET Core 8**, featuring:

- Two independent Web APIs:
  - **Users API**: PostgreSQL-backed service to manage users and subscriptions.
  - **Projects API**: MongoDB-backed service to manage user projects and charts.
- Dockerized with `docker-compose`.
- Includes **unit tests** and **integration test**.
- Supports querying the top 3 most-used indicators by subscription type.

---

## 📦 Technologies Used

- **ASP.NET Core 8 Web API**
- **Entity Framework Core (PostgreSQL)**
- **MongoDB.Driver**
- **Docker / Docker Compose**
- **xUnit / Moq**

---

## 🧪 Features

### ✅ Users API (PostgreSQL)
CRUD for:

- `Users`: `id`, `name`, `email`, `subscriptionId`
- `Subscriptions`: `id`, `type`, `startDate`, `endDate`

### ✅ Projects API (MongoDB)
CRUD for:

- `projects`: project per user with nested charts/indicators
- `user.settings`: language and theme per user

📈 **Analytics Endpoint**:

```http
GET /api/popularIndicators/{subscriptionType}
```

## 🧪 Running
```bash
docker-compose build --no-cache
docker-compose up
```

# 📁 Project Structure Overview (DDD)

This project follows **Domain-Driven Design (DDD)** principles and is organized into three main sections:

---

## 🗂️ SolutionFiles

This folder contains configuration files for the entire solution:

- `Directory.Build.props` – Centralized MSBuild properties for all projects.
- `docker-compose.yml` – Configuration for container orchestration.

---

## 📦 Src (8 projects)

The `Src` directory contains the **application logic** split into **bounded contexts** and layered according to DDD practices.

### 🔹 Projects Context

- **ProjectsApi** – API layer (controllers, endpoints) for handling external requests related to projects.
- **ProjectsApplication** – Application layer containing business use cases, services, and commands.
- **ProjectsDomain** – Domain layer with aggregates, entities, value objects, and domain services related to projects.

### 🔹 Shared Domain

- **SharedDomain** – Common domain types shared across multiple bounded contexts (e.g., value objects, enums, interfaces).

### 🔹 Users Context

- **UsersApi** – API layer for the users context.
- **UsersApplication** – Application services related to user management and interactions.
- **UsersDomain** – Domain logic for user-related functionality.
- **UsersDatabase** – Persistence infrastructure, possibly containing EF Core DbContexts, migrations, and configurations for Users.

---

## ✅ Tests (2 projects)

The `Tests` directory contains the test projects.

- **UsersIntegrationTests** – End-to-end or integration tests validating the interaction between components in the Users context.
- **UsersTests** – Unit tests focused on the domain and application layer of the Users context.

---

## ✅ DDD Layering Summary

Each bounded context (e.g., `Projects`, `Users`) is divided into:

- **API Layer** – Exposes HTTP endpoints and manages request/response lifecycles.
- **Application Layer** – Coordinates use cases and orchestrates domain logic.
- **Domain Layer** – Core business logic with rich domain models.
- **Infrastructure (optional)** – Handles persistence and external integrations (e.g., `UsersDatabase`).
