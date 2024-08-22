# Library API

This project serves as a demonstration of a RESTful API, built with ASP.NET Core 8, Entity Framework Core, SQL Server, and Azure. Swagger is used to document and test the API endpoints.

| **Swagger UI** |
|:-------------------------:|
| <a target="_blank" rel="noreferrer"> <img src="https://github.com/g-s-c-code/LibraryApi/blob/main/Images/screenshot0.png"/> |

### Architecture
- **Presentation**: Interact with the API using Swagger UI.
- **Business Logic**: Handles operations such as creating a book entry and managing loans.
- **Data Access**: Uses Entity Framework Core for data management.

### Features
- **CRUD Operations**: Manage books, borrowers, and loans.
- **Data Validation**: Ensures data integrity.
- **Swagger Documentation**: Provides an easy way to understand and test API endpoints.

### Notes
- Please note that you will need to manually configure a database connection, as the database used during the build is not connected due to hosting costs.
