using api.Models.Enums;

namespace api.Models.Dtos.SubscriptionDto
{
    public class ReturnUserSubscriptionPlanDto
    {
        public int UserSubscriptionPlanId { get; set; }
        public int UserId { get; set; }
        public int SubscriptionPlanId { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public string SubscriptionName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SubscriptionPlanName { get; set; }
        public string SubscriptionPlanDescription { get; set; }
        public decimal SubscriptionPlanPrice { get; set; }
        public int SubscriptionPlanDuration { get; set; }
        public SubscriptionPlanDurationType SubscriptionPlanDurationType { get; set; }
        public string? token { get; set; }
    }
}