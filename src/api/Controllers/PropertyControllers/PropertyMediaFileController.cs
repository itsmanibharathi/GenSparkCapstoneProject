using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyMediaFile;
using api.Models.Dtos.ResponseDtos;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.PropertyControllers
{
    [Route("Property/MediaFile")]
    [ApiController]
    public class PropertyMediaFileController : ControllerBase
    {
        private readonly IPropertyMediaFileService _propertyMediaFileService;
        private readonly ILogger<PropertyMediaFileController> _logger;

        public PropertyMediaFileController(IPropertyMediaFileService propertyMediaFileService,ILogger<PropertyMediaFileController> logger)
        {
            _propertyMediaFileService = propertyMediaFileService;
            _logger = logger;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyMediaFile(int id)
        {
            try
            {
                var result = await _propertyMediaFileService.DeletePropertyMediaFileAsync(id);
                if (result)
                {
                    var res = new ResponseDto(StatusCodes.Status200OK, "Property Media File deleted successfully");
                    return StatusCode(res.StatusCode, res);
                }
                else
                {
                    var res = new ResponseDto(StatusCodes.Status500InternalServerError, "An error occurred while deleting property media file");
                    return StatusCode(res.StatusCode, res);
                }
            }
            catch (EntityNotFoundException<PropertyMediaFile> ex)
            {
                _logger.LogError(ex, "Property Media File not found");
                var res = new ResponseDto(StatusCodes.Status404NotFound, "Property Media File not found");
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting property media file");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "An error occurred while deleting property media file");
                return StatusCode(res.StatusCode, res);
            }
        }
    }
}
