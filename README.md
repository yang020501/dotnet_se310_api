# dotnet_se310_api
.NET Backend for SE310 Project
## Required softwares to run this project
- .NET 6
- Visual Studio 2022
- Docker Desktop version > 3.8

## Run this project

### Initialize database
- run the docker compose file using
`
docker-compose up
`

### Migrate data to database
- open the project using Visual Studio
- Set up startup Project to DAL (right click on DAL on Solution Explorer and click "Set as Startup Project")
- Open Nuget Package Console (go to Tools -> Nuget Package Manager -> Console)
- In the Nuget Console, using this command to Start Data Migration
`Update-Database`

### Run the Project

- set Web as startup project (right click on Web on Solution Explorer and click "Set as Startup Project")
- Click Run button (green triangle icon on the top of the window) and see Swagger open in your default browser
