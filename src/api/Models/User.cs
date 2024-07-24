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
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; } = false;
        public string? VerificationToken { get; set; }
        public string? TeneantVerificationCode { get; set; }
        // Navigation property
        //public ICollection<Property>? PropertiesOwned { get; set; }
        public UserAuth UserAuth { get; internal set; }
    }
}
