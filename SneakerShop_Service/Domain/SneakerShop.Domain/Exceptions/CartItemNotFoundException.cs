namespace SneakerShop.Domain.Exceptions;

public class CartItemNotFoundException(Guid cartItemId)
    : InvalidOperationException($"Cart item with id {cartItemId} not found in cart")
{
    public Guid CartItemId => cartItemId;
}