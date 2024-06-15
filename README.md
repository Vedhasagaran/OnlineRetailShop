Introduction
OnlineRetailShop is a sample e-commerce web application built using the Clean Architecture pattern in ASP.NET Core. This project demonstrates how to structure a .NET application for maintainability and testability by separating concerns across different layers.

Project Structure
The solution is organized into multiple projects, each representing a different layer of the Clean Architecture:
OnlineRetailShop
│
├── OnlineRetailShop.Api            # Presentation Layer
│   ├── Controllers
│   ├── Middleware
│   ├── Program.cs
│   ├── Startup.cs
│
├── OnlineRetailShop.Application    # Application Layer
│   ├── Interfaces
│   ├── Services
│
├── OnlineRetailShop.Domain         # Domain Layer
│   ├── Entities
│   ├── Interfaces
│
├── OnlineRetailShop.Infrastructure # Infrastructure Layer
│   ├── Data
│   ├── Repositories
│   ├── Exceptions
