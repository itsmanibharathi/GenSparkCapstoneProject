using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyDtos;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }
        public async Task<ReturnPropertyDto> CreateAsync(CreatePropertyDto getProperityDto)
        {
            try
            {
                var property = _mapper.Map<Property>(getProperityDto);
                await _propertyRepository.AddAsync(property);
                return _mapper.Map<ReturnPropertyDto>(property);
            }
            catch (EntityAlreadyExistsException<Property>)
            {
                throw;
            }
            catch(UnableToDoActionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to create property. Please try again later.");
            }
        }

        public async Task<ReturnPropertyDto> GetAsync(int id)
        {
            try
            {
                return _mapper.Map<ReturnPropertyDto>(await _propertyRepository.GetAsync(id));
            }
            catch (EntityNotFoundException<Property>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to get property. Please try again later.");
            }
        }
    }
}
