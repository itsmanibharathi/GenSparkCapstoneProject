using api.Models.Dtos.PropertyMediaFile;

namespace api.Services.Interfaces
{
    public interface IPropertyMediaFileService
    {
        public Task<ReturnPropertyMediaFileDto> CreatePropertyMediaFileAsync(GetPropertyMediaFileDto getPropertyMediaFileDto);
        public Task<bool> DeletePropertyMediaFileAsync(int id);
    }
}
