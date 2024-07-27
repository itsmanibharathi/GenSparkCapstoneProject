using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyDtos;
using api.Models.Dtos.ResponseDtos;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.PropertyControllers
{
    [Authorize(Policy = "UserPolicy")]
    [Route("api/Property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ILogger<PropertyController> _logger;

        public PropertyController(IPropertyService propertyService, ILogger<PropertyController> logger )
        {
            _propertyService = propertyService;
            _logger = logger;
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> CreateProperty(GetPropertyDto getPropertyDto)
        {
            try
            {
                var result = await _propertyService.CreateAsync(getPropertyDto);
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
    }
}
