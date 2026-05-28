using SneakerShop.Domain.Base;
using SneakerShop.ValueObjects;

namespace SneakerShop.Domain.Entities;

public class CartItem : Entity<Guid>
{
    public Cart Cart { get; private set; }
    public ProductVariant ProductVariant { get; private set; }
    public ProductName ProductName { get; private set; }
    public Size Size { get; private set; }
    public Color Color { get; private set; }
    public Price PriceAtAdd { get; private set; }
    public Quantity Quantity { get; private set; }

    protected CartItem() { }

    public CartItem(
        Cart cart,
        ProductVariant productVariant,
        ProductName productName,
        Size size,
        Color color,
        Price priceAtAdd,
        Quantity quantity)
        : this(Guid.NewGuid(), cart, productVariant, productName, size, color, priceAtAdd, quantity) { }

    protected CartItem(Guid id,
        Cart cart,
        ProductVariant productVariant,
        ProductName productName,
        Size size,
        Color color,
        Price priceAtAdd,
        Quantity quantity)
        : base(id)
    {
        Cart = cart ?? throw new ArgumentNullException(nameof(cart));
        ProductVariant = productVariant ?? throw new ArgumentNullException(nameof(productVariant));
        ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
        Size = size ?? throw new ArgumentNullException(nameof(size));
        Color = color ?? throw new ArgumentNullException(nameof(color));
        PriceAtAdd = priceAtAdd ?? throw new ArgumentNullException(nameof(priceAtAdd));
        Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
    }

    internal void UpdateQuantity(Quantity newQuantity)
    {
        Quantity = newQuantity ?? throw new ArgumentNullException(nameof(newQuantity));
    }

    public decimal GetTotalPrice()
    {
        return (PriceAtAdd * Quantity).Value;
    }

    public override string ToString()
    {
        return $"{ProductName.Value} Size: {Size.Value} Color: {Color.Value} Qty: {Quantity.Value} Price: {PriceAtAdd.Value} Total: {GetTotalPrice()}";
    }
}