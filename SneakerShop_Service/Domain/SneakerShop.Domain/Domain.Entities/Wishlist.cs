using SneakerShop.Domain.Base;

namespace SneakerShop.Domain.Entities;

public class Wishlist : Entity<Guid>
{
    public User User { get; private set; }

    private readonly ICollection<WishlistItem> _items = [];
    public IReadOnlyCollection<WishlistItem> Items => _items.ToList().AsReadOnly();

    protected Wishlist() { }

    public Wishlist(User user)
        : this(Guid.NewGuid(), user) { }

    protected Wishlist(Guid id, User user)
        : base(id)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
    }

    public WishlistItem AddItem(User user, ProductVariant variant)
    {
        if (User != user)
            throw new InvalidOperationException($"User {user.Nickname.Value} cannot edit wishlist belonging to another user");

        if (variant == null) throw new ArgumentNullException(nameof(variant));

        var existingItem = _items.FirstOrDefault(i => i.ProductVariant?.Id == variant.Id);
        if (existingItem != null)
            return existingItem;

        var item = new WishlistItem(this, variant, variant.Product);
        _items.Add(item);
        return item;
    }

    public void RemoveItem(User user, Guid wishlistItemId)
    {
        if (User != user)
            throw new InvalidOperationException($"User {user.Nickname.Value} cannot edit wishlist belonging to another user");

        var item = _items.FirstOrDefault(i => i.Id == wishlistItemId);
        if (item == null)
            throw new InvalidOperationException($"Wishlist item with id {wishlistItemId} not found");

        _items.Remove(item);
    }

    public void RemoveItemByVariant(User user, Guid variantId)
    {
        if (User != user)
            throw new InvalidOperationException($"User {user.Nickname.Value} cannot edit wishlist belonging to another user");

        var item = _items.FirstOrDefault(i => i.ProductVariant?.Id == variantId);
        if (item != null)
            _items.Remove(item);
    }

    public bool ContainsVariant(User user, Guid variantId)
    {
        if (User != user)
            throw new InvalidOperationException($"User {user.Nickname.Value} cannot view wishlist belonging to another user");

        return _items.Any(i => i.ProductVariant?.Id == variantId);
    }

    public override string ToString()
    {
        return $"Wishlist {Id} User: {User.Nickname.Value} Items: {_items.Count}";
    }
}