using SneakerShop.Domain.Base;

namespace SneakerShop.Domain.Entities;

public class WishlistItem : Entity<Guid>
{
    public Wishlist Wishlist { get; private set; }
    public ProductVariant ProductVariant { get; private set; }
    public Product Product { get; private set; }

    protected WishlistItem() { }

    public WishlistItem(
        Wishlist wishlist,
        ProductVariant productVariant,
        Product product)
        : this(Guid.NewGuid(), wishlist, productVariant, product) { }

    protected WishlistItem(Guid id,
        Wishlist wishlist,
        ProductVariant productVariant,
        Product product)
        : base(id)
    {
        Wishlist = wishlist ?? throw new ArgumentNullException(nameof(wishlist));
        ProductVariant = productVariant ?? throw new ArgumentNullException(nameof(productVariant));
        Product = product ?? throw new ArgumentNullException(nameof(product));
    }

    public override string ToString()
    {
        return $"WishlistItem {Id} Product: {Product.ProductName.Value} Variant: {ProductVariant.Sku.Value}";
    }
}