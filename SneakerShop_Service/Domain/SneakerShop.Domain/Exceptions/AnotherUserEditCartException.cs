using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Exceptions;

public class AnotherUserEditCartException(Cart cart, Guid userId)
    : InvalidOperationException($"User {userId} cannot edit cart {cart.Id} belonging to user {cart.UserId}")
{
    public Cart Cart => cart;
    public Guid UserId => userId;
}