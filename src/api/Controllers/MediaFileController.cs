using api.Models.Dtos.ResponseDtos;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("media")]
    [ApiController]
    public class MediaFileController : ControllerBase
    {
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly ILogger<MediaFileController> _logger;

        public MediaFileController(IAzureBlobStorageService azureBlobStorageService, ILogger<MediaFileController> logger )
        {
            _azureBlobStorageService = azureBlobStorageService;
            _logger = logger;
        }

        [HttpPost("{containerName}")]
        public async Task<IActionResult> UploadFile(string containerName,[FromForm] IFormFile file)
        {
            try
            {
                var result = await _azureBlobStorageService.UploadFileAsync(containerName, file);
                var res = new ResponseDto<string>(StatusCodes.Status200OK, "File uploaded", result);
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "something went wrong ");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "something went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
        }

        [HttpDelete("{containerName}/{fileName}")]
        public async Task<IActionResult> DeleteFile(string containerName,string fileName)
        {
            try
            {
                var result = await _azureBlobStorageService.DeleteFileAsync(containerName, fileName);
                if (result)
                {
                    var res = new ResponseDto(StatusCodes.Status200OK, "File deleted successfully");
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                else
                {
                    var res = new ResponseDto(StatusCodes.Status500InternalServerError, "An error occurred while deleting file");
                    return StatusCode(StatusCodes.Status500InternalServerError, res);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "something went wrong ");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "something went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
        }
        // put
        [HttpPut("{containerName}/{fileName}")]
        public async Task<IActionResult> UpdateFile(string containerName,string fileName, [FromForm] IFormFile file)
        {
            try
            {
                var result = await _azureBlobStorageService.UpdateFileAsync(containerName, fileName, file);
                var res = new ResponseDto<string>(StatusCodes.Status200OK, "File updated", result);
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "something went wrong ");
                var res = new ResponseDto(StatusCodes.Status500InternalServerError, "something went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
        }

    }
}
