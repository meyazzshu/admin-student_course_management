# University Course Management System 🎓

A web-based university course management system designed to manage **student course registration, course enrollment, timetable management, subject withdrawal, and administrative course operations**.

The system includes separate functionality for **students and administrators**, with validation mechanisms to ensure that course registrations follow academic requirements.

## 📌 Project Overview

The **University Course Management System** was developed as an academic project using **ASP.NET Web Forms, C#, SQL Server, and ADO.NET**.

The system streamlines the course registration process by allowing students to register for courses, manage their enrolled subjects, view their timetable, and drop courses. Administrators can manage courses, lecturers, student accounts, and approve course registrations.

The system also implements academic validation such as **prerequisite checking, credit-hour validation, and timetable conflict detection**.

## ✨ Key Features

### 🎓 Student Features

* **Student Registration & Login**

  * Create and manage student accounts
  * Secure login and account access

* **Course Registration**

  * Browse available courses
  * Register for subjects
  * Validate course registration requirements

* **Course Enrollment**

  * Manage enrolled courses
  * View current registered subjects

* **Prerequisite Validation**

  * Check whether prerequisite subjects have been completed before registration

* **Credit Hour Validation**

  * Validate the number of registered credit hours

* **Subject Withdrawal**

  * Drop registered courses when required

* **Timetable Management**

  * Generate and view the student's timetable
  * Detect timetable conflicts between registered courses

### 🧑‍💼 Administrator Features

* **Admin Dashboard**

  * Centralized access to course management functions

* **Course Management**

  * Add and manage university courses
  * Maintain course information

* **Lecturer Management**

  * Assign lecturers to courses
  * Edit lecturer information

* **Registration Approval**

  * Review and approve student course registrations

* **Course Registration Management**

  * Manage student registration records

* **Account Management**

  * Manage system accounts and user information

## 🔄 System Workflow

### Student Workflow

```text
Student Registration / Login
            ↓
      Student Dashboard
            ↓
      Browse Courses
            ↓
    Course Registration
            ↓
 ┌──────────────────────┐
 │ Prerequisite Check   │
 │ Credit Hour Check    │
 │ Timetable Conflict   │
 └──────────────────────┘
            ↓
    Registration Approval
            ↓
       View Timetable
            ↓
     Drop Course (Optional)
```

### Administrator Workflow

```text
Admin Login
    ↓
Admin Dashboard
    ↓
├── Manage Courses
├── Manage Lecturers
├── Assign Lecturers
├── Manage Student Accounts
├── Manage Course Registrations
└── Approve Registrations
```

## 🏗️ Main System Modules

```text
University Course Management System
│
├── Student
│   ├── Registration
│   ├── Login
│   ├── Student Dashboard
│   ├── Course Registration
│   ├── Drop Course
│   └── View Timetable
│
└── Administration
    ├── Admin Dashboard
    ├── Manage Courses
    ├── Assign Lecturer
    ├── Edit Lecturer
    ├── Manage Accounts
    ├── Manage Course Registration
    └── Approve Registrations
```

## 🧩 System Architecture

The system follows a **three-tier architecture** to separate the application's presentation, business logic, and data access responsibilities.

```text
Presentation Layer
        ↓
Business Logic Layer
        ↓
Data Access Layer
        ↓
SQL Server Database
```

This structure helps organize the system into separate responsibilities and makes the application easier to maintain and manage.

## 🛠️ Technologies Used

| Technology            | Purpose                               |
| --------------------- | ------------------------------------- |
| **C#**                | Application logic and validation      |
| **ASP.NET Web Forms** | Web application development           |
| **SQL Server**        | Database management                   |
| **ADO.NET**           | Database connectivity and data access |
| **HTML & CSS**        | User interface and page styling       |
| **Visual Studio**     | Development environment               |

## 🎯 Project Objectives

The system was developed to:

* Simplify university course registration
* Allow students to manage their enrolled courses
* Automate prerequisite and credit-hour validation
* Detect timetable conflicts
* Support subject withdrawal
* Provide administrators with course and registration management tools
* Organize student, lecturer, course, and registration information

## 💡 Skills Demonstrated

* ASP.NET Web Forms development
* C# programming
* SQL Server database integration
* ADO.NET
* Three-tier architecture
* CRUD operations
* Business rule validation
* Course registration logic
* Database-driven application development
* Student and admin module development
* System analysis and problem-solving

---

**University Course Management System**
Academic Project | ASP.NET Web Forms • C# • SQL Server • ADO.NET
