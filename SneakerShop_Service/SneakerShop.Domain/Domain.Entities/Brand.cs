namespace Domain.Entities
{
    public class Brand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string LogoUrl { get; private set; }
        public string Description { get; private set; }

        // Навигационное свойство
        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private Brand() { } // Для EF Core

        public Brand(string name, string logoUrl, string description)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetLogoUrl(logoUrl);
            SetDescription(description);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название бренда не может быть пустым");
            Name = name;
        }

        public void SetLogoUrl(string logoUrl)
        {
            if (string.IsNullOrWhiteSpace(logoUrl))
                throw new ArgumentException("URL логотипа не может быть пустым");
            LogoUrl = logoUrl;
        }

        public void SetDescription(string description)
        {
            Description = description ?? string.Empty;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public override string ToString()
        {
            return $"Brand [Id: {Id}, Name: {Name}, LogoUrl: {LogoUrl}, Description: {Description?[..Math.Min(50, Description?.Length ?? 0)]}...]";
        }
    }
}