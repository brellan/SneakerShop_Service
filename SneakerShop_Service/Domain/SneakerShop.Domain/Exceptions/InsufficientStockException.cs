using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Exceptions;

public class InsufficientStockException(ProductVariant variant, int requestedQuantity)
    : InvalidOperationException($"Insufficient stock for variant {variant.Sku}. Requested: {requestedQuantity}, Available: {variant.QuantityInStock}")
{
    public ProductVariant Variant => variant;
    public int RequestedQuantity => requestedQuantity;
}