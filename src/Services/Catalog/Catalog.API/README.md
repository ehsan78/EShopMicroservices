# Catalog.API

The **Catalog API** is an ASP.NET Core microservice responsible for managing the product catalog in the EShopMicroservices solution. It exposes HTTP endpoints for creating and querying products.

## Overview

| Attribute | Value |
|-----------|-------|
| Framework | ASP.NET Core 8.0 |
| Architecture | Vertical Slice + CQRS |
| Default HTTP Port | 5000 (local), 8080 (Docker) |
| Default HTTPS Port | 5050 (local), 8081 (Docker) |

## Project Structure

```
Catalog.API/
├── Models/
│   ├── Product.cs           # Product entity
│   └── Category.cs          # Category entity
├── Products/
│   └── CreateProduct/
│       ├── CreateProductEndpoint.cs   # HTTP endpoint definition
│       └── CreateProductHandler.cs   # CQRS command handler
├── Properties/
│   └── launchSettings.json  # Launch profiles
├── appsettings.json         # Production configuration
├── appsettings.Development.json  # Development configuration
├── Dockerfile               # Multi-stage Docker build
└── Program.cs               # Application entry point
```

## Domain Models

### Product

Represents an item available for purchase in the catalog.

| Property | Type | Description |
|----------|------|-------------|
| `ProductId` | `Guid` | Unique identifier |
| `Name` | `string` | Product name |
| `Category` | `List<string>` | Category tags |
| `Description` | `string` | Product description |
| `ImageFile` | `string` | Image file name or path |
| `Price` | `decimal` | Product price |

### Category

Represents a product category.

| Property | Type | Description |
|----------|------|-------------|
| `CategoryId` | `int` | Unique identifier |
| `CategoryName` | `string` | Display name of the category |
| `CategoryDescription` | `string` | Description of the category |

## Features

### Create Product

Creates a new product in the catalog.

- **Handler**: `CreateProductHandler` — processes the `CreateProduct` command via MediatR.
- **Endpoint**: `CreateProductEndpoint` — maps the HTTP request to the command.

## Running the Service

### Locally

```bash
dotnet run --project src/Services/Catalog/Catalog.API/Catalog.API.csproj
```

Navigate to `http://localhost:5000`.

### With Docker

From the repository root:

```bash
docker build -f src/Services/Catalog/Catalog.API/Dockerfile -t catalog-api src/
docker run -p 8080:8080 -p 8081:8081 catalog-api
```

Navigate to `http://localhost:8080`.

## Launch Profiles

| Profile | URL | Notes |
|---------|-----|-------|
| `http` | `http://localhost:5000` | Standard HTTP |
| `https` | `https://localhost:5050` | HTTPS + HTTP |
| `IIS Express` | `http://localhost:58897` | IIS Express |
| `Container (Dockerfile)` | `http://localhost:8080` | Docker container |

## Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.VisualStudio.Azure.Containers.Tools.Targets` | 1.23.0 | Visual Studio Docker tooling |
| `BuildingBlocks` | (project ref) | CQRS interfaces |
