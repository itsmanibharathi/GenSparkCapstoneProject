using api.Models.Dtos.SubscriptionDto;

namespace api.Services.Interfaces
{
    public interface ISubscriptionPlanService
    {
        public Task<IEnumerable<ReturnSubscriptionPlanDto>> GetAsync();
    }
}
