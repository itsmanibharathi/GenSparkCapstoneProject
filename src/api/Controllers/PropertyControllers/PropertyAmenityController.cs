using api.Exceptions;
using api.Models;
using api.Models.Dtos.PropertyAmenityDtos;
using api.Models.Dtos.ResponseDtos;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.PropertyControllers
{
    [Route("Property/Amenity")]
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
    }
}
