using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyDtos;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class PropertyHomeService : IPropertyHomeService
    {
        private readonly IPropertyHomeRepository _propertyHomeRepository;
        private readonly IMapper _mapper;

        public PropertyHomeService(IPropertyHomeRepository propertyHomeRepository,IMapper mapper ) 
        {
            _propertyHomeRepository = propertyHomeRepository;
            _mapper = mapper;
        }
        public async Task<ReturnPropertyHomeDto> CreatePropertyHomeAsync(GetPropertyHomeDto getPropertyHomeDto)
        {
            try
            {
                var propertyHome = _mapper.Map<PropertyHome>(getPropertyHomeDto);
                propertyHome = await _propertyHomeRepository.AddAsync(propertyHome);
                return _mapper.Map<ReturnPropertyHomeDto>(propertyHome);
            }
            catch (EntityAlreadyExistsException<PropertyHome>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to create Property Home. Please try again later.");
            }
        }
    }
}
