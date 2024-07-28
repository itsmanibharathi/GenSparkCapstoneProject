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

    }
}
