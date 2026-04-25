using SneakerShop.Domain.Base;
using SneakerShop.Domain.Exceptions;
using SneakerShop.ValueObjects;

namespace SneakerShop.Domain.Entities;

public class ProductVariant : Entity<Guid>
{
    public Product Product { get; private set; }
    public Size Size { get; private set; }
    public Color Color { get; private set; }
    public Sku Sku { get; private set; }
    public int QuantityInStock { get; private set; }
    public ICollection<string> AdditionalImages { get; private set; }

    protected ProductVariant()
    {
        AdditionalImages = [];
    }

    public ProductVariant(Guid id, Product product, Size size, Color color, Sku sku,
        int quantityInStock, ICollection<string> additionalImages) : base(id)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Size = size ?? throw new ArgumentNullException(nameof(size));
        Color = color ?? throw new ArgumentNullException(nameof(color));
        Sku = sku ?? throw new ArgumentNullException(nameof(sku));

        if (quantityInStock < 0)
            throw new ArgumentException("Quantity in stock cannot be negative", nameof(quantityInStock));

        QuantityInStock = quantityInStock;
        AdditionalImages = additionalImages ?? [];
    }

    public void RemoveStock(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount to remove must be positive", nameof(amount));

        if (QuantityInStock < amount)
            throw new InsufficientStockException(this, amount);

        QuantityInStock -= amount;
    }

    public override string ToString()
    {
        return $"{Sku.Value} Size: {Size.Value} Color: {Color.Value} Stock: {QuantityInStock} (Id: {Id}, Product: {Product.ProductName.Value})";
    }
}