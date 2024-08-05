using api.Exceptions;
using api.Models.Dtos.ResponseDtos;
using api.Models.Dtos.SubscriptionDto;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.SubscriptionControllers
{

    [Authorize(Policy = "UserPolicy")]
    [Route("Subscription/subscribe")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly IUserSubscriptionPlanService _userSubscriptionPlanService;
        private readonly ILogger<SubscribeController> _logger;

        public SubscribeController(IUserSubscriptionPlanService userSubscriptionPlanService, ILogger<SubscribeController> logger)
        {
            _userSubscriptionPlanService = userSubscriptionPlanService;
            _logger = logger;
        }

        [HttpPost("{subscriptionPlanId}") ]
        public async Task<IActionResult> PostAsync(int subscriptionPlanId)
        {
            try
            {
                int id = int.Parse(User.FindFirst("Id").Value);
                var result = await _userSubscriptionPlanService.SubscribeAsync(id,subscriptionPlanId);
                var res = new ResponseDto<ReturnUserSubscriptionPlanDto>(StatusCodes.Status200OK, "Subscription successful", result);
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
