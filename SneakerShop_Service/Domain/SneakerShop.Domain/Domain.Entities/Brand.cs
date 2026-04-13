using SneakerShop.ValueObject;

namespace SneakerShop.Domain.Domain.Entities;

public class Brand
{
    public Guid Id { get; private set; }
    public BrandName Name { get; private set; }
    public LogoUrl LogoUrl { get; private set; }
    public Description Description { get; private set; }

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Brand() { }

    public Brand(BrandName name, LogoUrl logoUrl, Description description)
    {
        Id = Guid.NewGuid();
        Name = name;
        LogoUrl = logoUrl;
        Description = description;
    }

    public void UpdateName(BrandName name) => Name = name;
    public void UpdateLogo(LogoUrl logoUrl) => LogoUrl = logoUrl;
    public void UpdateDescription(Description description) => Description = description;

    public void AddProduct(Product product) => _products.Add(product);

    public override string ToString()
    {
        return $"Brand [Id: {Id}, Name: {Name}]";
    }
}