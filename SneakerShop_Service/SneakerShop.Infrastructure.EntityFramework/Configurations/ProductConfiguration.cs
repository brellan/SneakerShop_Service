using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Enums;
using SneakerShop.ValueObjects;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.Infrastructure.EntityFramework.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired();

        builder.Property(p => p.ProductName)
            .HasConversion(
                name => name.Value,
                value => new ProductName(value))
            .HasMaxLength(ProductNameValidator.MAX_LENGTH)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasConversion(
                desc => desc.Value,
                value => new Description(value))
            .HasMaxLength(DescriptionValidator.MAX_LENGTH)
            .IsRequired();

        builder.Property(p => p.BasePrice)
            .HasConversion(
                price => price.Value,
                value => new Price(value))
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Currency)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.MainImageUrl)
            .HasConversion(
                url => url.Value,
                value => new ImageUrl(value))
            .HasMaxLength(ImageUrlValidator.MAX_LENGTH)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .IsRequired();

        builder.HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey("BrandId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Variants)
            .WithOne(v => v.Product)
            .HasForeignKey("ProductId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(p => p.Variants);
    }
}