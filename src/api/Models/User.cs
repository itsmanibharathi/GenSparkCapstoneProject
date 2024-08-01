using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? UserProfileImageUrl { get; set; }
        public bool? IsOwner { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public string? TeneantVerificationCode { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }

        // Navigation property
        public ICollection<Property>? Property { get; set; }
        public UserAuth UserAuth { get; internal set; }
        public UserVerify? UserVerify { get; internal set; }
        public ICollection<UserSubscriptionPlan>? UserSubscriptionPlan { get; set; }
    }
}
