# EShopMicroservices

A microservices-based e-commerce application built with **.NET 8** and clean architecture principles. The solution demonstrates how to build scalable, maintainable microservices using CQRS, MediatR, and containerization with Docker.

## Solution Structure

```
EShopMicroservices/
├── src/
│   ├── BuildingBlocks/
│   │   └── BuildingBlocks/          # Shared CQRS abstractions & utilities
│   └── Services/
│       └── Catalog/
│           └── Catalog.API/         # Product catalog microservice
└── eshop-microservices.slnx         # Solution file
```

## Services

| Service | Description | Port (HTTP) | Port (HTTPS) |
|---------|-------------|-------------|--------------|
| [Catalog.API](src/Services/Catalog/Catalog.API/README.md) | Manages the product catalog | 5000 / 8080 | 5050 / 8081 |

## Shared Libraries

| Library | Description |
|---------|-------------|
| [BuildingBlocks](src/BuildingBlocks/BuildingBlocks/README.md) | Reusable CQRS interfaces and patterns |

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Runtime | .NET 8.0 |
| Web Framework | ASP.NET Core 8 |
| CQRS / Mediator | [MediatR 14](https://github.com/jbogard/MediatR) |
| Object Mapping | [Mapster 7](https://github.com/MapsterMapper/Mapster) |
| Containerization | Docker (multi-stage builds) |

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (optional, for container-based runs)

## Getting Started

### Run Locally

```bash
# Clone the repository
git clone https://github.com/ehsan78/EShopMicroservices.git
cd EShopMicroservices

# Restore dependencies
dotnet restore src/eshop-microservices.slnx

# Run the Catalog API
dotnet run --project src/Services/Catalog/Catalog.API/Catalog.API.csproj
```

The Catalog API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5050`

### Run with Docker

```bash
# Build the Catalog API image
docker build -f src/Services/Catalog/Catalog.API/Dockerfile -t catalog-api src/

# Run the container
docker run -p 8080:8080 -p 8081:8081 catalog-api
```

The Catalog API will be available at `http://localhost:8080`.

## Architecture

This solution follows a **vertical slice architecture** where each feature is self-contained. The CQRS pattern is applied using MediatR, separating read (Query) and write (Command) operations.

```
Request → Endpoint → MediatR → Handler → Response
                        ↑
                  ICommand / IQuery
                  (BuildingBlocks)
```

## License

This project is licensed under the [MIT License](LICENSE).
