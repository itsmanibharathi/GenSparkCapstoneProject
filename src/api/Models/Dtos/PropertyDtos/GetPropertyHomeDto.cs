using api.Models.Enums;

namespace api.Models.Dtos.PropertyDtos
{
    public class GetPropertyHomeDto
    {
        public int PropertyId { get; set; }
        public double Area { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int YearBuilt { get; set; }
        public FurnishingStatus FurnishingStatus { get; set; }
        public int FloorNumber { get; set; }
    }
}
