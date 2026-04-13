using SneakerShop.Domain.Domain.Enums;
using SneakerShop.ValueObject;

namespace SneakerShop.Domain.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public Guid BrandId { get; private set; }
    public ProductName Name { get; private set; }
    public Description Description { get; private set; }
    public decimal BasePrice { get; private set; }
    public Currency Currency { get; private set; }
    public MainImageUrl MainImageUrl { get; private set; }
    public bool IsActive { get; private set; }

    public Brand Brand { get; private set; }
    private readonly List<ProductVariant> _variants = new();
    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();

    private Product() { }

    public Product(
        Guid brandId,
        ProductName name,
        Description description,
        decimal basePrice,
        Currency currency,
        MainImageUrl mainImageUrl)
    {
        Id = Guid.NewGuid();
        BrandId = brandId;
        Name = name;
        Description = description;
        SetBasePrice(basePrice);
        Currency = currency;
        MainImageUrl = mainImageUrl;
        IsActive = true;
    }

    public void SetBasePrice(decimal basePrice)
    {
        if (basePrice <= 0)
            throw new ArgumentException("Цена должна быть больше нуля");
        BasePrice = basePrice;
    }

    public void UpdateName(ProductName name) => Name = name;
    public void UpdateDescription(Description description) => Description = description;
    public void UpdateMainImage(MainImageUrl mainImageUrl) => MainImageUrl = mainImageUrl;

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public void AddVariant(ProductVariant variant) => _variants.Add(variant);
    public List<Size> GetAvailableSizes()
    {
        return _variants.Where(v => v.QuantityInStock > 0)
                       .Select(v => v.Size)
                       .Distinct()
                       .ToList();
    }

    public List<ProductVariant> GetAvailableVariants()
    {
        return _variants.Where(v => v.QuantityInStock > 0).ToList();
    }

    public ProductVariant? GetVariantBySize(Size size)
    {
        return _variants.FirstOrDefault(v => v.Size == size);
    }

    public List<string> GetAllImages()
    {
        var images = new List<string> { MainImageUrl.Value };
        foreach (var variant in _variants)
        {
            if (!string.IsNullOrWhiteSpace(variant.AdditionalImages.Value))
            {
                images.AddRange(variant.AdditionalImages.Value.Split(','));
            }
        }
        return images.Distinct().ToList();
    }

    public override string ToString()
    {
        return $"Product [Id: {Id}, Name: {Name}, BrandId: {BrandId}, BasePrice: {BasePrice} {Currency}]";
    }
}