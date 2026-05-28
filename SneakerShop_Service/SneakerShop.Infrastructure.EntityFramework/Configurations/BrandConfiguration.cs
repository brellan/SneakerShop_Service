using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SneakerShop.Domain.Entities;
using SneakerShop.ValueObjects;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.Infrastructure.EntityFramework.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).IsRequired();

        builder.Property(b => b.Name)
            .HasConversion(
                name => name.Value,
                value => new BrandName(value))
            .HasMaxLength(BrandNameValidator.MAX_LENGTH)
            .IsRequired();

        builder.Property(b => b.LogoUrl)
            .HasConversion(
                url => url.Value,
                value => new LogoUrl(value))
            .HasMaxLength(LogoUrlValidator.MAX_LENGTH)
            .IsRequired();

        builder.Property(b => b.Description)
            .HasConversion(
                desc => desc.Value,
                value => new Description(value))
            .HasMaxLength(DescriptionValidator.MAX_LENGTH)
            .IsRequired();

        builder.HasMany(b => b.Products)
            .WithOne(p => p.Brand)
            .HasForeignKey("BrandId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(b => b.Products);
    }
}