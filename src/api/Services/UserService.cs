using api.Exceptions;
using api.Models;
using api.Models.Dtos.UserDtos;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IAzureBlobStorageService azureBlobStorageService, IMapper mapper)
        {
            _userRepository = userRepository;
            _azureBlobStorageService = azureBlobStorageService;
            _mapper = mapper;
        }
        public async Task<string> Activation(int id, string token)
        {
            try
            {
                var user = await _userRepository.GetAsync(id);
                if (user.IsActive)
                {
                    return "User already activated";
                }
                Console.WriteLine("in  "+user.ActivationToken);
                Console.WriteLine("out  " + token);
                if (user.ActivationToken == token)
                {
                    user.IsActive = true;
                    user.ActivationToken = null;
                    await _userRepository.UpdateAsync(user);
                    return "User activated successfully";
                }
                return "Invalid token";
            }
            catch (EntityNotFoundException<User>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to Activate the user",ex);
            }
        }

        public async Task<User> GetUserAsync(int id)
        {
            try
            {
                return await _userRepository.GetAsync(id);
            }
            catch (EntityNotFoundException<User>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the user", ex);
            }
        }

        public async Task<ReturnUserDto> UpdateUserAsync(int id, GetUserEditDto userEditDto)
        {
            try
            {
                var user = await _userRepository.GetAsync(id);
                user = _mapper.Map(userEditDto, user);
                await _userRepository.UpdateAsync(user);
                return _mapper.Map<ReturnUserDto>(user);
            }
            catch (EntityNotFoundException<User>)
            {
                throw;
            }
            catch (EnvironmentVariableUndefinedException)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to update the user", ex);
            }
        }

        async Task<ReturnUserDto> IUserService.GetUserAsync(int id)
        {
            try
            {
                var res = await _userRepository.GetAsync(id);
                return _mapper.Map<ReturnUserDto>(res);
            }
            catch (EntityNotFoundException<User>)
            {
                throw;
            }
            catch(UnableToDoActionException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Something went wrong", ex);
            }
        }

    }
}
