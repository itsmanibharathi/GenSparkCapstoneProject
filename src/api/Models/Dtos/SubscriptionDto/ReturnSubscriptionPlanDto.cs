using api.Models.Enums;

namespace api.Models.Dtos.SubscriptionDto
{
    
    public class ReturnSubscriptionPlanDto
    {
        public int SubscriptionPlanId { get; set; }
        public string SubscriptionPlanName { get; set; }
        public string SubscriptionPlanDescription { get; set; }
        public decimal SubscriptionPlanPrice { get; set; }
        public int SubscriptionPlanDuration { get; set; }
        public SubscriptionPlanDurationType SubscriptionPlanDurationType { get; set; }
        public bool IsActive { get; set; }
    }
}
