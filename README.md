# Global Logistics Asset Tracker (GLAT)

Technical Test – Full Stack Developer  
Lexart Tech – 2026

This project implements a simplified asset tracking system for a logistics company using a **Spec-Driven Development (SDD)** approach.

The API contract is defined first using OpenAPI and then used as the basis for backend implementation and frontend integration.

The system allows managing assets, storing telemetry data and calculating an asset **Health Score** using a native module written in ANSI C.

---

# Tech Stack

Backend

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Native ANSI C library (Health Score)
- P/Invoke

Frontend

- Blazor WebAssembly
- MudBlazor

Infrastructure

- Docker
- Docker Compose

---

# Architecture

The backend follows a Clean Architecture style structure:

- **Assets.Domain** – Domain entities and core logic
- **Assets.Application** – Application interfaces and use cases
- **Assets.Infrastructure** – Database access and integrations
- **Assets.Api** – HTTP API entrypoint
- **Assets.Contracts** – DTOs generated from the OpenAPI specification

---

# API Specification

The API contract is defined in:
