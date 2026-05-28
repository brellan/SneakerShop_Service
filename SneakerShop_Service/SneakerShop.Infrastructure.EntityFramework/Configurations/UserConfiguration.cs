using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SneakerShop.Domain.Entities;
using SneakerShop.ValueObjects;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.Infrastructure.EntityFramework.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).IsRequired();

        builder.Property(u => u.Nickname)
            .HasConversion(
                nickname => nickname.Value,
                value => new Nickname(value))
            .HasMaxLength(NicknameValidator.MAX_LENGTH)
            .IsRequired();

        builder.HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>("UserId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Wishlist)
            .WithOne(w => w.User)
            .HasForeignKey<Wishlist>("UserId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(u => u.Cart);
        builder.Ignore(u => u.Wishlist);
    }
}