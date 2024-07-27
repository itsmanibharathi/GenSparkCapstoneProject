using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyDtos;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class PropertyLandService : IPropertyLandService
    {
        private readonly IPropertyLandRepository _propertyLandRepository;
        private readonly IMapper _mapper;

        public PropertyLandService(IPropertyLandRepository propertyLandRepository,IMapper mapper)
        {
            _propertyLandRepository = propertyLandRepository;
            _mapper = mapper;
        }
        public async Task<ReturnPropertyLandDto> CreatePropertyLandAsync(GetPropertyLandDto getPropertyLandDto)
        {
            try
            {
                var propertyLand = _mapper.Map<PropertyLand>(getPropertyLandDto);
                propertyLand = await _propertyLandRepository.AddAsync(propertyLand);
                return _mapper.Map<ReturnPropertyLandDto>(propertyLand);
            }
            catch (EntityAlreadyExistsException<PropertyLand>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to create Property Land. Please try again later.");
            }
        }
    }
}
