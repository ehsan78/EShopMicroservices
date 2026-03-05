using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProduct
{

    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    public class GetProductByCategoryHandler (IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductByCategoryQuery");
            var products = await session.Query<Product>()
                .Where(x=> x.Category.Contains(query.Category))
                .ToListAsync(cancellationToken);

            if(!products.Any())
            {
                throw new ProductNotFoundException($"Product with id {query.Category} not found."); 
            }
            return new GetProductByCategoryResult(products);
        }
    }
}
