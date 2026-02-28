# BuildingBlocks Class Library

A shared class library that provides common cross-cutting abstractions and utilities used across the microservices in the EShopMicroservices solution.

---

## Overview

The **BuildingBlocks** library centralises reusable infrastructure concerns so that every microservice can depend on the same well-tested contracts instead of re-implementing them.  Currently it ships a full set of **CQRS** (Command Query Responsibility Segregation) interfaces built on top of [MediatR](https://github.com/jbogard/MediatR).

---

## Project Structure

```
BuildingBlocks/
└── CQRS/
    ├── ICommand.cs          # Command marker interfaces
    ├── ICommandHandler.cs   # Command handler interfaces
    ├── IQuery.cs            # Query marker interface
    └── IQueryHandler.cs     # Query handler interface
```

---

## CQRS Interfaces

### Commands

Commands represent an intent to **change state**.  Two overloads are provided:

| Interface | Description |
|-----------|-------------|
| `ICommand` | A command that returns no meaningful result (`MediatR.Unit`). |
| `ICommand<TResponse>` | A command that returns a typed response. |

```csharp
// A command with no return value
public record DeleteProductCommand(Guid ProductId) : ICommand;

// A command that returns a result
public record CreateProductCommand(string Name, decimal Price) : ICommand<CreateProductResult>;
```

### Command Handlers

| Interface | Description |
|-----------|-------------|
| `ICommandHandler<TCommand>` | Handles a `ICommand` (no return value). |
| `ICommandHandler<TCommand, TResponse>` | Handles a `ICommand<TResponse>` and returns `TResponse`. |

```csharp
public class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command, CancellationToken cancellationToken)
    {
        // implementation
        return new CreateProductResult(Guid.NewGuid());
    }
}
```

### Queries

Queries represent a request to **read state** without modifying it.

| Interface | Description |
|-----------|-------------|
| `IQuery<TResponse>` | A query that returns a non-null typed response. |

```csharp
public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;
```

### Query Handlers

| Interface | Description |
|-----------|-------------|
| `IQueryHandler<TQuery, TResponse>` | Handles a `IQuery<TResponse>` and returns `TResponse`. |

```csharp
public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(
        GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        // implementation
        return new GetProductByIdResult(product);
    }
}
```

---

## Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| [MediatR](https://www.nuget.org/packages/MediatR) | 14.x | In-process messaging / mediator pattern |
| [Mapster](https://www.nuget.org/packages/Mapster) | 7.x | High-performance object mapping |

---

## Getting Started

1. **Add a project reference** in your microservice's `.csproj`:

```xml
<ItemGroup>
  <ProjectReference Include="..\..\..\BuildingBlocks\BuildingBlocks\BuildingBlocks.csproj" />
</ItemGroup>
```

2. **Register MediatR** in your service's `Program.cs`:

```csharp
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
```

3. **Implement a command or query** using the interfaces from `BuildingBlocks.CQRS` and dispatch it via `ISender`/`IMediator`.

---

## Target Framework

- **.NET 8.0**
