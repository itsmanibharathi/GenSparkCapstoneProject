using api.Models.Dtos.PropertyDtos;

namespace api.Services.Interfaces
{
    public interface IPropertyService
    {
        public Task<ReturnPropertyDto> GetAsync(int id);
        public Task<IEnumerable<ReturnPropertyDto>> SearchPropertyAsync(PropertyQueryDto propertyQueryDto);
        public Task<ReturnPropertyDto> CreateAsync(int userId, CreatePropertyDto getProperityDto);
        public Task<ReturnPropertyDto> UpdateAsync(EditPropertyDto editPropertyDto);
    }
}
