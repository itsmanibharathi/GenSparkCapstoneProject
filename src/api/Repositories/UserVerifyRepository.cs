using api.Contexts;
using api.Exceptions;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserVerifyRepository : Repository<int, UserVerify>, IUserVerifyRepository
    {
        public UserVerifyRepository(DbSql context) : base(context)
        {

        }
        public override async Task<bool> IsDuplicate(UserVerify entity)
        {
            return await _context.UserVerifies.AnyAsync(x => x.UserId == entity.UserId);
        }

        public override async Task<UserVerify> GetAsync(int id)
        {
            try
            {
                return await _context.UserVerifies
                    .Include(x => x.User)
                    .FirstAsync(x => x.Id == id) ?? throw new EntityNotFoundException<UserVerify>();
            }
            catch (EntityNotFoundException<UserVerify>)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to get User Verify. Please try again later.");
            }

        }
    }
}
