using SneakerShop.Domain.Base;

namespace SneakerShop.Domain.Entities;

public class WishlistItem : Entity<Guid>
{
    public Wishlist Wishlist { get; private set; }
    public ProductVariant ProductVariant { get; private set; }
    public Guid ProductId { get; private set; }

    protected WishlistItem() { }

    public WishlistItem(Guid id, Wishlist wishlist, ProductVariant productVariant, Guid productId) : base(id)
    {
        Wishlist = wishlist ?? throw new ArgumentNullException(nameof(wishlist));
        ProductVariant = productVariant ?? throw new ArgumentNullException(nameof(productVariant));
        ProductId = productId;
    }

    public override string ToString()
    {
        return $"WishlistItem {Id} ProductId: {ProductId} VariantId: {ProductVariant.Id}";
    }
}