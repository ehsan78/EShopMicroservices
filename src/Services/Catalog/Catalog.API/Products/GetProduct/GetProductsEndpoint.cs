using Carter;
using Catalog.API.Models;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductResponse(IEnumerable<Product> Products);

    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
                {
                    GetProductQuery query = new GetProductQuery();
                    GetProductResult result = await sender.Send(query);
                    GetProductResponse response = result.Adapt<GetProductResponse>();
                    return Results.Ok(response);
                })
                .WithName("GetProducts")
                .Produces<GetProductResult>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Gets all products")
                .WithDescription("Retrieves a list of all products in the catalog");
        }
    }
}
