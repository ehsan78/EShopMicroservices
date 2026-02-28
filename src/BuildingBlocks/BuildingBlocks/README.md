# BuildingBlocks

The **BuildingBlocks** library is a shared class library providing reusable CQRS (Command Query Responsibility Segregation) abstractions for use across all microservices in the EShopMicroservices solution.

## Overview

| Attribute | Value |
|-----------|-------|
| Framework | .NET 8.0 |
| Purpose | Shared CQRS interfaces and utilities |
| Key Dependencies | MediatR 14, Mapster 7 |

## Project Structure

```
BuildingBlocks/
└── CQRS/
    ├── ICommand.cs          # Command marker interfaces
    ├── ICommandHandler.cs   # Command handler interfaces
    ├── IQuery.cs            # Query marker interface
    └── IQueryHandler.cs     # Query handler interface
```

## CQRS Abstractions

This library wraps [MediatR](https://github.com/jbogard/MediatR) with strongly-typed interfaces that enforce a clear separation between commands (write operations) and queries (read operations).

### Commands

A **command** represents a write operation that changes state. Commands may or may not return a response value.

```csharp
// Command with no return value (returns Unit)
public interface ICommand : ICommand<Unit> { }

// Command with a return value
public interface ICommand<out TResponse> : IRequest<TResponse> { }
```

#### ICommandHandler

```csharp
// Handle a command with no return value
public interface ICommandHandler<in TCommand>
    : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand { }

// Handle a command with a return value
public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull { }
```

### Queries

A **query** represents a read operation that returns data without modifying state.

```csharp
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull { }
```

#### IQueryHandler

```csharp
public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull { }
```

## Usage

### 1. Reference the library

Add a project reference to `BuildingBlocks` in your microservice `.csproj`:

```xml
<ItemGroup>
  <ProjectReference Include="..\..\..\BuildingBlocks\BuildingBlocks\BuildingBlocks.csproj" />
</ItemGroup>
```

### 2. Define a command

```csharp
using BuildingBlocks.CQRS;

public record CreateProductCommand(string Name, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid ProductId);
```

### 3. Implement the handler

```csharp
using BuildingBlocks.CQRS;

public class CreateProductHandler
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command, CancellationToken cancellationToken)
    {
        // business logic here
        var id = Guid.NewGuid();
        return new CreateProductResult(id);
    }
}
```

### 4. Define a query

```csharp
using BuildingBlocks.CQRS;

public record GetProductByIdQuery(Guid ProductId)
    : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Guid ProductId, string Name, decimal Price);
```

### 5. Implement the query handler

```csharp
using BuildingBlocks.CQRS;

public class GetProductByIdHandler
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(
        GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        // data retrieval logic here
        return new GetProductByIdResult(query.ProductId, "Sample Product", 9.99m);
    }
}
```

## Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| [MediatR](https://github.com/jbogard/MediatR) | 14.0.0 | In-process messaging and request/response pipeline |
| [Mapster](https://github.com/MapsterMapper/Mapster) | 7.4.0 | Fast, convention-based object mapping |
