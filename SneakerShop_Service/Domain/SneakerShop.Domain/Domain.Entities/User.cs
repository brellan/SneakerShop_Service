using SneakerShop.ValueObject;

namespace SneakerShop.Domain.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public Nickname Nickname { get; private set; }

    public Cart Cart { get; private set; }
    public Wishlist Wishlist { get; private set; }

    private User() { }

    public User(Nickname nickname)
    {
        Id = Guid.NewGuid();
        Nickname = nickname;
    }

    public void UpdateNickname(Nickname nickname) => Nickname = nickname;

    public void CreateCart()
    {
        Cart = new Cart(Id);
    }

    public void CreateWishlist()
    {
        Wishlist = new Wishlist(Id);
    }

    public override string ToString()
    {
        return $"User [Id: {Id}, Nickname: {Nickname}]";
    }
}