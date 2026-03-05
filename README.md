# Global Logistics Asset Tracker (GLAT)

Technical Test – Full Stack Developer  
Lexart Tech – 2026

The goal of this project is to build a platform capable of managing logistics assets and monitoring telemetry data in real time.

The API specification is defined first using OpenAPI, and both the backend and frontend implementations are built based on this contract.

---

# Architecture Overview

The system is composed of three main components:

Backend API (.NET 9)  
Handles asset management, telemetry logs and sensor health calculations.

Frontend Dashboard (Blazor WebAssembly)  
Provides a user interface to visualize assets, telemetry logs and sensor status.

Database (SQL Server)  
Stores assets and telemetry information.

All services run inside Linux containers using Docker Compose.

---

# Tech Stack

Backend

- .NET 9 Web API
- Entity Framework Core
- SQL Server
- Native ANSI C module integrated using P/Invoke

Frontend

- Blazor WebAssembly
- MudBlazor UI components

Infrastructure

- Docker
- Docker Compose
- Linux containers

---

# Requirements

To run this project you need:

- Docker Desktop
- Docker Compose v2+

Optional for development:

- .NET 9 SDK

---

# Running the Project

All commands must be executed from the root directory of the repository.

1. Clone the repository

git clone https://github.com/RominaVera87/Lexart

2. Navigate to the project folder

cd Lexart

3. Build and start the containers

docker compose up -d --build

4. Verify containers are running

docker compose ps

You should see:

lexart-api  
lexart-sqlserver  
lexart-ui

---

# Important Note

SQL Server containers usually take longer to start than the API.

In some environments the backend may attempt to connect before SQL Server is ready, which causes a temporary connection failure.

If this happens simply run the command again:

docker compose up -d --build

This will restart the API container and the system will connect to the database correctly.

---

# Access the Application

Frontend dashboard  
http://localhost:5173

Swagger API documentation  
http://localhost:8080/swagger

---

# Database

SQL Server runs inside Docker.

The database LexartDb is created automatically when the API starts using Entity Framework Core migrations.

Connection string used internally by the API:

Server=sqlserver,1433  
Database=LexartDb  
User Id=sa  
Password=Lexart2026!  
TrustServerCertificate=True  
Encrypt=False

---

# Author

Romina Vera
