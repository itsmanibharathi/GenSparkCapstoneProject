using api.Models.Dtos.PropertyAmenityDtos;
using api.Models.Dtos.PropertyMediaFile;
using api.Models.Enums;

namespace api.Models.Dtos.PropertyDtos
{
    public class EditPropertyDto
    {
        public int PropertyId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Landmark { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public PropertyCategory Category { get; set; }
        public PropertyType Type { get; set; }
        public int UserId { get; set; }
        public PropertyStatus? Status { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }

        // Navigation properties
        public IEnumerable<EditPropertyAmenityDto>? Amenities { get; set; }
        public IEnumerable<EditPropertyMediaFileDto>? MediaFiles { get; set; }
        public EditPropertyHomeDto? Home { get; set; }
        public EditPropertyLandDto? Land { get; set; }
    }
}
