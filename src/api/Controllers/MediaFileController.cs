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

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            try
            {
                var result = await _azureBlobStorageService.UploadFileAsync("containerName", file);
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
    }
}
