using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SneakerShop.Domain.Entities;
using SneakerShop.ValueObjects;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.Infrastructure.EntityFramework.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.Id);
        builder.Property(ci => ci.Id).IsRequired();

        builder.Property(ci => ci.ProductName)
            .HasConversion(
                name => name.Value,
                value => new ProductName(value))
            .HasMaxLength(ProductNameValidator.MAX_LENGTH)
            .IsRequired();

        builder.Property(ci => ci.Size)
            .HasConversion(
                size => size.Value,
                value => new Size(value))
            .IsRequired();

        builder.Property(ci => ci.Color)
            .HasConversion(
                color => color.Value,
                value => new Color(value))
            .HasMaxLength(ColorValidator.MAX_LENGTH)
            .IsRequired();

        builder.Property(ci => ci.PriceAtAdd)
            .HasConversion(
                price => price.Value,
                value => new Price(value))
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(ci => ci.Quantity)
            .HasConversion(
                qty => qty.Value,
                value => new Quantity(value))
            .IsRequired();

        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey("CartId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ci => ci.ProductVariant)
            .WithMany()
            .HasForeignKey("VariantId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}