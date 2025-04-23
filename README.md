# Demo Project - ASP.NET Core MVC with User and Account Management

This project demonstrates an **ASP.NET Core MVC** application with complete **User Management**, **Role Management**, **Employee Management**, and **Account Management** functionality. It includes user profile management (CRUD operations) and account management for user registration, login, and password recovery. The project also incorporates role-based access control, employee and department management, **Google Authentication**, **Password Reset via Email/SMS**, and handling **User CRUD operations**.
The application follows important **design patterns** and **architectural patterns** to ensure maintainability, scalability, and clean code practices. **DTOs** (Data Transfer Objects) and **ViewModels** are used for data abstraction and structuring data in a way that suits the presentation layer.


---

## Features:

### **User Management** (Handled by `UserController`):
- **User Profile CRUD**:
  - **Create**, **Read**, **Update**, and **Delete** user profiles.
  - View and edit user details such as first name, last name, and phone number.
  - **List** all users with optional search by user name.
- **Assign Roles to Users**: Users can be assigned roles such as Admin, User, etc.
  
### **Account Management** (Handled by `AccountController`):
- **User Authentication**:
  - **Register** new users.
  - **Login** users (password-based and Google OAuth).
  - **Password Reset** functionality via email and SMS.
  - **Log out** users from the system.
  
### **Role Management** (Handled by `RoleController`):
- **Role CRUD**:
  - **Create**, **Read**, **Update**, and **Delete** roles.
  -   - **List** all users with optional search by role name.
  - Assign roles to users for permission-based access control.
  
### **Employee Management** (Handled by `EmployeeController`):
- **Employee CRUD**:
  - **Create**, **Read**, **Update**, and **Delete** employee profiles.
  - Each employee can be associated with a department.
  - **List** all employees with optional search by user name.
  - Manage employee details such as name, job title, and profile picture.

### **Department Management** (Handled by `DepartmentController`):
- **Department CRUD**:
  - **Create**, **Read**, **Update**, and **Delete** departments.
  - Employees can be linked to specific departments for organizational structure.
  
### **Google Login**:
- Integrates Google Authentication for seamless login using Google accounts.

---

## Technologies Used:

- **ASP.NET Core MVC**: Main framework for the web application.
- **Entity Framework Core**: ORM for database interactions.
- **Identity Framework**: For user authentication and role management.
- **Automapper**: For mapping between data models and view models.
- **SMTP/Email & SMS Service**: For sending password reset links via email and SMS.
- **Google OAuth**: For Google login integration.
- **Microsoft SQL Server**: Database for storing users, roles, employees, and departments.
- **MailKit**: For sending emails (e.g., password reset emails, user registration emails).
- **MimeKit**: For creating and parsing MIME messages (used with MailKit for email content handling).
- **Twilio**: For sending SMS messages (e.g., for password reset via SMS).
- **Proxies for Lazy Loading**: Implements proxy-based lazy loading for efficient data loading, particularly useful for relationships like **Employee-Department**.

---

## Design Patterns and Architectural Patterns:

### **Design Patterns:**
The application employs several **design patterns** to improve maintainability, flexibility, and separation of concerns:

- **Repository Pattern**:
  - This pattern abstracts the data access logic, providing a clean and consistent way to access the underlying data store while hiding the complexities of database queries.
  
- **Unit of Work Pattern**:
  - Ensures that all changes made during a transaction are committed as a single unit, maintaining data integrity. This pattern helps coordinate and manage multiple repository operations in a single transaction.
  
- **Dependency Injection**:
  - Facilitates loose coupling between classes, making it easier to manage and test the application's components. Services and repositories are injected into controllers, reducing direct dependencies and improving flexibility.

### **Architectural Patterns:**

- **N-Tier Architecture**:
  - The application follows an **N-tier architecture** with a clear separation of concerns. The different layers include:
    - **Presentation Layer**: Responsible for handling user interactions, typically through controllers and views.
    - **Business Logic Layer (BLL)**: Encapsulates the core business logic of the application.
    - **Data Access Layer (DAL)**: Manages data interaction with the database using **Entity Framework Core**.
    - **Service Layer**: Acts as an intermediary between the **BLL** and **DAL**, allowing more abstract and reusable service methods for controllers.

## Data Handling Patterns

### DTO (Data Transfer Object) Pattern
DTOs are used to transfer data between layers while maintaining a clear separation of concerns. They help prevent overexposure of entity models and simplify data transformation.

### ViewModel Pattern
ViewModels are tailored representations of data meant specifically for the presentation layer. They help structure and organize the data needed in views, often aggregating values from multiple sources.

---

## Getting Started:

### Prerequisites:
1. **.NET SDK** (version 8.0 or later).
2. **SQL Server** (or any other relational database that supports EF Core).
3. **Visual Studio or Visual Studio Code** with C# extension.

### Installation:

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/demo-project.git
