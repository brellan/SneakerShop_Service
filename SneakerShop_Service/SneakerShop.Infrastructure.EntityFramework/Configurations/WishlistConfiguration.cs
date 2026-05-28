using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SneakerShop.Domain.Entities;

namespace SneakerShop.Infrastructure.EntityFramework.Configurations;

public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id).IsRequired();

        builder.Property<Guid>("UserId")
            .IsRequired();

        builder.HasOne(w => w.User)
            .WithOne()
            .HasForeignKey<Wishlist>("UserId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Items)
            .WithOne(wi => wi.Wishlist)
            .HasForeignKey("WishlistId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(w => w.Items);
    }
}