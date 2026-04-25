using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Exceptions;

public class ProductNotActiveException(Product product)
    : InvalidOperationException($"Product {product.ProductName} (id: {product.Id}) is not active")
{
    public Product Product => product;
}