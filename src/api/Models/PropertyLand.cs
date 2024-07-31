using api.Models.Enums;

namespace api.Models
{
    public class PropertyLand
    {
        public int PropertyId { get; set; }
        public double LandArea { get; set; }
        public string? ZoningInformation { get; set; }
        public LandType LandType { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }

        // Navigation property
        public Property? Property { get; set; }
    }
}
