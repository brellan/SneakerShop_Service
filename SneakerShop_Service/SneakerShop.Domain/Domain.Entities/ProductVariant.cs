namespace Domain.Entities
{
    public class ProductVariant
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public float Size { get; private set; }
        public string Color { get; private set; }
        public string Sku { get; private set; }
        public int QuantityInStock { get; private set; }
        public string AdditionalImages { get; private set; }

        // Навигационное свойство
        public Product Product { get; private set; }

        private ProductVariant() { }

        public ProductVariant(
            Guid productId,
            float size,
            string color,
            string sku,
            int quantityInStock,
            string additionalImages = null)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Size = size;
            SetColor(color);
            SetSku(sku);
            SetQuantityInStock(quantityInStock);
            AdditionalImages = additionalImages;
        }

        public void SetColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Цвет не может быть пустым");
            Color = color;
        }

        public void SetSku(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU не может быть пустым");
            Sku = sku;
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
}