var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LibraryAPI_EFCore>("libraryapi-efcore");

builder.AddProject<Projects.LibraryAPI_Dapper>("libraryapi-dapper");

builder.Build().Run();
