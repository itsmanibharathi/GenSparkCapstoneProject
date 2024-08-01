using api.Contexts;
using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyDtos;
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

        public override async Task<Property> GetAsync(int id)
        {
            try
            {
                return await _context
                    .Properties
                    .Include(x => x.Amenities)
                    .Include(x => x.MediaFiles)
                    .Include(x => x.Land)
                    .Include(x => x.Home)
                    .FirstAsync(x => x.PropertyId == id) 
                    ?? throw new EntityNotFoundException<Property>($"No property in the database with id {id}");
            }
            catch (EntityNotFoundException<Property>)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw new EntityNotFoundException<Property>($"No property in the database with id {id}");
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get Property", e);
            }

        }

        public async Task<IEnumerable<Property>> SearchPropertyAsync(PropertyQueryDto propertyQueryDto)
        {
            try
            {
                var res= await _context
                    .Properties
                    .Include(x => x.Amenities)
                    .Include(x => x.MediaFiles)
                    .Include(x => x.Land)
                    .Include(x => x.Home)
                    .ToListAsync();
                return res;
                //throw new EntityNotFoundException<Property>();
            }
            catch (EntityNotFoundException<Property>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to search property", e);
            }
        }

        public async Task<Property> GetWithOwnerInfoAsync(int id)
        {
            try
            {
                var property = await _context
                    .Properties
                    .Include(x => x.User)
                    .Include(x => x.UserPropertyInteractions)
                    .SingleOrDefaultAsync(x => x.PropertyId == id);

                if (property == null)
                {
                    throw new EntityNotFoundException<Property>($"No property in the database with id {id}");
                }

                return property;
            }
            catch (EntityNotFoundException<Property>)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw new EntityNotFoundException<Property>($"No property in the database with id {id}");
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get Property", e);
            }
        }
    }
}
