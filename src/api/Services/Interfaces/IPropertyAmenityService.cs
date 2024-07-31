using api.Models.Dtos.PropertyAmenityDtos;

namespace api.Services.Interfaces
{
    public interface IPropertyAmenityService
    {
        public Task<bool> DeletePropertyAmenityAsync(int id);
    }
}
