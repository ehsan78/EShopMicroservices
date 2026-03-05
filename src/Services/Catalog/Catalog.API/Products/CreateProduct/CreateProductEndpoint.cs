using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(
        string Name,
        List<string> Category,
        string Description,
        decimal Price,
        string ImageFile
    );

    public record CreateProductResponse(
        Guid Id
    );
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Define a route for creating a product
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
                {
                    CreateProductCommand command = request.Adapt<CreateProductCommand>();
                    CreateProductResult product = await sender.Send(command);
                    return Results.Created($"/products/{product.Id}", product);
                })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Creates a new product")
                .WithDescription("Creates a new product with the specified details")
                ;

        }
    }
}
