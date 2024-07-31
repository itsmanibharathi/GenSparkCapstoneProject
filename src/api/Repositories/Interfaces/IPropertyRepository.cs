using api.Models;
using api.Models.Dtos.PropertyDtos;

namespace api.Repositories.Interfaces
{
    public interface IPropertyRepository : IRepository<int,Property>
    {
        public Task<IEnumerable<Property>> SearchPropertyAsync(PropertyQueryDto propertyQueryDto);
    }
}
