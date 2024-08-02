using api.Controllers.PropertyControllers;
using api.Models.Dtos.PropertSeedData;
using api.Models.Dtos.PropertyDtos;
using api.Models.Dtos.ResponseDtos;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.SeedPropertyControllers
{
    [Route("property/seed")]
    [ApiController]
    public class SeedPropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ILogger<SeedPropertyController> _logger;

        public SeedPropertyController(IPropertyService propertyService,
            ILogger<SeedPropertyController> logger)
        {
            _propertyService = propertyService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SeedProperty(IEnumerable<SeedPropertyDto> seedPropertyDtos)
        {
            try
            {
                var result = await _propertyService.SeedPropert(seedPropertyDtos);
                var res = new ResponseDto<IEnumerable<ReturnPropertyDto>>(StatusCodes.Status200OK, "Properties seeded", result);
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
