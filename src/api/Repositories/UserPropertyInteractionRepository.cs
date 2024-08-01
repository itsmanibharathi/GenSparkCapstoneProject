using api.Contexts;
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
    }
}
