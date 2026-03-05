using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(
        string Name,
        List<string> Category,
        string Description,
        decimal Price,
        string ImageFile
    );

    public record UpdateProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Define a route for updating a product
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
                {
                    UpdateProductCommand command = request.Adapt<UpdateProductCommand>();
                    UpdateProductResult product = await sender.Send(command);
                    return Results.Ok(new UpdateProductResponse(true));
                })
                .WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Updates an existing product")
                .WithDescription("Updates an existing product with the specified details")
                ;

        }
    }
}
