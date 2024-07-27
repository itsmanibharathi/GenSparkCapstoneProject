using api.Contexts;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PropertyRepository : Repository<int, Property> , IPropertyRepository
    {
        public PropertyRepository(DbSql context) : base(context)
        {
        }

        public override async Task<bool> IsDuplicate(Property entity)
        {
            return await _context.Properties.AnyAsync(x => x.Title == entity.Title && x.Price == entity.Price && x.UserId == x.UserId);
        }
    }
}
