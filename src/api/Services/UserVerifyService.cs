using api.Exceptions;
using api.Models;
using api.Models.Enums;
using api.Repositories.Interfaces;
using api.Services.Interfaces;

namespace api.Services
{
    public class UserVerifyService : IUserVerifyService
    {
        private readonly IUserVerifyRepository _userVerifyRepository;
        private readonly IUserRepository _userRepository;

        public UserVerifyService(IUserVerifyRepository userVerifyRepository, IUserRepository userRepository )
        {
            _userVerifyRepository = userVerifyRepository;
            _userRepository = userRepository;

        }
        public async Task<string> VertifyUser(int id, string token)
        {
            try
            {
                var userVerify = await _userVerifyRepository.GetAsync(id);
                if (userVerify.UserVerifyStatus == UserVerifyStatus.Verified)
                {
                    return "User already activated";
                }
                if (userVerify.Token == token)
                {
                    userVerify.Token = null;
                    userVerify.UserVerifyStatus = UserVerifyStatus.Verified;
                    userVerify.User.IsActive = true;
                    await _userVerifyRepository.UpdateAsync(userVerify);
                    return "User activated successfully";
                }
                return "Invalid token";
            }
            catch (EntityNotFoundException<UserVerify>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to Activate the user", ex);
            }

        }
    }
}
