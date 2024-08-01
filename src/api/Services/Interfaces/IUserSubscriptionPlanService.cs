using api.Models.Dtos.SubscriptionDto;

namespace api.Services.Interfaces
{
    public interface IUserSubscriptionPlanService
    {
        public Task<ReturnUserSubscriptionPlanDto> SubscribeAsync(int userId, int subscriptionPlanId);
    }
}
