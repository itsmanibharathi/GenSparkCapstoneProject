using api.Contexts;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserRepository : Repository<int, User>, IUserRepository
    {
        public UserRepository(DbSql context) : base(context)
        {
        }

        public override async Task<bool> IsDuplicate(User entity)
        {
            return await _context.Users.AnyAsync(x => x.UserEmail == entity.UserEmail);
        }
    }
}