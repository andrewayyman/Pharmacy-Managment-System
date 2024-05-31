# Pharmacy Management System

A comprehensive Pharmacy Management System built with ASP.NET Core, Entity Framework Core, JWT authentication, and other modern technologies to facilitate the management of medicines, categories, patients, and requests.

## Project Description

The Pharmacy Management System provides an online dashboard for managing medicines, categories, and patients. It caters to two types of users: Admin and Patient. Admin users have full control over the system, including managing medicines, categories, patients, and requests. Patients can view available medicines, send requests for required medicines, and track their request history.

## Requirements

- Admin and Patient authentication for accessing the dashboard.
- Admin functionalities:
  - Manage medicines (CRUD).
  - Manage categories of medicines (CRUD).
  - Manage Patients (CRUD).
  - Accept or decline requests by the Patient (CRUD).
  - Show the history of patient requests for medicines.
- Patient functionalities:
  - View and filter medicines.
  - Send a request to the admin for required medicines.
  - Show history of medicine searches related to the patient's account.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- JWT Authentication
- SQL Server
- LINQ
- Dependency Injection
- Generic Repository Design Pattern
- Swagger (Swashbuckle)
- Postman

## Features

- Authentication and Authorization for Admin and Patient users.
- CRUD operations for managing medicines, categories, and patients.
- Request functionality for patients to request medicines.
- History tracking for patient requests and medicine searches.

## Database Models

The project includes a database design schema to efficiently store and manage medicines, categories, patients, and requests.


## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/andrewayyman/Pharmacy-Managment-System.git
