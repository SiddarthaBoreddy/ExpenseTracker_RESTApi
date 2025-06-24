# Expense Tracker API

## Overview
The Expense Tracker API is a RESTful API built with ASP.NET Core (.NET 8) for managing personal or team expenses. It supports CRUD operations for expenses (amount, category, description, date) and uses JWT-based authentication with role-based authorization (User and Admin roles). The project demonstrates secure and scalable practices suitable for a C# Developer role.

## Features
- **CRUD Operations**: Create, read, update, and delete expenses.
- **Authentication**: JWT-based authentication with user registration and login.
- **Authorization**: Role-based access (Admins can manage all expenses; Users manage their own).
- **Security**: Password hashing (BCrypt), input sanitization, HTTPS enforcement.
- **Scalability**: Async/await, dependency injection, in-memory caching.
- **Database**: SQLite for simplicity.
- **Testing**: Basic unit tests for the ExpensesController.

## Prerequisites
- .NET 8 SDK
- SQLite (included via EF Core)
- Postman or curl for testing API endpoints

## Setup Instructions
1. Clone the repository:
   ```bash
   git clone <your-repo-url>
   cd ExpenseTrackerApi