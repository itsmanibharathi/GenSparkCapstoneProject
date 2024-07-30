using api.Models.Enums;

namespace api.Models
{
    public class UserVerify
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public UserVerifyStatus UserVerifyStatus { get; set; } = UserVerifyStatus.Pending;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        // Navigation property
        public User? User { get; set; }
    }
}
