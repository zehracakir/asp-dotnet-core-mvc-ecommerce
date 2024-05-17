# E-Commerce Clothing Store Project

## Overview

This project is an e-commerce application built using [ASP.NET](https://asp.net/) Core MVC. The application focuses on selling clothes and allows users to view and order different types of clothing. The project implements a role-based authorization system where Admin and Kullanici (User) roles have different levels of access and functionalities. The application uses Entity Framework Core for database operations, follows the Repository Design Pattern for managing data access, and employs Dependency Injection for managing dependencies.

## Features

### User (Kullanici) Role:

- **View Clothing Types:** Users can browse through different types of clothing available on the platform.
- **Order Clothes:** Users can place orders for their desired clothing items.

### Admin Role:

- **Manage Clothing Types (KiyafetTuru):**
    - Add new clothing types.
    - Update existing clothing types.
    - Delete clothing types.
    - View all clothing types in a tabular format.
- **Manage Clothes (Kiyafet):**
    - Add new clothes with images.
    - Update existing clothes.
    - Delete clothes.
    - View all clothes.
    - Display the clothing type associated with each piece of clothing using a foreign key.
- **Order Management (SiparisVerme):**
    - Create orders based on user ID and clothing name.
    - View all orders.
    - Update existing orders.
    - Delete orders.

### General Features:

- **Authentication:** Implemented using [ASP.NET](http://asp.net/) Core Identity for managing user accounts and login functionality. Scaffolded Identity UI for easy setup and customization.
- **Authorization:** Implemented role-based authorization to differentiate between Admin and User functionalities.
- **Frontend:** Basic styling and structure for user-friendly navigation.
- **Error Handling:** Added validations and user error checks on forms to ensure data integrity.
- **Notifications:** Used TempData to display alerts for the results of CRUD operations.
- **Dependency Injection:** Used to manage service lifetimes and dependencies across the application.

## Getting Started

### Prerequisites

- .NET Core SDK
- SQL Server

### Installation

1. Clone the repository:
    
    ```
    git clone <https://github.com/zehracakir/asp-dotnet-core-mvc-ecommerce.git>
    cd asp-dotnet-core-mvc-ecommerce
    
    ```
    
2. Setup the database:
    - Update the connection string in `appsettings.json` to match your SQL Server configuration using Windows Authentication.
        
        ```json
        "ConnectionStrings": {
            "DefaultConnection": "Server=your_server;Database=your_database;Integrated Security=True;"
        }
        
        ```
        
    - Run the migrations to create the database and tables:
        
        ```
        dotnet ef database update
        
        ```
        
3. Run the application:
    
    ```
    dotnet run
    
    ```
    

## Usage

### Admin Operations:

- **Manage Clothing Types:** Navigate to the relevant section from the admin dashboard to add, update, delete, and view clothing types.
- **Manage Clothes:** Use the admin dashboard to add new clothes, update existing ones, delete clothes, and view the entire catalog.
- **Order Management:** Admin can create, update, and delete orders based on user ID and clothing item.

### User Operations:

- **Browse Clothes:** Users can browse the available clothing types and items.
- **Place Orders:** Users can select clothing items and place orders.

## Project Structure

- **Models:** Contains the entity classes like `KiyafetTuru` and `Kiyafet`.
- **Controllers:** Handles the logic for managing views, processing user inputs and place orders.
    - `KiyafetTuruController`: Manages clothing type-related operations.
    - `KiyafetController`: Manages clothing-related operations.
    - `SiparisVermeController`: Manages orders operations.
- **Views:** Contains the Razor views for displaying the UI.
    - `KiyafetTuruEkle`: View for adding new clothing types.
    - `KiyafetEkleGuncelle`: View for adding and updating clothes.
- **Data:** Includes the `ApplicationDbContext` for EF Core and repository classes for data access.
    - `KiyafetRepository`: Manages data operations for the `Kiyafet` entity.
    - `IKiyafetRepository`: Interface for `KiyafetRepository`.

---
