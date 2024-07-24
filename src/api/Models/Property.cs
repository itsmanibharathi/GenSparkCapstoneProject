using api.Models.Enums;
using System;

namespace api.Models
{
    public class Property
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
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public PropertyCategory Category { get; set; }
        public PropertyType Type { get; set; }
        public int OwnerId { get; set; }
        public PropertyStatus Status { get; set; }

        // Navigation properties
        public ICollection<Amenity>? Amenities { get; set; }
        public ICollection<MediaFile>? MediaFiles { get; set; }
    }
}
