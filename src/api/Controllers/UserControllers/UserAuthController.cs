using api.Exceptions;
using api.Models;
using api.Models.Dtos.ResponseDtos;
using api.Models.Dtos.UserDtos;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.UserControllers
{
    [Route("user/auth")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthServices;
        private readonly ILogger<UserAuthController> _logger;

        public UserAuthController(IUserAuthService userAuthServices, ILogger<UserAuthController> logger)
        {
            _userAuthServices = userAuthServices;
            _logger= logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(GetUserLoginDto getUserLoginDto)
        {
            try
            {
                var result = await _userAuthServices.Login(getUserLoginDto);
                var res = new ResponseDto<ReturnUserLoginDto>(StatusCodes.Status201Created, "User Successfully Logged in", result);
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (EntityNotFoundException<UserAuth> e)
            {
                _logger.LogError(e, "User not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "User not found");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (InvalidUserCredentialException e)
            {
                _logger.LogError(e, "Invalid User Credential");
                var res = new ResponseDto(StatusCodes.Status401Unauthorized, "Invalid User Credential");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (UserNotActivateException e)
            {
                _logger.LogError(e, "User not activated");
                var res = new ResponseDto(StatusCodes.Status403Forbidden, "User not activated");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (UnableToDoActionException e)
            {
                _logger.LogError(e, "Unable to login user");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "Unable to login user");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(GetUserRegisterDto getUserRegisterDto)
        {
            try
            {
                var result = await _userAuthServices.Register(getUserRegisterDto);
                var res = new ResponseDto<ReturnUserRegisterDto>(StatusCodes.Status201Created, "User Successfully Registered", result);
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (EntityAlreadyExistsException<UserAuth> e)
            {
                _logger.LogError(e, "User already exists");
                var res = new ResponseDto(StatusCodes.Status409Conflict, "User already exists");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (UnableToDoActionException e)
            {
                _logger.LogError(e, "Unable to register user");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "Unable to register user");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
        }
    }
}
