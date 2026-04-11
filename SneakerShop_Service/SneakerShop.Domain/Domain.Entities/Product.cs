using Domain.Enums;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public Guid BrandId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal BasePrice { get; private set; }
        public Currency Currency { get; private set; }
        public string MainImageUrl { get; private set; }
        public bool IsActive { get; private set; }

        // Навигационные свойства
        public Brand Brand { get; private set; }
        private readonly List<ProductVariant> _variants = new();
        public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();

        private Product() { }

        public Product(
            Guid brandId,
            string name,
            string description,
            decimal basePrice,
            Currency currency,
            string mainImageUrl)
        {
            Id = Guid.NewGuid();
            BrandId = brandId;
            SetName(name);
            SetDescription(description);
            SetBasePrice(basePrice);
            Currency = currency;
            SetMainImageUrl(mainImageUrl);
            IsActive = true;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название товара не может быть пустым");
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description ?? string.Empty;
        }

        public void SetBasePrice(decimal basePrice)
        {
            if (basePrice <= 0)
                throw new ArgumentException("Цена должна быть больше нуля");
            BasePrice = basePrice;
        }

        public void SetMainImageUrl(string mainImageUrl)
        {
            if (string.IsNullOrWhiteSpace(mainImageUrl))
                throw new ArgumentException("URL изображения не может быть пустым");
            MainImageUrl = mainImageUrl;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void AddVariant(ProductVariant variant)
        {
            _variants.Add(variant);
        }

        public override string ToString()
        {
            return $"Product [Id: {Id}, Name: {Name}, BrandId: {BrandId}, BasePrice: {BasePrice} {Currency}, IsActive: {IsActive}, VariantsCount: {_variants.Count}]";
        }
    }
}