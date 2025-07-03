# 🧩 MicroservicesDemo

A complete microservices-based system built using **.NET**, **RabbitMQ**, **Redis**, **Docker**, **MongoDB**, and **PostgreSQL**, demonstrating Clean Architecture, CQRS, MediatR, and Event-Driven communication. This project is ideal for learning microservices architecture in a practical and scalable manner.

---

## 🏗️ Architecture Overview

This project implements a **microservice architecture** with **event-driven communication** using RabbitMQ. Each service is independently deployable, containerized with Docker, and communicates through asynchronous messages. Redis is integrated for caching and message queuing where applicable.

### ✅ Key Features

- **.NET 9** with **Clean Architecture**
- **CQRS + MediatR** for separation of concerns
- **Unit of Work** and **Repository Pattern** for transactional data access and abstraction
- **FluentValidation** for robust and reusable validation logic
- **Minimal APIs** used for lightweight and expressive HTTP endpoints
- **RabbitMQ** for event-driven messaging
- **Redis** for caching and fast data access
- **MongoDB** and **PostgreSQL** for data persistence
- **Docker Compose** for orchestration
- **Serilog** for structured logging
- **Exception handling** with middleware

---

## 🧩 Microservices

| Service              | Tech Stack               | Database     | Description                                  |
|----------------------|--------------------------|--------------|----------------------------------------------|
| **ProductService**   | ASP.NET Core Minimal API | PostgreSQL   | Manages product-related operations           |
| **InventoryService** | ASP.NET Core Minimal API | MongoDB      | Manages inventory stock levels               |

Each service is isolated and follows the **Single Responsibility Principle**. Business logic is encapsulated inside the Application Layer using **Clean Architecture**.

---

## ✅ Validation

All input models are validated using **FluentValidation**, ensuring that only valid data proceeds to the business logic layer. Validators are registered automatically and integrated into the request pipeline.

---

## 🔄 Communication

The services communicate via **RabbitMQ** using **publish/subscribe** patterns:

- `Events` is published by **ProductService**
- **InventoryService** listens and updates inventory accordingly

**Redis** is used for caching frequently accessed data to improve performance and scalability.

---

## 🐳 Docker Setup

### Prerequisites

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Run the whole system

```bash
docker-compose up --build
