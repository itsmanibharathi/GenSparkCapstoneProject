using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyAmenityDtos;
using api.Models.Dtos.ResponseDtos;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.PropertyControllers
{
    [Route("property/amenity")]
    [ApiController]
    public class PropertyAmenityController : ControllerBase
    {
        private readonly IPropertyAmenityService _propertyAmenityService;
        private readonly ILogger<PropertyAmenityController> _logger;

        public PropertyAmenityController(IPropertyAmenityService propertyAmenityService, ILogger<PropertyAmenityController> logger)
        {
            _propertyAmenityService = propertyAmenityService;
            _logger = logger;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyAmenity(int id)
        {
            try
            {
                var result = await _propertyAmenityService.DeletePropertyAmenityAsync(id);
                if (result)
                {
                    var res =new ResponseDto(statusCode: StatusCodes.Status200OK, message: "Property amenity deleted successfully");
                    return StatusCode(res.StatusCode, res);
                }
                else
                {
                    var res = new ResponseDto(statusCode: StatusCodes.Status500InternalServerError, message: "An error occurred while deleting property amenity");
                    return StatusCode(res.StatusCode, res);
                }
            }
            catch (EntityNotFoundException<PropertyAmenity> ex)
            {
                _logger.LogError(ex, "Property amenity not found");
                var res = new ResponseDto(statusCode: StatusCodes.Status404NotFound, message: "Property amenity not found");
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting property amenity");
                var res = new ResponseDto(statusCode: StatusCodes.Status500InternalServerError, message: "An error occurred while deleting property amenity");
                return StatusCode(res.StatusCode, res);
            }
        }

    }
}
