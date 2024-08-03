using api.Exceptions;
using api.Models.Dtos.ResponseDtos;
using api.Models.Dtos.SubscriptionDto;
using api.Services;
using api.Services.Interfaces;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.SubscriptionControllers
{
    [Route("Subscription/Plan")]

    [ApiController]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly ISubscriptionPlanService _subscriptionPlanService;
        private readonly ILogger<SubscriptionPlanController> _logger;

        public SubscriptionPlanController(ISubscriptionPlanService subscriptionPlanService, ILogger<SubscriptionPlanController> logger )
        {
            _subscriptionPlanService = subscriptionPlanService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _subscriptionPlanService.GetAsync();
                var res = new ResponseDto<IEnumerable<ReturnSubscriptionPlanDto>>(StatusCodes.Status200OK, "Subscription plans fetched", result);
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (UnableToDoActionException e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
