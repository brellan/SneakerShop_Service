using SneakerShop.Domain.Domain.Enums;
namespace SneakerShop.Domain.Domain.Entities;

public class ProductCatalog
{
    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public void RemoveProduct(Guid productId)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);
        if (product != null)
            _products.Remove(product);
    }

    public Product? GetProductById(Guid productId)
    {
        return _products.FirstOrDefault(p => p.Id == productId);
    }

    public List<Product> GetAllProducts()
    {
        return _products.ToList();
    }

    public List<Product> FilterProducts(ProductFilter filter)
    {
        return _products.Where(p =>
        {
            var variants = p.Variants.ToList();
            return filter.Matches(p, variants);
        }).ToList();
    }

    public List<Product> GetProductsByBrand(Guid brandId)
    {
        return _products.Where(p => p.BrandId == brandId).ToList();
    }

    public List<Product> GetProductsByPriceRange(decimal min, decimal max)
    {
        return _products.Where(p => p.BasePrice >= min && p.BasePrice <= max).ToList();
    }

    public List<Product> GetProductsBySize(Size size)
    {
        return _products.Where(p => p.GetAvailableSizes().Contains(size)).ToList();
    }
}