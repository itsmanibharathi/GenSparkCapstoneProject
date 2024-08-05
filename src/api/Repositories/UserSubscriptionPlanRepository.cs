using api.Contexts;
using api.Exceptions;
using api.Models;
using api.Models.Enums;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserSubscriptionPlanRepository : Repository<int,UserSubscriptionPlan> , IUserSubscriptionPlanRepository
    {
        public UserSubscriptionPlanRepository(DbSql context) : base(context)
        {
        }

        public async Task<bool> UserUseFreePlanAsync(int userId,int planId)
        {
            try
            {
                var freePlanIds = await _context.SubscriptionPlans
                .Where(x => x.SubscriptionPlanId == planId && x.SubscriptionPlanPrice == 0)
                .Select(x => x.SubscriptionPlanId)
                .ToListAsync();
                if (freePlanIds.Count == 0)
                {
                    return false;
                }
                var userHasFreePlan = await _context.UserSubscriptionPlans
                    .AnyAsync(x => x.UserId == userId && freePlanIds.Contains(x.SubscriptionPlanId));

                return userHasFreePlan;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to check if user has free plan", e);
            }
        }

        public async Task<bool> UserHasActivePlanAsync(int userId, int planId)
        {
            try
            {
                var userHasActivePlan = await _context.UserSubscriptionPlans
                    .AnyAsync(x => x.UserId == userId && x.SubscriptionPlanId == planId && x.IsActive);

                return userHasActivePlan;

            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to check if user has active plan", e);
            }
        }


        public async Task<UserSubscriptionPlan> UserSubscriptionPlanAsync(int userId, SubscriptionPlanDurationType subscriptionPlanDurationType)
        {
            try
            {
                return await _context.UserSubscriptionPlans.FirstOrDefaultAsync(x => x.UserId == userId && subscriptionPlanDurationType == subscriptionPlanDurationType && x.IsActive ==true) ?? throw new EntityNotFoundException<UserSubscriptionPlan>();
            }
            catch (EntityNotFoundException<UserSubscriptionPlan>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get user subscription plan", e);
            }
        }

    }
}
