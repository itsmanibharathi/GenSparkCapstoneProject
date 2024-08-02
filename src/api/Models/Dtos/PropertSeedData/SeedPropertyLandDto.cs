using api.Models.Enums;

namespace api.Models.Dtos.PropertSeedData
{
    public class SeedPropertyLandDto
    {
        public double LandArea { get; set; }
        public string? ZoningInformation { get; set; }
        public LandType LandType { get; set; }
    }
}
