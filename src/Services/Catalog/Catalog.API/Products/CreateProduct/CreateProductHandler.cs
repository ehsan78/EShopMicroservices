namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        string Description,
        decimal Price,
        List<string> Category,
        string ImageFile
    ) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
            
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty();
        }
    }
    
    public class CreateProductCommandHandler (IDocumentSession session,ILogger<CreateProductCommandHandler> logger)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        // ReSharper disable once AsyncMethodWithoutAwait
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
            // # add log

            logger.LogInformation("Creating product with name: {ProductName}, price: {Price}, categories: {Categories}",
                command.Name, command.Price, string.Join(", ", command.Category));
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                Price = command.Price,
                ImageFile = command.ImageFile
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);

        }
    }

}
