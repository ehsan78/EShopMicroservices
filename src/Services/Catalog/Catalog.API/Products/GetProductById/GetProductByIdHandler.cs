using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten.Linq.QueryHandlers;
using Catalog.API.Exception;

namespace Catalog.API.Products.GetProduct
{

    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    public class GetProductByIdHandler (IDocumentSession session, ILogger<GetProductByIdHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductByIdQuery");
            var product = await session.LoadAsync<Product>(query.ProductId, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException($"Product with id {query.ProductId} not found."); 
            }
            return new GetProductByIdResult(product);
        }
    }
}
