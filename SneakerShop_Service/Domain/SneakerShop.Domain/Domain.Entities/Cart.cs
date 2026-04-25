using SneakerShop.Domain.Base;
using SneakerShop.Domain.Exceptions;

namespace SneakerShop.Domain.Entities;

public class Cart : Entity<Guid>
{
    public Guid UserId { get; private set; }

    private readonly ICollection<CartItem> _items = [];
    public IReadOnlyCollection<CartItem> Items => _items.ToList().AsReadOnly();

    public decimal TotalPrice => _items.Sum(item => item.PriceAtAdd.Value * item.Quantity);

    protected Cart() { }

    public Cart(Guid id, Guid userId) : base(id)
    {
        UserId = userId;
    }

    public CartItem AddItem(Guid userId, ProductVariant variant, int quantity = 1)
    {
        if (UserId != userId)
            throw new AnotherUserEditCartException(this, userId);

        if (variant == null) throw new ArgumentNullException(nameof(variant));
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive", nameof(quantity));

        if (!variant.Product.IsActive)
            throw new ProductNotActiveException(variant.Product);

        if (variant.QuantityInStock < quantity)
            throw new InsufficientStockException(variant, quantity);

        var existingItem = _items.FirstOrDefault(i => i.ProductVariant?.Id == variant.Id);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
            return existingItem;
        }

        var item = new CartItem(
            Guid.NewGuid(),
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

    public void RemoveItem(Guid userId, Guid cartItemId)
    {
        if (UserId != userId)
            throw new AnotherUserEditCartException(this, userId);

        var item = _items.FirstOrDefault(i => i.Id == cartItemId);
        if (item == null)
            throw new CartItemNotFoundException(cartItemId);

        _items.Remove(item);
    }

    public void UpdateItemQuantity(Guid userId, Guid cartItemId, int newQuantity)
    {
        if (UserId != userId)
            throw new AnotherUserEditCartException(this, userId);

        if (newQuantity <= 0)
        {
            RemoveItem(userId, cartItemId);
            return;
        }

        var item = _items.FirstOrDefault(i => i.Id == cartItemId);
        if (item == null)
            throw new CartItemNotFoundException(cartItemId);

        if (item.ProductVariant.QuantityInStock < newQuantity)
            throw new InsufficientStockException(item.ProductVariant, newQuantity);

        item.UpdateQuantity(newQuantity);
    }

    public void Clear(Guid userId)
    {
        if (UserId != userId)
            throw new AnotherUserEditCartException(this, userId);

        _items.Clear();
    }

    public override string ToString()
    {
        return $"Cart {Id} UserId: {UserId} Items: {_items.Count} Total: {TotalPrice}";
    }
}