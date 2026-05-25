using SneakerShop.Domain.Base;
using SneakerShop.ValueObjects;

namespace SneakerShop.Domain.Entities;

public class User : Entity<Guid>
{
    public Nickname Nickname { get; private set; }

    protected User() { }

    public User(Nickname nickname)
        : this(Guid.NewGuid(), nickname) { }

    protected User(Guid id, Nickname nickname)
        : base(id)
    {
        Nickname = nickname ?? throw new ArgumentNullException(nameof(nickname));
    }

    public CartItem AddToCart(Cart cart, ProductVariant variant, Quantity? quantity = null)
    {
        return cart.AddItem(this, variant, quantity);
    }

    public void RemoveFromCart(Cart cart, Guid cartItemId)
    {
        cart.RemoveItem(this, cartItemId);
    }

    public void UpdateCartItemQuantity(Cart cart, Guid cartItemId, Quantity newQuantity)
    {
        cart.UpdateItemQuantity(this, cartItemId, newQuantity);
    }

    public void ClearCart(Cart cart)
    {
        cart.Clear(this);
    }

    public WishlistItem AddToWishlist(Wishlist wishlist, ProductVariant variant)
    {
        return wishlist.AddItem(this, variant);
    }

    public void RemoveFromWishlist(Wishlist wishlist, Guid wishlistItemId)
    {
        wishlist.RemoveItem(this, wishlistItemId);
    }

    public void RemoveFromWishlistByVariant(Wishlist wishlist, Guid variantId)
    {
        wishlist.RemoveItemByVariant(this, variantId);
    }

    public bool IsInWishlist(Wishlist wishlist, Guid variantId)
    {
        return wishlist.ContainsVariant(this, variantId);
    }

    public override string ToString()
    {
        return $"{Nickname.Value} (Id: {Id})";
    }
}