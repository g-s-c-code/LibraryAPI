# Library API

This project serves as a demonstration of a RESTful API, built with ASP.NET Core 8, Entity Framework Core, SQL Server, and Azure. Swagger is used to document and test the API endpoints.

## Preview (click to enlarge)
| **Swagger UI** |
|:-------------------------:|
| <a target="_blank" rel="noreferrer"> <img src="https://github.com/g-s-c-code/LibraryApi/blob/main/Images/screenshot0.png" width="300" height="700"/> |

## Architecture
The API is structured around a three-layered architecture:

- **Presentation Layer**: Swagger UI presents a user-friendly interface for interacting with the API, allowing users to perform CRUD operations on books, borrowers, and loans.
- **Business Logic Layer**: Handles operations such as creating a new book entry, validating borrower information, and managing the loaning process.
- **Data Access Layer**: Utilizes Entity Framework Core to interact with SQL Server, providing a robust and scalable solution for data management.

## Features
- **CRUD Operations**: Full Create, Read, Update, and Delete functionality for books, borrowers, and loans.
- **Data Transfer Objects (DTOs)**: Structured data handling with DTOs for each entity.
- **Data Validation**: Validates input data to ensure data integrity.
- **Swagger Documentation**: Interactive documentation to easily test and understand API endpoints.
- **Entity Framework Core**: Code-first database approach with migrations support.

## How to Use
1. **Explore the API**: Use Swagger UI to explore the available endpoints.
2. **CRUD Operations**: Add, retrieve, update, and delete records for books, borrowers, and loans through the respective endpoints.
3. **Testing**: Send requests and receive responses directly through Swagger UI.

### Important Note on Database Connection:
For security reasons, the connection string to the database is not included in this project. Users need to manually configure the database connection, either by setting up SQL Server locally or connecting to an Azure SQL Database. Instructions for setting up and configuring the database can be found in the project's documentation.

### Note:
The API is a proof of concept and serves as a portfolio piece to demonstrate API development capabilities with ASP.NET Core 8. Future enhancements may include additional features such as authorization, more complex business rules, and advanced querying capabilities.