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
        private readonly IMapper _mapper;

        public UserAuthService(
            IUserAuthRepository userAuthRepository,
            ITokenService<User> tokenService,
            IPasswordHashService passwordHashService,
            IMapper mapper) 
        {
            _userAuthRepository = userAuthRepository;
            _userAuthTokenService = tokenService;
            _passwordHashService = passwordHashService;
            _mapper = mapper;
        }
        public Task<ReturnUserLoginDto> Login(GetUserLoginDto getUserLoginDto)
        {
            try
            {
                var userAuth = _userAuthRepository.GetByUserEmailAsync(getUserLoginDto.UserEmail).Result;
                if (userAuth == null)
                {
                    throw new EntityNotFoundException<UserAuth>();
                }

                if (!_passwordHashService.Verify(getUserLoginDto.Password,userAuth.Password))
                {
                    throw new InvalidPasswordException();
                }

                var token = _userAuthTokenService.GenerateToken(userAuth.User);
                return Task.FromResult(new ReturnUserLoginDto
                {
                    UserName = userAuth.User.UserName,
                    Token = token
                });
            }
            catch (EntityNotFoundException<UserAuth>)
            {
                throw;
            }
            catch (InvalidPasswordException)
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

                var res = await _userAuthRepository.AddAsync(userAuth);
                return _mapper.Map<ReturnUserRegisterDto>(res);
            }
            catch (EntityAlreadyExistsException<UserAuth> )
            {
                throw;
            }
            catch (UnableToDoActionException)
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
