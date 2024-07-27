using api.Models.Enums;

namespace api.Models.Dtos.PropertyDtos
{
    public class ReturnPropertyLandDto
    {
        public int PropertyId { get; set; }
        public double LandArea { get; set; }
        public string? ZoningInformation { get; set; }
        public LandType LandType { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
