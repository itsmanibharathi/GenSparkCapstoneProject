using api.Exceptions;
using api.Models;
using api.Models.Dtos.ResponseDtos;
using api.Models.Dtos.UserDtos;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.UserControllers
{
    [Authorize(Policy = "UserPolicy")]
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger) 
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetUsers()
        {
            int id = int.Parse(User.FindFirst("Id").Value);
            Console.WriteLine("Id" + id);
            try
            {
                var result = await _userService.GetUserAsync(id);
                var res = new ResponseDto<ReturnUserDto>(StatusCodes.Status200OK, "User found", result);
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (EntityNotFoundException<User> e)
            {
                _logger.LogError(e, "User not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "User not found");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "something went wrong ");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "something went wrong");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> EditUser(GetUserEditDto getUserEditDto)
        {
            try
            {
                int id = int.Parse(User.FindFirst("Id").Value);
                var result = await _userService.UpdateUserAsync(id, getUserEditDto);
                var res = new ResponseDto<ReturnUserDto>(StatusCodes.Status200OK, "User updated successfully", result);
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (EntityNotFoundException<User> e)
            {
                _logger.LogError(e, "User not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "User not found");
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
