using ESP.Infrastructure;

namespace ESP.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);

    }
}
