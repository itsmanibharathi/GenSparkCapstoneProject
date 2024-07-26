using api.Exceptions;
using api.Models;
using api.Models.Dtos.ResponseDtos;
using api.Models.Dtos.UserDtos;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.UserController
{
    [Route("user/activate")]
    [ApiController]
    public class UserActivationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserActivationController> _logger;

        public UserActivationController(IUserService userService, ILogger<UserActivationController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPut()]
        public async Task<IActionResult> ActivateUser([FromQuery] int id, string token)
        {
            try
            {
                var result = await _userService.Activation(id, token);
                var res = new ResponseDto(StatusCodes.Status200OK,result);
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (EntityNotFoundException<User> e)
            {
                _logger.LogError(e, "User not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "User not found");
                return StatusCode(statusCode: res.StatusCode, value: res);
            } 
            catch (UnableToDoActionException e)
            {
                _logger.LogError(e, "Unable to activate user");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "Unable to activate user");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "something went wrong ");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "something went wrong");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
        }

    }
}
