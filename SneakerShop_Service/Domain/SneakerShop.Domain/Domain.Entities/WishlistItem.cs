namespace SneakerShop.Domain.Domain.Entities;

public class WishlistItem
{
    public Guid Id { get; private set; }
    public Guid WishlistId { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid? ProductVariantId { get; private set; }

    public Wishlist Wishlist { get; private set; }
    public Product Product { get; private set; }
    public ProductVariant ProductVariant { get; private set; }

    private WishlistItem() { }

    public WishlistItem(Guid wishlistId, Guid productId, Guid? productVariantId = null)
    {
        Id = Guid.NewGuid();
        WishlistId = wishlistId;
        ProductId = productId;
        ProductVariantId = productVariantId;
    }

    public override string ToString()
    {
        return $"WishlistItem [Id: {Id}, WishlistId: {WishlistId}, ProductId: {ProductId}, VariantId: {ProductVariantId?.ToString() ?? "не указан"}]";
    }
}