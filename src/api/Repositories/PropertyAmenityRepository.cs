using api.Contexts;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PropertyAmenityRepository : Repository<int, PropertyAmenity> , IPropertyAmenityRepository
    {
        public PropertyAmenityRepository(DbSql context) : base(context)
        {
        }

        public override async Task<bool> IsDuplicate(PropertyAmenity entity)
        {
            return await _context.Amenities.AnyAsync(x => x.PropertyId == entity.PropertyId && x.Name == entity.Name);
        }
    }
}
