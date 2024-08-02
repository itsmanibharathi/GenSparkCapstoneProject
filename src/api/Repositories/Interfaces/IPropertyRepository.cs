using api.Models;
using api.Models.Dtos.PropertyDtos;

namespace api.Repositories.Interfaces
{
    public interface IPropertyRepository : IRepository<int,Property>
    {
        public Task<IQueryable<Property>> SearchPropertyAsync( PropertyQueryDto propertyQueryDto);
        public Task<Property> GetWithOwnerInfoAsync(int id);
    }
}
