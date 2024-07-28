using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyMediaFile;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class PropertyMediaFileService : IPropertyMediaFileService
    {
        private readonly IPropertyMediaFileRepository _propertyMediaFileRepository;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IMapper _mapper;

        public PropertyMediaFileService(IPropertyMediaFileRepository propertyMediaFileRepository, IAzureBlobStorageService azureBlobStorageService, IMapper mapper)
        {
            _propertyMediaFileRepository = propertyMediaFileRepository;
            _azureBlobStorageService = azureBlobStorageService;
            _mapper = mapper;

        }
        public Task<bool> DeletePropertyMediaFileAsync(int id)
        {
            try
            {
                return _propertyMediaFileRepository.DeleteAsync(id);
            }
            catch (EntityNotFoundException<PropertyMediaFile>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to delete Property Media File. Please try again later.");
            }
        }
    }
}
