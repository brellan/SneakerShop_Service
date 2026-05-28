using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SneakerShop.Domain.Entities;

namespace SneakerShop.Infrastructure.EntityFramework.Configurations;

public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
{
    public void Configure(EntityTypeBuilder<WishlistItem> builder)
    {
        builder.HasKey(wi => wi.Id);
        builder.Property(wi => wi.Id).IsRequired();

        builder.HasOne(wi => wi.Wishlist)
            .WithMany(w => w.Items)
            .HasForeignKey("WishlistId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(wi => wi.ProductVariant)
            .WithMany()
            .HasForeignKey("VariantId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(wi => wi.Product)
            .WithMany()
            .HasForeignKey("ProductId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}