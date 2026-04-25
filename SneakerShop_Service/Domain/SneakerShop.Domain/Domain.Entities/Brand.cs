using SneakerShop.Domain.Base;
using SneakerShop.Domain.Enums;
using SneakerShop.ValueObjects;

namespace SneakerShop.Domain.Entities;

public class Brand : Entity<Guid>
{
    public BrandName Name { get; private set; }
    public string LogoUrl { get; private set; }
    public Description Description { get; private set; }

    private readonly ICollection<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.ToList().AsReadOnly();

    protected Brand() { }

    public Brand(Guid id, BrandName name, string logoUrl, Description description) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        LogoUrl = logoUrl ?? throw new ArgumentNullException(nameof(logoUrl));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public Product AddProduct(ProductName name, Description description, Price basePrice,
        Currency currency, string mainImageUrl)
    {
        var product = new Product(Guid.NewGuid(), this, name, description, basePrice, currency, mainImageUrl);
        _products.Add(product);
        return product;
    }

    internal void AddProduct(Product product)
    {
        if (!_products.Contains(product))
            _products.Add(product);
    }

    public override string ToString()
    {
        return $"{Name.Value} (Id: {Id}, Products: {_products.Count})";
    }
}