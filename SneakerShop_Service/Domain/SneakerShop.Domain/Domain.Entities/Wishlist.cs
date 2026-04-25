using SneakerShop.Domain.Base;

namespace SneakerShop.Domain.Entities;

public class Wishlist : Entity<Guid>
{
    public Guid UserId { get; private set; }

    private readonly ICollection<WishlistItem> _items = [];
    public IReadOnlyCollection<WishlistItem> Items => _items.ToList().AsReadOnly();

    protected Wishlist() { }

    public Wishlist(Guid id, Guid userId) : base(id)
    {
        UserId = userId;
    }

    public WishlistItem AddItem(Guid userId, ProductVariant variant)
    {
        if (UserId != userId)
            throw new InvalidOperationException($"User {userId} cannot edit wishlist belonging to another user");

        if (variant == null) throw new ArgumentNullException(nameof(variant));

        var existingItem = _items.FirstOrDefault(i => i.ProductVariant?.Id == variant.Id);
        if (existingItem != null)
            return existingItem;

        var item = new WishlistItem(Guid.NewGuid(), this, variant, variant.Product.Id);
        _items.Add(item);
        return item;
    }

    public void RemoveItem(Guid userId, Guid wishlistItemId)
    {
        if (UserId != userId)
            throw new InvalidOperationException($"User {userId} cannot edit wishlist belonging to another user");

        var item = _items.FirstOrDefault(i => i.Id == wishlistItemId);
        if (item == null)
            throw new InvalidOperationException($"Wishlist item with id {wishlistItemId} not found");

        _items.Remove(item);
    }

    public void RemoveItemByVariant(Guid userId, Guid variantId)
    {
        if (UserId != userId)
            throw new InvalidOperationException($"User {userId} cannot edit wishlist belonging to another user");

        var item = _items.FirstOrDefault(i => i.ProductVariant?.Id == variantId);
        if (item != null)
            _items.Remove(item);
    }

    public bool ContainsVariant(Guid userId, Guid variantId)
    {
        if (UserId != userId)
            throw new InvalidOperationException($"User {userId} cannot view wishlist belonging to another user");

        return _items.Any(i => i.ProductVariant?.Id == variantId);
    }

    public override string ToString()
    {
        return $"Wishlist {Id} UserId: {UserId} Items: {_items.Count}";
    }
}