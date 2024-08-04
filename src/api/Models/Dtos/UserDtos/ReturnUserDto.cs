namespace api.Models.Dtos.UserDtos
{
    public class ReturnUserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? UserProfileImageUrl { get; set; }
        public bool IsOwner { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; } 
        public DateTime? UpdateAt { get; set; }
    }
}
