namespace Domain.Entities
{
    public class Wishlist
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        private readonly List<WishlistItem> _items = new();
        public IReadOnlyCollection<WishlistItem> Items => _items.AsReadOnly();

        public int Count => _items.Count;

        // Приватный конструктор для EF Core
        private Wishlist() { }

        // Основной конструктор
        public Wishlist(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
        }

        public void AddItem(WishlistItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var exists = _items.Any(i => i.ProductId == item.ProductId &&
                                         i.ProductVariantId == item.ProductVariantId);
            if (exists)
                throw new InvalidOperationException("Товар уже в списке желаний");

            _items.Add(item);
        }

        public void RemoveItem(Guid wishlistItemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == wishlistItemId);
            if (item != null)
                _items.Remove(item);
        }

        public void RemoveProduct(Guid productId)
        {
            var itemsToRemove = _items.Where(i => i.ProductId == productId).ToList();
            foreach (var item in itemsToRemove)
                _items.Remove(item);
        }

        public bool ContainsProduct(Guid productId)
        {
            return _items.Any(i => i.ProductId == productId);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public override string ToString()
        {
            return $"Wishlist [Id: {Id}, UserId: {UserId}, ItemsCount: {_items.Count}]";
        }
    }
}