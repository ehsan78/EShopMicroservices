using Carter;
using Catalog.API.Models;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
                {
                    GetProductQuery query = new GetProductQuery();
                    GetProductResult result = await sender.Send(query);
                    GetProductsResponse response = result.Adapt<GetProductsResponse>();
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
