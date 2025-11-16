# C# Project: SOLID Architecture, API Design, GraphQL Integration, Automated Testing

This project demonstrates a clean, production-aligned C# solution that follows SOLID principles and modern backend engineering practices. It showcases API design, GraphQL integration, automated testing, and a layered architecture structure similar to the enterprise environments used across retail, logistics, and large-scale distributed systems.  
The goal is to highlight senior-level coding habits, thoughtful design decisions, and clear separation of concerns.

---

## What This Project Includes, and Why

### ✔ Multi-layer architecture designed to enforce SOLID
The folder structure uses well-defined boundaries between API, Application, Core, and Infrastructure. This encourages loose coupling, testability, dependency inversion, and clean business logic modelling. It reflects the style used by large engineering teams to keep systems maintainable over years, not months.

### ✔ Real GraphQL client implementation  
The `CSharp.Infrastructure` layer includes a strongly typed GraphQL client that performs queries via `GraphQLHttpClient`.  
This demonstrates experience integrating with distributed services, working with schemas, handling responses, mapping dynamic data, and applying clean domain models.

### ✔ Extensible API Layer (`CSharp.Api`)
The API exposes an endpoint that validates an order and optionally retrieves external data.  
It highlights controller design, dependency injection, domain-first design, and clean request handling.

### ✔ Application logic separated from transport concerns
All business rules live inside `CSharp.Application`.  
This prevents controllers from becoming “fat” and keeps business logic testable and reusable.

### ✔ Core domain layer modelling real business concepts
The Core layer contains domain models and interfaces.  
This keeps the system independent of infrastructure, external libraries, or frameworks.

### ✔ Automated test suite (`CSharp.Tests`)
The test suite uses `xUnit`.  
It demonstrates TDD thinking, validator testing, and mocking through interfaces.

### ✔ Foundation for Kafka or RabbitMQ integration
The Infrastructure folder includes stubs and extension points for message producers.  
This shows readiness for event-driven and distributed workflows.

### ✔ Strong engineering practices
The project demonstrates:
- Clean namespace conventions  
- SOLID applied in practice  
- Interface-driven design  
- Domain-first modelling  
- Separation of concerns  
- Testable, decoupled components  

---

## Architecture Overview

         +---------------------+
         |      API Layer      |
         |   CSharp.Api        |
         +----------+----------+
                    |
                    v
    +---------------------------------------+
    |            Application Layer          |
    |        CSharp.Application             |
    +------------------+--------------------+
                       |
                       v
           +-----------------------+
           |       Core Layer      |
           |    CSharp.Core        |
           +-----------+-----------+
                       ^
                       |
    +---------------------------------------+
    |          Infrastructure Layer         |
    |        CSharp.Infrastructure          |
    +---------------------------------------+

             ^                         ^
             |                         |
             +-------------------------+
                   Tests Layer
               CSharp.Tests


## SOLID Definitions

                     ┌────────────────────────────────┐
                     │        S O L I D Principles    │
                     └────────────────────────────────┘
                                      │
                                      │
      ┌──────────────────────────────────────────────────────────────────┐
      │                            S — Single Responsibility             │ 
      │                                                                  │ 
      │ Each class handles one well-defined concern.                     │
      │                                                                  │
      │ Example in this project:                                         │
      │   CSharp.Application.Services.BasicOrderValidator                │
      │   → Only validates orders. No API logic. No data fetching.       │
      └──────────────────────────────────────────────────────────────────┘

      ┌──────────────────────────────────────────────────────────────────┐
      │                     O — Open/Closed Principle                    │
      │                                                                  │ 
      │ Classes are open for extension, closed for modification.         │
      │                                                                  │
      │ Example in this project:                                         │
      │   Add new validators (PromoOrderValidator, HighValueValidator)   │
      │   without modifying BasicOrderValidator.                         │
      └──────────────────────────────────────────────────────────────────┘

      ┌──────────────────────────────────────────────────────────────────┐
      │                  L — Liskov Substitution Principle               │
      │                                                                  │       
      │ Any implementation of an interface must behave as expected.      │
      │                                                                  │
      │ Example in this project:                                         │
      │   All validators implement IOrderValidator and can be swapped    │
      │   without breaking API endpoints.                                │
      └──────────────────────────────────────────────────────────────────┘

      ┌──────────────────────────────────────────────────────────────────┐
      │               I — Interface Segregation Principle                │
      │                                                                  │       
      │ Prefer multiple small interfaces over one large one.             │
      │                                                                  │
      │ Example in this project:                                         │
      │   IOrderValidator                                                │
      │   IGraphQLOrderClient                                            │
      │   IOrderEventProducer                                            │
      │   → Each interface focuses on a single behaviour.                │
      └──────────────────────────────────────────────────────────────────┘

      ┌──────────────────────────────────────────────────────────────────┐
      │            D — Dependency Inversion Principle                    │
      │                                                                  │       
      │ High-level code depends on abstractions, not concrete classes.   │
      │                                                                  │
      │ Example in this project:                                         │
      │   API layer depends on IOrderValidator (abstraction),            │
      │   not BasicOrderValidator (concrete).                            │
      │                                                                  │
      │   GraphQL client injected via interface.                         │
      │   Kafka event producer injected via interface.                   │
      └──────────────────────────────────────────────────────────────────┘

      ---

## SOLID Principles Explained

### **S — Single Responsibility Principle**
Each class has exactly one reason to change.  
**Example:**  
`BasicOrderValidator` validates orders only.  
No API logic or data access concerns.

---

### **O — Open Closed Principle**
Components are open for extension but closed for modification.  
**Example:**  
Add `PromoOrderValidator` or `HighValueOrderValidator` without editing existing validators.

---

### **L — Liskov Substitution Principle**
Implementations of an interface should be replaceable without breaking behaviour.  
**Example:**  
All validators implement `IOrderValidator` and can be swapped at runtime or in tests.

---

### **I — Interface Segregation Principle**
Small, focused interfaces improve clarity and maintainability.  
**Examples:**  
- `IOrderValidator`  
- `IGraphQLOrderClient`  
- `IOrderEventProducer`  

---

### **D — Dependency Inversion Principle**
High-level modules depend on abstractions, not concrete implementations.  
**Examples:**  
- API injects an `IOrderValidator`  
- GraphQL clients and event producers injected via interfaces  

This keeps components loosely coupled and easier to test.

## How to Run the Project and See It in Action

Follow the steps below to run the API, execute GraphQL calls, and run the automated tests.

---

## 1. Install Requirements

### ✔ .NET SDK 10 or later  
Verify with:

```bash
dotnet --version

dotnet restore

dotnet build

# Run the API  
dotnet run --project src/CSharp.Api

http://localhost:<ACTIVE_PORT>/swagger

# Run the NBomber Load Tests;
dotnet run --project CSharp.LoadTests