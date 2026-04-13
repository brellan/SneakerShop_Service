using SneakerShop.Domain.Domain.Enums;
using SneakerShop.ValueObject;

namespace SneakerShop.Domain.Domain.Entities;

public class ProductVariant
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Size Size { get; private set; }
    public Color Color { get; private set; }
    public Sku Sku { get; private set; }
    public int QuantityInStock { get; private set; }
    public AdditionalImages AdditionalImages { get; private set; }

    public Product Product { get; private set; }

    private ProductVariant() { }

    public ProductVariant(
        Guid productId,
        Size size,
        Color color,
        Sku sku,
        int quantityInStock,
        AdditionalImages additionalImages = null)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Size = size;
        Color = color;
        Sku = sku;
        SetQuantityInStock(quantityInStock);
        AdditionalImages = additionalImages ?? new AdditionalImages(null);
    }

    public void SetQuantityInStock(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Количество на складе не может быть отрицательным");
        QuantityInStock = quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть больше нуля");
        if (QuantityInStock - quantity < 0)
            throw new InvalidOperationException("Недостаточно товара на складе");
        QuantityInStock -= quantity;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть больше нуля");
        QuantityInStock += quantity;
    }

    public bool IsInStock() => QuantityInStock > 0;

    public override string ToString()
    {
        return $"ProductVariant [Id: {Id}, ProductId: {ProductId}, Size: {Size}, Color: {Color}, SKU: {Sku}, InStock: {QuantityInStock}]";
    }
}