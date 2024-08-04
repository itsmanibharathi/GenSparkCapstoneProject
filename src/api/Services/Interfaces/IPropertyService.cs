using api.Models;
using api.Models.Dtos.PropertSeedData;
using api.Models.Dtos.PropertyDtos;

namespace api.Services.Interfaces
{
    public interface IPropertyService
    {
        public Task<ReturnPropertyDto> GetAsync(int userId,int id);
        public Task<IEnumerable<ReturnViewPropertyDto>> SearchPropertyAsync(int userId, PropertyQueryDto propertyQueryDto);
        public Task<ReturnPropertyDto> CreateAsync(int userId, CreatePropertyDto getProperityDto);
        public Task<ReturnPropertyDto> UpdateAsync(EditPropertyDto editPropertyDto);
        public Task<IEnumerable<ReturnPropertyDto>> SeedPropert(IEnumerable<SeedPropertyDto> seedPropertyDtos);
    }
}
