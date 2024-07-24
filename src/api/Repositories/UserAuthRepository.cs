using api.Contexts;
using api.Exceptions;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserAuthRepository : Repository<int,UserAuth> , IUserAuthRepository
    {
        public UserAuthRepository(DbSql context) : base(context)
        {
        }

        public async Task<UserAuth> GetByUserEmailAsync(string userEmail)
        {
            try
            {
                return await _context.UserAuths
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.User.UserEmail == userEmail) 
                    ?? throw new EntityNotFoundException<UserAuth>();
            }
            catch (EntityNotFoundException<UserAuth>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get entity", e);
            }
        }

        public override async Task<bool> IsDuplicate(UserAuth entity)
        {
            return await _context.UserAuths.AnyAsync(x => x.User.UserEmail == entity.User.UserEmail);
        }
    }
}
