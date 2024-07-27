using api.Models.Dtos.PropertyDtos;

namespace api.Services.Interfaces
{
    public interface IPropertyHomeService
    {
        public Task<ReturnPropertyHomeDto> CreatePropertyHomeAsync(GetPropertyHomeDto getPropertyHomeDto);
    }
}
