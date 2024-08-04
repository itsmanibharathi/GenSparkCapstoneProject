using api.Contexts;
using api.Exceptions;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserPropertyInteractionRepository : Repository<int, UserPropertyInteraction>, IUserPropertyInteractionRepository
    {
        public UserPropertyInteractionRepository(DbSql context) : base(context)
        {
        }

        public override async Task<bool> IsDuplicate(UserPropertyInteraction entity)
        {
            return await _context.UserPropertyInteractions.AnyAsync(x => x.UserId == entity.UserId && x.PropertyId == entity.PropertyId && x.InteractionType == entity.InteractionType);
        }

        public async Task<IEnumerable<UserPropertyInteraction>> GetByUserId(int userId)
        {
            try
            {
                return await _context.UserPropertyInteractions
                    .Include(x => x.User)
                    .Include(x => x.Property)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
            }
            catch (EntityNotFoundException<UserPropertyInteraction>)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw new EntityNotFoundException<UserPropertyInteraction>($"No UserPropertyInteraction in the database with id {userId}");
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get UserPropertyInteraction", e);
            }
        }
    }
}
