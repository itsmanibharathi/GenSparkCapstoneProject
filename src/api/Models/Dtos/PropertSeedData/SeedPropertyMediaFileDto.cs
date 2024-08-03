using api.Models.Enums;

namespace api.Models.Dtos.PropertSeedData
{
    public class SeedPropertyMediaFileDto
    {
        public string? Title { get; set; }
        public MediaType Type { get; set; }
        public string? Url { get; set; }
    }
}
