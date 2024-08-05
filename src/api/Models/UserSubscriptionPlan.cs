using api.Models.Enums;
using System.Configuration;

namespace api.Models
{
    public class UserSubscriptionPlan
    {
        public int UserSubscriptionPlanId { get; set; }
        public int UserId { get; set; }
        public int SubscriptionPlanId { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public int AvailableSellerViewCount { get; set; } = 0;
        public SubscriptionPlanDurationType SubscriptionPlanDurationType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation properties
        public User User { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
