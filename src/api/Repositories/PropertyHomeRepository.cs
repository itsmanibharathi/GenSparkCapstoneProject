using api.Contexts;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PropertyHomeRepository : Repository<int, PropertyHome>
    {
        public PropertyHomeRepository(DbSql context) : base(context)
        {
        }

        public override async Task<bool> IsDuplicate(PropertyHome entity)
        {
            return await _context.Homes.AnyAsync(x => x.PropertyId == entity.PropertyId);
        }
    }
}
