using SneakerShop.Domain.Base;
using SneakerShop.Domain.Exceptions;
using SneakerShop.ValueObjects;

namespace SneakerShop.Domain.Entities;

public class Cart : Entity<Guid>
{
    public User User { get; private set; }

    private readonly ICollection<CartItem> _items = [];
    public IReadOnlyCollection<CartItem> Items => _items.ToList().AsReadOnly();

    public TotalPrice TotalPrice => new TotalPrice(_items.Sum(item => item.PriceAtAdd.Value * item.Quantity.Value));

    protected Cart() { }

    public Cart(User user)
        : this(Guid.NewGuid(), user) { }

    protected Cart(Guid id, User user)
        : base(id)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
    }

    public CartItem AddItem(User user, ProductVariant variant, Quantity? quantity = null)
    {
        if (User != user)
            throw new AnotherUserEditCartException(this, user.Id);

        if (variant == null) throw new ArgumentNullException(nameof(variant));
        quantity ??= new Quantity(1);

        if (!variant.Product.IsActive)
            throw new ProductNotActiveException(variant.Product);

        if (variant.QuantityInStock.Value < quantity.Value)
            throw new InsufficientStockException(variant, quantity.Value);

        var existingItem = _items.FirstOrDefault(i => i.ProductVariant?.Id == variant.Id);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(new Quantity(existingItem.Quantity.Value + quantity.Value));
            return existingItem;
        }

        var item = new CartItem(
            this,
            variant,
            variant.Product.ProductName,
            variant.Size,
            variant.Color,
            variant.Product.BasePrice,
            quantity);

        _items.Add(item);
        return item;
    }

    public void RemoveItem(User user, Guid cartItemId)
    {
        if (User != user)
            throw new AnotherUserEditCartException(this, user.Id);

        var item = _items.FirstOrDefault(i => i.Id == cartItemId);
        if (item == null)
            throw new CartItemNotFoundException(cartItemId);

        _items.Remove(item);
    }

    public void UpdateItemQuantity(User user, Guid cartItemId, Quantity newQuantity)
    {
        if (User != user)
            throw new AnotherUserEditCartException(this, user.Id);

        if (newQuantity.Value <= 0)
        {
            RemoveItem(user, cartItemId);
            return;
        }

        var item = _items.FirstOrDefault(i => i.Id == cartItemId);
        if (item == null)
            throw new CartItemNotFoundException(cartItemId);

        if (item.ProductVariant.QuantityInStock.Value < newQuantity.Value)
            throw new InsufficientStockException(item.ProductVariant, newQuantity.Value);

        item.UpdateQuantity(newQuantity);
    }

    public void Clear(User user)
    {
        if (User != user)
            throw new AnotherUserEditCartException(this, user.Id);

        _items.Clear();
    }

    public override string ToString()
    {
        return $"Cart {Id} User: {User.Nickname.Value} Items: {_items.Count} Total: {TotalPrice.Value}";
    }
}