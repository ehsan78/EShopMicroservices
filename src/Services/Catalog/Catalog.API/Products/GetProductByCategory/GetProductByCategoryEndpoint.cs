using Carter;
using Catalog.API.Models;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
                {
                    GetProductByCategoryQuery query = new GetProductByCategoryQuery(category);
                    var result = await sender.Send(query);
                    GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();
                    return Results.Ok(response);
                })
                .WithName("GetProductByCategory")   
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Gets products by Category")
                .WithDescription("Retrieves all products from the catalog by category");
        }
    }
}
