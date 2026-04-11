namespace Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        private readonly List<CartItem> _items = new();
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        // Навигационное свойство
        public User User { get; private set; }

        public decimal TotalPrice => _items.Sum(item => item.PriceAtAdd * item.Quantity);

        private Cart() { }

        public Cart(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
        }

        public void AddItem(CartItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var existingItem = _items.FirstOrDefault(i =>
                i.ProductId == item.ProductId && i.VariantId == item.VariantId);

            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(item.Quantity);
            }
            else
            {
                _items.Add(item);
            }
        }

        public void RemoveItem(Guid cartItemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == cartItemId);
            if (item != null)
                _items.Remove(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void UpdateQuantity(Guid cartItemId, int newQuantity)
        {
            var item = _items.FirstOrDefault(i => i.Id == cartItemId);
            if (item != null)
                item.SetQuantity(newQuantity);
        }

        public override string ToString()
        {
            if (!_items.Any())
                return $"Cart [Id: {Id}, UserId: {UserId}, Empty]";

            return $"Cart [Id: {Id}, UserId: {UserId}, ItemsCount: {_items.Count}, TotalPrice: {TotalPrice:C}]";
        }
    }
}