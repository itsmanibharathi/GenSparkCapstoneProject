using api.Exceptions;
using api.Models;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
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
    }
}
