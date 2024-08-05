using api.Models;
using api.Models.Enums;

namespace api.Repositories.Interfaces
{
    public interface IUserSubscriptionPlanRepository : IRepository<int,UserSubscriptionPlan>
    {
        public Task<UserSubscriptionPlan> UserSubscriptionPlanAsync(int userId, SubscriptionPlanDurationType subscriptionPlanDurationType);
        public Task<bool> UserUseFreePlanAsync(int userId, int planId);

        public Task<bool> UserHasActivePlanAsync(int userId, int planId);

    }
}
