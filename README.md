# ATM Management System 🏧

A robust and scalable ATM management system built with .NET 8.0, implementing Clean Architecture principles and modern development practices.

## 🚀 Features

- **ATM Management**: Create, update, and monitor ATMs
- **Balance Operations**: Handle deposits and withdrawals
- **Real-time Monitoring**: Track ATM status and transactions
- **Secure Authentication**: JWT-based authentication and authorization
- **Comprehensive Logging**: Track all system operations
- **API Documentation**: Full Swagger/OpenAPI support

## 🏗 Architecture

The project follows Clean Architecture principles and is divided into four main layers:

📦 ATM.Management
┣ 📂 ATM.Domain
┣ 📂 ATM.Application
┣ 📂 ATM.Infrastructure
┗ 📂 ATM.Presentation.WebAPI

### Key Technologies

- **.NET 8.0**
- **Entity Framework Core**
- **PostgreSQL**
- **MediatR**
- **AutoMapper**
- **FluentValidation**
- **JWT Authentication**
- **Swagger/OpenAPI**

## 📐 Project Structure

- **ATM.Domain**: Contains enterprise logic and entities
- **ATM.Application**: Contains business logic and interfaces
- **ATM.Infrastructure**: Implements interfaces and contains data access logic
- **ATM.Presentation.WebAPI**: Contains API controllers and configuration

## 🔒 Security

- JWT-based authentication
- Role-based authorization
- Input validation
- Exception handling
- Secure communication
