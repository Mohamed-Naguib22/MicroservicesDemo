# 🧩 MicroservicesDemo

A comprehensive microservices-based system built with **.NET**, **RabbitMQ**, **Redis**, **Docker**, **MongoDB**, and **PostgreSQL**, showcasing **Clean Architecture**, **CQRS**, **MediatR**, **Event Sourcing**, **Vertical Slice Architecture (VSA)**, and robust **event-driven communication**.

This project serves as a practical and scalable example for learning and applying microservices architecture in the real world.

---

## 🏗️ Architecture Overview

This solution implements a **modular microservices architecture** using **RabbitMQ** for asynchronous communication between services. Each service is:

- **Independently deployable**
- **Containerized with Docker**
- **Communicates via events/messages**

Redis is used for **caching** and enhancing performance. The architecture follows the **Vertical Slice Architecture (VSA)** pattern for modular feature organization, and **Event Sourcing** is adopted for full traceability and auditability of key domain actions.

---

## ✅ Key Features

- ✅ Built with **.NET 9** and **Clean Architecture**
- ✅ Implements **CQRS** with **MediatR**
- ✅ Uses **Vertical Slice Architecture (VSA)** for modular design
- ✅ Supports **Event Sourcing** for traceable and replayable state
- ✅ Implements **Unit of Work** and **Repository Pattern**
- ✅ Uses **FluentValidation** for input validation
- ✅ Leverages **Minimal APIs** for lightweight endpoints
- ✅ Uses **RabbitMQ** for event-driven messaging
- ✅ Integrates **Redis** for caching
- ✅ Supports **MongoDB** and **PostgreSQL** for persistence
- ✅ Orchestrated with **Docker Compose**
- ✅ Logs structured data using **Serilog**
- ✅ Centralized **exception handling** via middleware

---

## 🧩 Microservices

| Service              | Tech Stack               | Database     | Mapper     | Description                                  |
|----------------------|--------------------------|--------------|------------|----------------------------------------------|
| **ProductService**   | ASP.NET Core Minimal API | PostgreSQL   | AutoMapper | Handles product catalog and operations       |
| **InventoryService** | ASP.NET Core API         | MongoDB      | Mapster    | Manages stock levels and inventory tracking  |

Each service follows the **Single Responsibility Principle**, and business logic is encapsulated in the **Application Layer** using **Vertical Slice Architecture** and **Clean Architecture** principles.

---

## 🐳 Docker Setup

### 📦 Prerequisites

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### ▶️ Running the System

```bash
docker-compose up --build
