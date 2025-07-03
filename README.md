# 🧩 MicroservicesDemo

A complete microservices-based system built using **.NET**, **RabbitMQ**, **Docker**, and **MongoDB/PostgreSQL**, demonstrating Clean Architecture, CQRS, MediatR, and Event-Driven communication. This project is ideal for learning microservices architecture in a practical and scalable manner.

---

## 🏗️ Architecture Overview

This project implements a **microservice architecture** with **event-driven communication** using RabbitMQ. Each service is independently deployable, containerized with Docker, and communicates through asynchronous messages.

### ✅ Key Features

- **.NET 8** with **Clean Architecture**
- **CQRS + MediatR** for separation of concerns
- **RabbitMQ** for event-driven messaging
- **MongoDB** and **PostgreSQL** for data persistence
- **Docker Compose** for orchestration
- **Serilog** for structured logging
- **Swagger** for API documentation
- **Exception handling** with middleware

---

## 🧩 Microservices

| Service              | Tech Stack         | Database     | Description                                  |
|----------------------|--------------------|--------------|----------------------------------------------|
| **ProductService**   | ASP.NET Core Web API | PostgreSQL   | Manages product-related operations           |
| **InventoryService** | ASP.NET Core Web API | MongoDB      | Manages inventory stock levels               |

Each service is isolated and follows **Single Responsibility Principle**, and all business logic is encapsulated inside Application Layer following **Clean Architecture** principles.

---

## 🔄 Communication

The services communicate via **RabbitMQ** using **publish/subscribe** patterns.

- `ProductCreatedEvent` is published by ProductService
- `InventoryService` listens and reacts to the event

This decouples services, promoting scalability and resilience.

---

## 🐳 Docker Setup

### Prerequisites

- Docker & Docker Compose

### Run the whole system

```bash
docker-compose up --build
