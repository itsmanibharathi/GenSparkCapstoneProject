using api.Contexts;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PropertyLandRepository : Repository<int,PropertyLand> , IPropertyLandRepository
    {
        public PropertyLandRepository(DbSql context) : base(context)
        {
        }

        public override async Task<bool> IsDuplicate(PropertyLand entity)
        {
            return await _context.Lands.AnyAsync(x => x.PropertyId == entity.PropertyId);
        }
    }
}
