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
    public int Quantity { get; private set; }

    protected CartItem() { }

    public CartItem(Guid id, Cart cart, ProductVariant productVariant, ProductName productName,
        Size size, Color color, Price priceAtAdd, int quantity) : base(id)
    {
        Cart = cart ?? throw new ArgumentNullException(nameof(cart));
        ProductVariant = productVariant ?? throw new ArgumentNullException(nameof(productVariant));
        ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
        Size = size ?? throw new ArgumentNullException(nameof(size));
        Color = color ?? throw new ArgumentNullException(nameof(color));
        PriceAtAdd = priceAtAdd ?? throw new ArgumentNullException(nameof(priceAtAdd));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        Quantity = quantity;
    }

    internal void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(newQuantity));

        Quantity = newQuantity;
    }

    public decimal GetTotalPrice()
    {
        return PriceAtAdd.Value * Quantity;
    }

    public override string ToString()
    {
        return $"{ProductName.Value} Size: {Size.Value} Color: {Color.Value} Qty: {Quantity} Price: {PriceAtAdd.Value} Total: {GetTotalPrice()}";
    }
}