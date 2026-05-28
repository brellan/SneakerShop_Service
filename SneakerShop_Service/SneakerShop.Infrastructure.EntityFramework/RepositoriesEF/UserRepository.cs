using AuctionTrading.Infrastructure.EntityFramework.RepositoriesEF;
using Microsoft.EntityFrameworkCore;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories.Abstractions;
using SneakerShop.Infrastructure.EntityFramework;
using SneakerShop.ValueObjects;

namespace SneakerShop.Infrastructure.EntityFramework.RepositoriesEF;

public class UserRepository(ApplicationDbContext context)
    : EfRepository<User, Guid>(context), IUserRepository
{
    private readonly DbSet<User> _users = context.Set<User>();

    public override async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _users
            .Include(u => u.Cart)
            .Include(u => u.Wishlist)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    => await _users
        .Include("Cart")
        .Include("Wishlist")
        .FirstOrDefaultAsync(u => u.Nickname.Equals(new Nickname(username)), cancellationToken);
}