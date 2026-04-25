using SneakerShop.Domain.Base;
using SneakerShop.ValueObjects;

namespace SneakerShop.Domain.Entities;

public class User : Entity<Guid>
{
    public Nickname Nickname { get; private set; }
    public Guid CartId { get; private set; }
    public Guid WishlistId { get; private set; }

    protected User() { }

    public User(Guid id, Nickname nickname, Guid cartId, Guid wishlistId) : base(id)
    {
        Nickname = nickname ?? throw new ArgumentNullException(nameof(nickname));
        CartId = cartId;
        WishlistId = wishlistId;
    }

    public override string ToString()
    {
        return $"{Nickname.Value} (Id: {Id}, CartId: {CartId}, WishlistId: {WishlistId})";
    }
}