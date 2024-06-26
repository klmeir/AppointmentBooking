AppointmentBooking API with .NET

# The main architectural patterns and styles that guide this solution are 
* Port and Adapter Architecture
* CQRS (Command Query Responsibility Segregation)

# Technical specifications:
* Ready to containerize with Docker.
* Entity Framework Core 6 
* Generic Repository (very useful with aggregate management)
* Shadow Properties on entities: Properties that are added to domain entities without "poisoning" the entity's own definition in that layer.
* Automatic Domain Services injection using "[DomainService]" annotation.
* MediaTR : register command handlers and queries automatically (via reflection does scan of the assembly)
* Global Exception Handler
* Unit (Domain) tests with XUnit
* NSubstitute for Mocking
* Logs : Console
* Swagger

### Project structure:
Solution for VisualStudio(.sln) composed of the following folders :

* Api : Api Rest, entry point of the application
* Application : Domain Services Orchestration Layer; Ports, Commands, Queries, Handlers
* Infrastructure : Adapters
* Domain : Entities, Value Objects, Ports, Domain Services, Aggregates
* Domain.Tests : Unit Tests for Domain Services

## Build & Run

# Visual Studio 2022
To run the project open the solution in visual studio, check the database connection string and run the script to create tables, sp and test data

# Docker and Docker Compose

The execution of docker compose from visual studio is functional, at the moment we are working to execute it from command line...

In progress...
To startup the whole solution, execute the following command:

```
docker-compose build --no-cache
docker-compose up -d
```

Then the following containers should be running on `docker ps`:

| Application 	            | URL                                                                                  |
|--------------------       | ------------------------------------------------------------------------------------ |
| AppointmentBooking API 	  | https://localhost:8080                                                               |
| SQL Server 	              | Server=localhost;User Id=sa;Password=<YourStrong!Passw0rd>;Database=ReservaTurnos;   |

Connect to the SQL Server and run the script Database/GenerateTurns.sql

Browse to [http://localhost:8000](http://localhost:8000) and view the swagger documentation
