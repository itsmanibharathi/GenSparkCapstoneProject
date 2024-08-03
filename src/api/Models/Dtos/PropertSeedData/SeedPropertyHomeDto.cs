using api.Models.Enums;

namespace api.Models.Dtos.PropertSeedData
{
    public class SeedPropertyHomeDto
    {
        public double Area { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int YearBuilt { get; set; }
        public FurnishingStatus FurnishingStatus { get; set; }
        public int FloorNumber { get; set; }
    }
}
