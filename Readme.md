# Ropey Web API

This application is the backend web API for the Ropey DvD rental service web application.
To run this application, make sure that you have [dotnet 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) and [MS SQL](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15) installed. After that, create a database called "Dvd_db" and run the following command on your shell. [^1]

```cmd
dotnet run
```

This should create all the tables and a super admin with the following details:

| Field | Value |
| ----------- | ----------- |
| Username | Young_mula_baby |
| Password | Mypassword1! |

[^1]: If you are not experienced with command line interface then we recommend that you use [Visual Studio](https://visualstudio.microsoft.com/downloads/) to open and run the application.
