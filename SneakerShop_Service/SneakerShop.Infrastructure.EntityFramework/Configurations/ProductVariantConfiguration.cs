using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SneakerShop.Domain.Entities;
using SneakerShop.ValueObjects;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.Infrastructure.EntityFramework.Configurations;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.HasKey(pv => pv.Id);
        builder.Property(pv => pv.Id).IsRequired();

        builder.Property(pv => pv.Sku)
            .HasConversion(
                sku => sku.Value,
                value => new Sku(value))
            .HasMaxLength(SkuValidator.MAX_LENGTH)
            .IsRequired();

        builder.Property(pv => pv.Size)
            .HasConversion(
                size => size.Value,
                value => new Size(value))
            .IsRequired();

        builder.Property(pv => pv.Color)
            .HasConversion(
                color => color.Value,
                value => new Color(value))
            .HasMaxLength(ColorValidator.MAX_LENGTH)
            .IsRequired();

        builder.Property(pv => pv.QuantityInStock)
            .HasConversion(
                stock => stock.Value,
                value => new StockQuantity(value))
            .IsRequired();

        builder.Property(pv => pv.AdditionalImages)
            .HasColumnType("text[]")
            .IsRequired(false);

        builder.HasOne(pv => pv.Product)
            .WithMany(p => p.Variants)
            .HasForeignKey("ProductId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}