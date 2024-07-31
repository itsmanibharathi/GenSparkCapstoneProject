using api.Models.Enums;

namespace api.Models
{
    public class PropertyHome
    {
        public int PropertyId { get; set; }
        public double Area { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int YearBuilt { get; set; }
        public FurnishingStatus FurnishingStatus { get; set; }
        public int FloorNumber { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }

        // Navigation property
        public Property? Property { get; set; }
    }
}
