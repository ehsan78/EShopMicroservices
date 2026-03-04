namespace Catalog.API.Exception
{
    public class ProductNotFoundException(string errorMessage) 
        : System.Exception(errorMessage);
}
