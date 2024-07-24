namespace api.Models
{
    public class UserAuth
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public User User { get; set; }
    }
}
