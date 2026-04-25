using SneakerShop.Domain.Base;
using SneakerShop.Domain.Enums;
using SneakerShop.Domain.Exceptions;
using SneakerShop.ValueObjects;

namespace SneakerShop.Domain.Entities;

public class Product : Entity<Guid>
{
    public Brand Brand { get; private set; }
    public ProductName ProductName { get; private set; }
    public Description Description { get; private set; }
    public Price BasePrice { get; private set; }
    public Currency Currency { get; private set; }
    public string MainImageUrl { get; private set; }
    public bool IsActive { get; private set; } = true;

    private readonly ICollection<ProductVariant> _variants = [];
    public IReadOnlyCollection<ProductVariant> Variants => _variants.ToList().AsReadOnly();

    protected Product() { }

    public Product(Guid id, Brand brand, ProductName name, Description description,
        Price basePrice, Currency currency, string mainImageUrl, bool isActive = true) : base(id)
    {
        Brand = brand ?? throw new ArgumentNullException(nameof(brand));
        ProductName = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        BasePrice = basePrice ?? throw new ArgumentNullException(nameof(basePrice));
        Currency = currency;
        MainImageUrl = mainImageUrl ?? throw new ArgumentNullException(nameof(mainImageUrl));
        IsActive = isActive;
    }

    public ProductVariant AddVariant(Size size, Color color, Sku sku, int quantityInStock,
        List<string>? additionalImages = null)
    {
        if (!IsActive) throw new ProductNotActiveException(this);

        var variant = new ProductVariant(Guid.NewGuid(), this, size, color, sku, quantityInStock,
            additionalImages ?? []);
        _variants.Add(variant);
        return variant;
    }

    internal void AddVariant(ProductVariant variant)
    {
        if (!_variants.Contains(variant))
            _variants.Add(variant);
    }

    public override string ToString()
    {
        return $"{ProductName.Value} {BasePrice.Value} {Currency} Status: {(IsActive ? "Active" : "Inactive")} (Id: {Id}, Brand: {Brand.Name.Value}, Variants: {_variants.Count})";
    }
}