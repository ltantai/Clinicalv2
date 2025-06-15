Architecture structure of the Clinical project on the Back-end side

/Clinical
│
Clinical.API             --> ASP.NET Core Web API (Presentation Layer)
│
Clinical.Application     --> Application logic, DTOs, interfaces, use cases
│
Clinical.Domain          --> Entity classes, domain interfaces, enums
│
Clinical.Infrastructure  --> EF Core DbContext, repositories, data access logic
│
Clinical.Persistence     --> Database context implementation, migrations
│
Clinical.Tests           --> Unit and integration tests
│
Clinical.sln             --> Solution file



Install dotnet-ef CLI Tool
        install dotnet ef command
   + dotnet tool install --global dotnet-ef
        if installed then update
   + dotnet tool update --global dotnet-ef
        Check dotnet ef version
   + dotnet ef --version

Add migration command
   + dotnet ef migrations add InitialCreate --project Clinical.Persistence --startup-project Clinical.API

Update Database command
   + dotnet ef database update --project Clinical.Persistence --startup-project Clinical.API