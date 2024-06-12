AppointmentBooking API with .NET

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
