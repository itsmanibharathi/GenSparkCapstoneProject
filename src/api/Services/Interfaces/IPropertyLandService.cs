using api.Models.Dtos.PropertyDtos;

namespace api.Services.Interfaces
{
    public interface IPropertyLandService
    {
        public Task<ReturnPropertyLandDto> CreatePropertyLandAsync(GetPropertyLandDto getPropertyLandDto);
    }
}
