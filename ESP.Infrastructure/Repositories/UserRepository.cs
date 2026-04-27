
using ESP.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ESP.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string mail)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Mail == mail);
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }

}
