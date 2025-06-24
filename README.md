# 🌍 Expense Tracker API  

## 📖 Overview  

The Expense Tracker API is a **secure and scalable RESTful API** built with **ASP.NET Core (.NET 8)** to help users manage their personal or team expenses. It allows users to **create, view, update, and delete expense records** (amount, category, description, date) securely. The API uses **JWT-based authentication** with **role-based access** for admins and regular users. Designed for a **C# Developer role test**, it demonstrates **best practices in API development, security, and scalability**, making it suitable for financial applications.  

---  

## 🛠 Technologies Used  

- **C#** – Core programming language for backend logic.  
- **ASP.NET Core 8** – Framework for building the REST API.  
- **Entity Framework Core** – ORM for database operations with SQLite.  
- **JWT (JSON Web Tokens)** – Authentication and authorization mechanism.  
- **BCrypt.Net** – Secure password hashing.  
- **Microsoft.Extensions.Caching.Memory** – In-memory caching for performance.  
- **SQLite** – Lightweight database for storing expenses and users.  
- **xUnit & Moq** – Unit testing framework and mocking library.  

---  

## 📌 Features  

✅ **CRUD Operations:** Create, read, update, and delete expense records.  
✅ **JWT Authentication:** Secure user login and registration with token-based access.  
✅ **Role-Based Authorization:** Admins manage all expenses; users manage their own.  
✅ **Security:** Password hashing, input sanitization, and HTTPS enforcement.  
✅ **Scalability:** Async operations, dependency injection, and caching.  
✅ **Unit Testing:** Basic tests for the expense controller.  
✅ **RESTful Design:** Follows REST principles for easy integration with front-end apps.  

---  

## 📂 Project Structure  

```
📁 ExpenseTrackerApi/
│── 📁 Controllers/           # API endpoints for expenses and authentication
│   │── AuthController.cs  
│   │── ExpensesController.cs  
│  
│── 📁 Models/                # Data models for entities
│   │── Expense.cs  
│   │── User.cs  
│  
│── 📁 Data/                  # Database context for Entity Framework
│   │── AppDbContext.cs  
│  
│── 📁 Services/              # Business logic and interfaces
│   │── IExpenseService.cs  
│   │── ExpenseService.cs  
│   │── IAuthService.cs  
│   │── AuthService.cs  
│  
│── 📁 DTOs/                  # Data transfer objects for API requests
│   │── ExpenseDto.cs  
│   │── RegisterDto.cs  
│   │── LoginDto.cs  
│  
│── 📁 Tests/                 # Unit tests for the API
│   │── ExpenseControllerTests.cs  
│  
│── appsettings.json          # Configuration settings (JWT, database)
│── Program.cs                # Application entry point
│── ExpenseTrackerApi.csproj  # Project file with dependencies
│── .gitignore                # Git ignore rules
│── README.md                 # Project documentation (this file)
```  

---  

## 🚀 How It Works  

### **1️⃣ Authentication (User Login & Registration)**  
- Users register with a username and password, which are securely stored (hashed with **BCrypt**).  
- Upon login, a **JWT token** is generated and must be included in the **Authorization header** for protected API calls.  

### **2️⃣ Expense Management (CRUD Operations)**  
- **Create:** Add a new expense (e.g., $50 for lunch).  
- **Read:** View all expenses (Admins see all; Users see their own) or a specific expense by ID.  
- **Update:** Modify an existing expense’s details.  
- **Delete:** Remove an expense (restricted by user role).  

### **3️⃣ Security & Scalability**  
- **Security:** Passwords are hashed, inputs are sanitized to prevent attacks (e.g., XSS), and HTTPS is enforced.  
- **Scalability:** Asynchronous database operations, in-memory caching, and dependency injection ensure efficient performance.  

---  

## 🏃 Running the Project  

### **1️⃣ Install Prerequisites**  
- Install **.NET 8 SDK**.  
- Use **Postman** or `curl` for testing API endpoints.  

### **2️⃣ Clone the Repository**  
```sh
git clone <your-repo-url>
cd ExpenseTrackerApi
```  

### **3️⃣ Restore Dependencies**  
```sh
dotnet restore
```  

### **4️⃣ Apply Database Migrations**  
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```  

### **5️⃣ Run the API**  
```sh
dotnet run
```  

The API will be available at **https://localhost:5001** (or your configured port).  

### **6️⃣ Test the API**  

**Register a User:**  
```http
POST https://localhost:5001/api/auth/register  
Content-Type: application/json  
{
    "username": "testuser",
    "password": "Password123!"
}
```  

**Login to Get JWT Token:**  
```http
POST https://localhost:5001/api/auth/login  
Content-Type: application/json  
{
    "username": "testuser",
    "password": "Password123!"
}
```  

**Create an Expense:**  
```http
POST https://localhost:5001/api/expenses  
Authorization: Bearer <jwt-token>  
Content-Type: application/json  
{
    "amount": 50.00,
    "category": "Food",
    "description": "Lunch",
    "date": "2025-06-24T10:00:00"
}
```  

### **7️⃣ Run Unit Tests**  
```sh
dotnet test
```  

---  

## 🔮 Future Enhancements  

🔹 **Front-End Integration:** Develop a web interface (e.g., using React or Angular) to visualize expenses.  
🔹 **Advanced Authentication:** Add OAuth2 support for third-party logins (e.g., Google).  
🔹 **Rate Limiting:** Implement throttling to prevent API abuse.  
🔹 **Cloud Deployment:** Deploy the API to **Azure** or **AWS** for production use.  
🔹 **Analytics:** Add endpoints for expense summaries (e.g., total by category or month).  
🔹 **Database Upgrade:** Switch to **PostgreSQL** or **SQL Server** for production-grade scalability.  

---  

## 📜 References  

- [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core)  
- [Entity Framework Core](https://learn.microsoft.com/ef/core)  
- [JWT Authentication in ASP.NET Core](https://learn.microsoft.com/aspnet/core/security/authentication/jwt)  
- [BCrypt.Net Documentation](https://github.com/BcryptNet/bcrypt.net)  
- [xUnit Testing Framework](https://xunit.net/)  

---  

## 📧 Contact  

**Author:** Siddartha Reddy Boreddy  
📍 **SUNY Binghamton**  
✉️ **Email:** sboreddy@binghamton.edu  

---  

### ⭐ If you find this project helpful, feel free to star the repository! 🚀  
