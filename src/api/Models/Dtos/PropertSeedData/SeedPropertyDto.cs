using api.Models.Dtos.PropertyAmenityDtos;
using api.Models.Dtos.PropertyDtos;
using api.Models.Dtos.PropertyMediaFile;
using api.Models.Enums;

namespace api.Models.Dtos.PropertSeedData
{
    public class SeedPropertyDto
    {
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

        public IEnumerable<SeedPropertyAmenityDto>? Amenities { get; set; }
        public IEnumerable<SeedPropertyMediaFileDto>? MediaFiles { get; set; }
        public SeedPropertyHomeDto? Home { get; set; }
        public SeedPropertyLandDto? Land { get; set; }
    }
}
