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
    public StockQuantity QuantityInStock { get; private set; }
    public ICollection<string> AdditionalImages { get; private set; }

    protected ProductVariant()
    {
        AdditionalImages = [];
    }

    public ProductVariant(
        Product product,
        Size size,
        Color color,
        Sku sku,
        StockQuantity quantityInStock,
        ICollection<string>? additionalImages = null)
        : this(Guid.NewGuid(), product, size, color, sku, quantityInStock, additionalImages) { }

    protected ProductVariant(Guid id,
        Product product,
        Size size,
        Color color,
        Sku sku,
        StockQuantity quantityInStock,
        ICollection<string>? additionalImages = null)
        : base(id)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Size = size ?? throw new ArgumentNullException(nameof(size));
        Color = color ?? throw new ArgumentNullException(nameof(color));
        Sku = sku ?? throw new ArgumentNullException(nameof(sku));
        QuantityInStock = quantityInStock ?? throw new ArgumentNullException(nameof(quantityInStock));
        AdditionalImages = additionalImages ?? [];
    }

    public bool RemoveStock(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount to remove must be positive", nameof(amount));

        if (QuantityInStock.Value < amount)
            throw new InsufficientStockException(this, amount);

        QuantityInStock = new StockQuantity(QuantityInStock.Value - amount);
        return true;
    }

    public override string ToString()
    {
        return $"{Sku.Value} Size: {Size.Value} Color: {Color.Value} Stock: {QuantityInStock.Value} (Id: {Id}, Product: {Product.ProductName.Value})";
    }
}