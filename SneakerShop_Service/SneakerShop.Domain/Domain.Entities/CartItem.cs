namespace Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; private set; }
        public Guid CartId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid VariantId { get; private set; }
        public string ProductName { get; private set; }
        public string Color { get; private set; }
        public float Size { get; private set; }
        public decimal PriceAtAdd { get; private set; }
        public int Quantity { get; private set; }

        // Навигационное свойство
        public Cart Cart { get; private set; }

        private CartItem() { }

        public CartItem(
            Guid cartId,
            Guid productId,
            Guid variantId,
            string productName,
            float size,
            string color,
            decimal priceAtAdd,
            int quantity)
        {
            Id = Guid.NewGuid();
            CartId = cartId;
            ProductId = productId;
            VariantId = variantId;
            ProductName = productName;
            Size = size;
            Color = color;
            PriceAtAdd = priceAtAdd;
            SetQuantity(quantity);
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Количество должно быть больше нуля");
            Quantity = quantity;
        }

        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Количество для увеличения должно быть больше нуля");
            Quantity += amount;
        }

        public decimal GetTotalPrice() => PriceAtAdd * Quantity;

        public override string ToString()
        {
            return $"CartItem [Id: {Id}, Product: {ProductName}, Size: {Size}, Color: {Color}, Quantity: {Quantity}, PriceAtAdd: {PriceAtAdd}, Total: {GetTotalPrice()}]";
        }
    }
}