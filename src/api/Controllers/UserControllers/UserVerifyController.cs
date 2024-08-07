﻿using api.Exceptions;
using api.Models;
using api.Models.Dtos.ResponseDtos;
using api.Models.Dtos.UserDtos;
using api.Services;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.UserControllers
{
    [Route("user/verify")]
    [ApiController]
    public class UserVerifyController : ControllerBase
    {
        private readonly IUserVerifyService _userVerifyService;
        private readonly ILogger<UserVerifyController> _logger;

        public UserVerifyController(IUserVerifyService userVerifyService, ILogger<UserVerifyController> logger)
        {
            _userVerifyService = userVerifyService;
            _logger = logger;
        }

        [HttpPut("{id}/{token}")]
        public async Task<IActionResult> ActivateUser( int id, string token)
        {
            try
            {

                var result = await _userVerifyService.VertifyUserAsync(id, token);
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
