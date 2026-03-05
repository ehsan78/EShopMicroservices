using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id);    

    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Define a route for deleting a product
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
                {
                    DeleteProductCommand command = new DeleteProductCommand(id);
                    DeleteProductResult product = await sender.Send(command);
                    return Results.Ok(new DeleteProductResponse(product.IsSuccess));
                })
                .WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Deletes an existing product")
                .WithDescription("Deletes an existing product with the specified ID ")
                ;

        }
    }
}
