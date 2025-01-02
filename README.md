# LibraryAPI

This repository contains a solution demonstrating two approaches for building a RESTful API: one using Entity Framework Core and the other using Dapper. Both projects are fully functional, interchangeable, and configured with Swagger for API documentation and testing. Each project functions independently, allowing users to select based on preference or requirements. Both projects also utilize .NET Aspire for orchestration and telemetry capture, enabling performance benchmarking between the two implementations.

## Projects

### 1. Entity Framework Core Based API

This project demonstrates a RESTful API built with ASP.NET Core 8, Entity Framework Core and SQL Server. Swagger is utilized to document and test the API endpoints.

#### Architecture
- **Presentation**: Interact with the API using Swagger UI.
- **Business Logic**: Handles operations such as creating a book entry and managing loans.
- **Data Access**: Uses Entity Framework Core for data management.

#### Features
- **CRUD Operations**: Manage books, borrowers, and loans.
- **Data Validation**: Ensures data integrity.
- **Swagger Documentation**: Provides an easy way to understand and test API endpoints.

#### Notes
- You will need to manually configure a database connection, as the database used during the build is not connected due to hosting costs.

| **Entity Framework Core** |
|:-------------------------:|
| <a target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/g-s-c-code/LibraryAPI/main/Images/efcore.png" width="400px" /> |

<br>

### 2. Dapper Based API

This project demonstrates a RESTful API built with ASP.NET Core 8, Dapper and SQL Server. It serves as a lightweight alternative to the EF Core-based API, providing a streamlined approach to data access.

#### Architecture
- **Presentation**: Interact with the API using Swagger UI.
- **Business Logic**: Similar operations as the EF Core-based API but implemented with Dapper.
- **Data Access**: Uses Dapper for data management.

#### Features
- **CRUD Operations**: Manage books, borrowers, and loans.
- **Data Validation**: Ensures data integrity.
- **Swagger Documentation**: Provides an easy way to understand and test API endpoints.

#### Notes
- As with the EF Core-based API, you will need to manually configure a database connection due to the absence of a connected database.

| **Dapper** |
|:-------------------------:|
| <a target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/g-s-c-code/LibraryAPI/main/Images/dapper.png" width="400px" /> |

<br>

## Usage

You can choose to run either project depending on your needs:

1. **Entity Framework Core Based API**: Navigate to the `LibraryAPI.EFCore` project and set it as the startup project.
2. **Dapper Based API**: Navigate to the `LibraryAPI.Dapper` project and set it as the startup project.

Both projects are fully configured to run standalone and include Swagger for API documentation and testing. Ensure you have the necessary database configurations set up in the `appsettings.json` file for the chosen project.

## Getting Started

1. Clone the repository:
    ```bash
    git clone https://github.com/g-s-c-code/LibraryAPI.git
    ```

2. Navigate to the desired project directory:
    ```bash
    cd LibraryAPI/EFCore
    # or
    cd LibraryAPI/Dapper
    ```

3. Install the required dependencies:
    ```bash
    dotnet restore
    ```

4. Create and configure `appsettings.json` with your database connection (nested in 'root').

5. Run the project:
    ```bash
    dotnet run
    ```

6. Access Swagger UI at `http://localhost:5000/swagger` (port may vary).