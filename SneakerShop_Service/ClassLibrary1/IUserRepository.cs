using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories.Abstractions.Base;

namespace SneakerShop.Domain.Repositories.Abstractions;

public interface IUserRepository : IRepository<User, Guid>
{
    // Так как имя пользователя уникальное
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
}

