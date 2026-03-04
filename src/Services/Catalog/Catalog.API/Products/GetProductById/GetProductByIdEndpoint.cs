using Carter;
using Catalog.API.Models;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
                {
                    GetProductByIdQuery query = new GetProductByIdQuery(id);
                    var result = await sender.Send(query);
                    GetProductByIdResponse response = result.Adapt<GetProductByIdResponse>();
                    return Results.Ok(response);
                })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Gets a product by ID")
                .WithDescription("Retrieves a product from the catalog by its ID");
        }
    }
}
