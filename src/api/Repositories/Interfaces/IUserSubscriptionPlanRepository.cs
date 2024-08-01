using api.Models;

namespace api.Repositories.Interfaces
{
    public interface IUserSubscriptionPlanRepository : IRepository<int,UserSubscriptionPlan>
    {
        public Task<bool> UserUseFreePlanAsync(int userId, int planId);

        public Task<bool> UserHasActivePlanAsync(int userId, int planId);

    }
}
