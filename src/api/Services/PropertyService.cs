using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertSeedData;
using api.Models.Dtos.PropertyDtos;
using api.Models.Enums;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PropertyService(IPropertyRepository propertyRepository,IUserRepository userRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ReturnPropertyDto> CreateAsync(int userId,CreatePropertyDto getProperityDto)
        {
            try
            {

                var property = _mapper.Map<Property>(getProperityDto);
                property.User = await _userRepository.GetAsync(userId);
                property.User.IsOwner = true;
                if(property.Category == PropertyCategory.Home)
                {
                    property.Home = new PropertyHome();
                }
                else
                {
                    property.Land = new PropertyLand(); 
                }
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

        public async Task<ReturnPropertyDto> GetAsync(int userId, int id)
        {
            try
            {
                var res = await _propertyRepository.GetAsync(id);
                if (res.UserId != userId)
                {
                    throw new UnauthorizedAccessException("You are not authorized to view this property");
                }
                return _mapper.Map<ReturnPropertyDto>(res);
            }
            catch(UnauthorizedAccessException)
            {
                throw;
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

        public async Task<IEnumerable<ReturnViewPropertyDto>> SearchPropertyAsync(int userId, PropertyQueryDto propertyQueryDto)
        {
            try
            {
                var res = await _propertyRepository.SearchPropertyAsync(propertyQueryDto);

                // Perform server-side filtering
                if (propertyQueryDto.Type != null)
                {
                    res = res.Where(x => x.Type == propertyQueryDto.Type);
                }
                if (propertyQueryDto.Category != null)
                {
                    res = res.Where(x => x.Category == propertyQueryDto.Category);
                }
                if (propertyQueryDto.GetMyProperty)
                {
                    res = res.Where(x => x.UserId == userId);
                }
                else
                {
                    res = res.Where(x => x.Status == PropertyStatus.Active);
                }

                // Retrieve data from the database first
                var properties = await res
                    .Include(p => p.Amenities)
                    .Include(p => p.MediaFiles)
                    .Include(p => p.Home)
                    .Include(p => p.Land)
                    .OrderBy(x => x.CreateAt)
                    .ToListAsync();

                // Perform client-side filtering
                if (propertyQueryDto.SearchQuery != null)
                {
                    var fineQuery = propertyQueryDto.SearchQuery.ToLower().Split(" ");
                    properties = properties
                        .Where(x => fineQuery.Any(y =>
                            x.Title.ToLower().Contains(y) ||
                            x.Description.ToLower().Contains(y) ||
                            x.State.ToLower().Contains(y) ||
                            x.City.ToLower().Contains(y) ||
                            x.Landmark.ToLower().Contains(y)))
                        .ToList();
                }

                return _mapper.Map<IEnumerable<ReturnViewPropertyDto>>(properties);

            }
            catch (EntityNotFoundException<Property>)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to search property. Please try again later.");
            }
        }

        public async Task<IEnumerable<ReturnPropertyDto>> SeedPropert(IEnumerable<SeedPropertyDto> seedPropertyDtos)
        {
            try
            {
                var properties = _mapper.Map<IEnumerable<Property>>(seedPropertyDtos);
                // for each property, add the property to the database
                foreach (var property in properties)
                {
                    await _propertyRepository.AddAsync(property);
                }
                return _mapper.Map<List<ReturnPropertyDto>>(properties);
            }
            catch (EntityAlreadyExistsException<Property>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException("Unable to seed property. Please try again later.");
            }
        }

        public async Task<ReturnPropertyDto> UpdateAsync(EditPropertyDto editPropertyDto)
        {
            try
            {
                var property = _mapper.Map<Property>(editPropertyDto);
                await _propertyRepository.UpdateAsync(property);
                return _mapper.Map<ReturnPropertyDto>(property);
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
                throw new UnableToDoActionException("Unable to update property. Please try again later.");
            }
        }
    }
}
