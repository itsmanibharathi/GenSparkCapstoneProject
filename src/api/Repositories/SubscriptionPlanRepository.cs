using api.Contexts;
using api.Models;
using api.Repositories.Interfaces;

namespace api.Repositories
{
    public class SubscriptionPlanRepository : Repository<int,SubscriptionPlan> ,ISubscriptionPlanRepository
    {
        public SubscriptionPlanRepository(DbSql context) : base(context)
        {
        }

    }
}
