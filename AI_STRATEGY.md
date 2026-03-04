# AI_STRATEGY

## 1. Tools used

- ChatGPT: used to validate the OpenAPI contract, reason about Clean Architecture boundaries, and troubleshoot Docker / EF Core / P/Invoke issues.
- GitHub Copilot: used for small code completions and repetitive boilerplate while implementing repositories/controllers.

## 2. Prompt log (examples)

### OpenAPI / REST validation

Prompt:
"Review this OpenAPI spec and check REST consistency, status codes (200/201/204/400/401/403/404/409/500), naming conventions and schema alignment."

Outcome:
Helped refine the contract before generating code.

### NSwag generation

Prompt:
"Given this assets-api.yaml, how do I generate DTOs and controllers for a .NET Web API using NSwag?"

Outcome:
Configured generation and connected generated contracts to the implementation.

### Docker + SQL Server (Linux containers)

Prompt:
"Provide a docker-compose setup for SQL Server in a Linux container and a .NET API container, and explain how connection strings should differ between running locally vs inside Docker."

Outcome:
Unblocked networking and environment configuration.

### P/Invoke + native C library

Prompt:
"Show a minimal ANSI C shared library and the corresponding .NET P/Invoke signature, including how to load a .so inside a Linux Docker container."

Outcome:
Implemented the native Health Score library and integrated it with the backend.

## 3. Efficiency & quality

AI helped reduce time spent searching documentation and trial-and-error, especially for Docker networking, NSwag wiring, and P/Invoke details.

Quality was ensured by:

- compiling and running the solution end-to-end (API + DB + native library) using Docker,
- verifying the OpenAPI contract via Swagger,
- testing key endpoints (CRUD, telemetry flow, and /api/native/healthscore).
