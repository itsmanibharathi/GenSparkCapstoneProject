using api.Models.Enums;

namespace api.Models
{
    public class SubscriptionPlan
    {
        public int SubscriptionPlanId { get; set; }
        public string SubscriptionPlanName { get; set; }
        public string SubscriptionPlanDescription { get; set; }
        public decimal SubscriptionPlanPrice { get; set; }
        public int SubscriptionPlanDuration { get; set; }
        public SubscriptionPlanDurationType SubscriptionPlanDurationType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public UserSubscriptionPlan UserSubscriptionPlan { get; set; }

    }
}
