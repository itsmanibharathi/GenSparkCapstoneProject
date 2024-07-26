namespace api.Models.Dtos.UserDtos
{
    public class GetUserEditDto
    {
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public IFormFile? Profileimage { get; set; }
        public bool IsActive { get; set; }
    }
}
