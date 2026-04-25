using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Exceptions;

public class VariantNotBelongProductException(ProductVariant variant, Product product)
    : InvalidOperationException($"Variant {variant.Sku} (id: {variant.Id}) does not belong to product {product.ProductName} (id: {product.Id})")
{
    public ProductVariant Variant => variant;
    public Product Product => product;
}