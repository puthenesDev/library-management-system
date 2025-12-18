# 📚 Library Management System

A clean, modular Library Management System built with ASP.NET Core, CQRS + MediatR, and Entity Framework Core.  
This project demonstrates domain-driven design, repository pattern, and unit testing with Moq.

## 🚀 Features
- Manage Libraries, Books, Members, and Loans
- Borrow and return workflows with validation
- Update/delete operations across entities
- Unit tests using xUnit + Moq
- RESTful API with clear contracts

## 🛠️ Tech Stack
- .NET 9 / ASP.NET Core Web API
- Entity Framework Core
- CQRS + MediatR
- xUnit + Moq
- PostgreSQL

## 📂 Project Structure
LibraryManagement/ 
├── Domain/ # Entities & repository interfaces 
├── Application/ # Commands, queries, handlers, DTOs 
├── Infrastructure/ # EF Core DbContext & repository implementations 
├── Api/ # Controllers (REST endpoints) 
└── Tests/ # Unit tests with Moq
└── src/ ├── database/ # SQL scripts for schema & seed data 
└── Postman/ # Postman collection for API testing

## ⚡ How to Run
1. `dotnet restore`
2. `dotnet run --project LibraryManagement.Api`
3. API base URL: `https://localhost:7135/swagger`

## 🔗 API Endpoints

### Libraries
- POST `/api/libraries` — create
- GET `/api/libraries/{id}` — get one
- PUT `/api/libraries/{id}` — update
- DELETE `/api/libraries/{id}` — delete

### Books
- POST `/api/books` — create
- GET `/api/books/{id}` — get one
- GET `/api/books` — list all
- PUT `/api/books/{id}` — update
- DELETE `/api/books/{id}` — delete

### Members
- POST `/api/members` — create
- GET `/api/members/{id}` — get one
- GET `/api/members` — list all
- PUT `/api/members/{id}` — update
- DELETE `/api/members/{id}` — delete

### Loans
- POST `/api/loans` — borrow (create loan)
- POST `/api/loans/{id}/return` — return a book
- GET `/api/loans/{id}` — get loan
- PUT `/api/loans/{id}` — update loan due date
- DELETE `/api/loans/{id}` — delete loan

## 🧪 Testing
Run tests: `dotnet test`

## 👨‍💻 Author
Built by Putheneswaran.
