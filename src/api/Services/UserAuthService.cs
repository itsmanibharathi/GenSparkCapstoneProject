using api.Exceptions;
using api.Models;
using api.Models.Dtos.UserDtos;
using api.Repositories;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;

namespace api.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly ITokenService<User> _userAuthTokenService;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public UserAuthService(
            IUserAuthRepository userAuthRepository,
            ITokenService<User> tokenService,
            IPasswordHashService passwordHashService,
            IMailService mailService,
            IMapper mapper) 
        {
            _userAuthRepository = userAuthRepository;
            _userAuthTokenService = tokenService;
            _passwordHashService = passwordHashService;
            _mailService = mailService;
            _mapper = mapper;
        }
        public async Task<ReturnUserLoginDto> Login(GetUserLoginDto getUserLoginDto)
        {
            try
            {
                var userAuth = await _userAuthRepository.GetByUserEmailAsync(getUserLoginDto.UserEmail);
                if (userAuth.User.IsActive == false)
                {
                    throw new UserNotActivateException();
                }
                if (_passwordHashService.Verify(getUserLoginDto.Password, userAuth.Password))
                {
                    var token = _userAuthTokenService.GenerateToken(userAuth.User);
                    return new ReturnUserLoginDto
                    {
                        UserName = userAuth.User.UserName,
                        Token = token
                    };
                }
                throw new InvalidUserCredentialException();
            }
            catch (EntityNotFoundException<UserAuth>)
            {
                throw;
            }
            catch (InvalidUserCredentialException)
            {
                throw;
            }
            catch (UserNotActivateException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to login user", e);
            }
        }

        public async Task<ReturnUserRegisterDto> Register(GetUserRegisterDto getUserRegisterDto)
        {
            try
            {
                UserAuth userAuth = new UserAuth
                {
                    User = _mapper.Map<User>(getUserRegisterDto),
                    Password = _passwordHashService.Hash(getUserRegisterDto.Password)
                };
                userAuth.User.ActivationToken = Guid.NewGuid().ToString();
                var res = await _userAuthRepository.AddAsync(userAuth);
                await _mailService.Send(userAuth.User.UserEmail, "Activate your account", $"Click <a href='http://localhost:5000/api/user/activate/{userAuth.User.ActivationToken}'>here</a> to activate your account");
                return _mapper.Map<ReturnUserRegisterDto>(res.User);
            }
            catch (EntityAlreadyExistsException<UserAuth> )
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (EnvironmentVariableUndefinedException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to register user", e);
            }

        }
    }
}
