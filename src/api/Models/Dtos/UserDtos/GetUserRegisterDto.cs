namespace api.Models.Dtos.UserDtos
{
    public class GetUserRegisterDto
    {
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? Password { get; set; }
    }
}
