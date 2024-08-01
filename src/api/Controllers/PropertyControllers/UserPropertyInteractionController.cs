using api.Exceptions;
using api.Models;
using api.Models.Dtos.IUserPropertyInteractionDto;
using api.Models.Dtos.ResponseDtos;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.PropertyControllers
{
    [Route("property/Interaction")]
    [Authorize(Policy = "UserPolicy")]
    [ApiController]
    public class UserPropertyInteractionController : ControllerBase
    {
        private readonly IUserPropertyInteractionService _userPropertyInteractionService;
        private readonly ILogger<UserPropertyInteractionController> _logger;

        public UserPropertyInteractionController(IUserPropertyInteractionService userPropertyInteractionService,ILogger<UserPropertyInteractionController> logger )
        {
            _userPropertyInteractionService = userPropertyInteractionService;
            _logger = logger;
        }

        [HttpPut("contact/{propertyId}")]
        public async Task<IActionResult> Intraction(int propertyId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("Id").Value);
                var result = await _userPropertyInteractionService.Contact(userId, propertyId);

                if (!result)
                {

                    var res = new ResponseDto(StatusCodes.Status500InternalServerError, "Unable to contact owner");
                    return StatusCode(res.StatusCode, res);
                }
                else
                {
                    var res = new ResponseDto(StatusCodes.Status200OK, "Contacted owner successfully");
                    return StatusCode(res.StatusCode, res);
                }
            }
            catch (EntityAlreadyExistsException<UserPropertyInteraction> ex)
            {
                _logger.LogError(ex, "UserPropertyInteraction Already Exists");
                var res = new ResponseDto(StatusCodes.Status400BadRequest, "You have already contacted the owner");
                return StatusCode(res.StatusCode, res);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error in UserPropertyInteractionController Intraction");
                var res = new ResponseDto(StatusCodes.Status400BadRequest, "Unable to contact owner");
                return StatusCode(res.StatusCode, res);
            }
            catch (EntityNotFoundException<User> ex)
            {
                _logger.LogError(ex, "User Not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "User Not Found");
                return StatusCode(res.StatusCode, res);
            }
            catch (EntityNotFoundException<Property> ex)
            {
                _logger.LogError(ex, "Propert Not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "Propert Not Found");
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "Something went wrong");
                return StatusCode(res.StatusCode, res);
            }

        }

        [HttpGet("viewOwnerInfo/{propertyId}")]
        public async Task<IActionResult> ViewOwnerInfo(int propertyId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("Id").Value);
                var result = await _userPropertyInteractionService.ViewOwnerInfo(userId, propertyId);
                var res = new ResponseDto<BuyerViewOwnerInfoDto>(StatusCodes.Status200OK, "Owner info found", result);
                return StatusCode(res.StatusCode, res);
            }
            catch (EntityAlreadyExistsException<UserPropertyInteraction> ex)
            {
                _logger.LogError(ex, "UserPropertyInteraction Already Exists");
                var res = new ResponseDto(StatusCodes.Status400BadRequest, "You have already View the owner see the history");
                return StatusCode(res.StatusCode, res);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error in UserPropertyInteractionController Intraction");
                var res = new ResponseDto(StatusCodes.Status400BadRequest, "Unable to contact owner");
                return StatusCode(res.StatusCode, res);
            }
            catch (EntityNotFoundException<User> ex)
            {
                _logger.LogError(ex, "User Not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "User Not Found");
                return StatusCode(res.StatusCode, res);
            }
            catch (EntityNotFoundException<Property> ex)
            {
                _logger.LogError(ex, "Propert Not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "Propert Not Found");
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "Something went wrong");
                return StatusCode(res.StatusCode, res);
            }

        }
    }
}
