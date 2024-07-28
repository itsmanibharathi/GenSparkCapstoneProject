using api.Contexts;
using api.Exceptions;
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

        public override Task<Property> GetAsync(int id)
        {
            try
            {
                return _context.Properties
                    .Include(x => x.Amenities)
                    .Include(x => x.MediaFiles)
                    .Include(x => x.Land)
                    .Include(x => x.Home)
                    .FirstOrDefaultAsync(x => x.PropertyId == id)
                    ?? throw new EntityNotFoundException<Property>($"property with id {id} not found");
            }
            catch (EntityNotFoundException<Property>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get Property", e);
            }

        }
    }
}
