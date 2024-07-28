using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyAmenityDtos;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class PropertyAmenityService : IPropertyAmenityService
    {
        private readonly IPropertyAmenityRepository _propertyAmenityRepository;
        private readonly IMapper _mapper;

        public PropertyAmenityService(IPropertyAmenityRepository propertyAmenityRepository, IMapper mapper)
        {
            _propertyAmenityRepository = propertyAmenityRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeletePropertyAmenityAsync(int id)
        {
            try
            {
                return await _propertyAmenityRepository.DeleteAsync(id);
            }
            catch (EntityNotFoundException<PropertyAmenity>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to delete Property Amenity. Please try again later.");
            }
        }
    }
}
