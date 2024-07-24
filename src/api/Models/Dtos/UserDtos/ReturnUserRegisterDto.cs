namespace api.Models.Dtos.UserDtos
{
    public class ReturnUserRegisterDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
    }
}
