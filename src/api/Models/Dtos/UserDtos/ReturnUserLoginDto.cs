namespace api.Models.Dtos.UserDtos
{
    public class ReturnUserLoginDto
    {
        public string? UserName { get; set; }
        public bool IsOwner { get; set; }
        public string UserProfileImageUrl { get; set; }
        public string? Token { get; set; }
    }
}
