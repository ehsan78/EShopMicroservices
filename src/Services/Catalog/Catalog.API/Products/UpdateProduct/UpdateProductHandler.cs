namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        List<string> Category,
        string ImageFile
    ) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(Guid Id);

    public class UpdateProductCommandHandler (IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        // ReSharper disable once AsyncMethodWithoutAwait
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException($"Product with ID {command.Id} not found.");  
            }

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Category = command.Category;
            product.ImageFile = command.ImageFile;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(product.Id);

        }
    }

}
