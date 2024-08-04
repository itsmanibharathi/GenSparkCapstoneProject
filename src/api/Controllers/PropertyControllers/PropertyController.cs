using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyDtos;
using api.Models.Dtos.ResponseDtos;
using api.Services.Interfaces;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.PropertyControllers
{
    [Route("Property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ILogger<PropertyController> _logger;

        public PropertyController(IPropertyService propertyService,
            ILogger<PropertyController> logger )
        {
            _propertyService = propertyService;
           
            _logger = logger;
        }

        

        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(Response<ReturnPropertyDto>) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProperty(int id)
        {
            try
            {
                var result = await _propertyService.GetAsync(id);
                var res = new ResponseDto<ReturnPropertyDto>(StatusCodes.Status200OK, "Property found", result);
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (EntityNotFoundException<Property> e)
            {
                _logger.LogError(e, "Property not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "Property not found");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "something went wrong ");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "something went wrong");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
        }

        [HttpPost()]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> CreateProperty(CreatePropertyDto getPropertyDto)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("Id").Value);
                var result = await _propertyService.CreateAsync(userId, getPropertyDto);
                var res = new ResponseDto<ReturnPropertyDto>(StatusCodes.Status201Created, "Property created successfully", result);
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (EntityAlreadyExistsException<Property> e)
            {
                _logger.LogError(e, "Property already exists");
                var res = new ResponseDto(StatusCodes.Status409Conflict, "Property already exists");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (UnableToDoActionException e)
            {
                _logger.LogError(e, "Unable to create property");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "Unable to create property");
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
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> UpdateProperty(EditPropertyDto editPropertyDto)
        {
            try
            {
                if (editPropertyDto.UserId != int.Parse(User.FindFirst("Id").Value))
                {
                    var r = new ResponseDto(StatusCodes.Status403Forbidden, "You are not allowed to update this property");
                    return StatusCode(statusCode: r.StatusCode, value: r);
                }

                var result = await _propertyService.UpdateAsync(editPropertyDto);

                var res = new ResponseDto<ReturnPropertyDto>(StatusCodes.Status200OK, "Property updated successfully", result);
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (EntityNotFoundException<Property> e)
            {
                _logger.LogError(e, "Property not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "Property not found");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (UnableToDoActionException e)
            {
                _logger.LogError(e, "Unable to update property");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "Unable to update property");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "something went wrong ");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "something went wrong");
                return StatusCode(statusCode: res.StatusCode, value: res);
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetProperties([FromQuery] PropertyQueryDto propertyQueryDto)
        {
            try
            {
                
                int userId = int.Parse(User.FindFirst("Id").Value);
                var result = await _propertyService.SearchPropertyAsync(userId, propertyQueryDto);
                var res = new ResponseDto<IEnumerable<ReturnViewPropertyDto>>(StatusCodes.Status200OK, "Properties found", result);

                return StatusCode(statusCode: res.StatusCode, value: res);
            }
            catch (EntityNotFoundException<Property> e)
            {
                _logger.LogError(e, "Property not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "Property not found");
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
