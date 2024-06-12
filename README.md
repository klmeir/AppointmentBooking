AppointmentBooking API with .NET

## Build & Run

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
