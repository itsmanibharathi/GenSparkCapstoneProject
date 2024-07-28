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
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public PropertyCategory Category { get; set; }
        public PropertyType Type { get; set; }
        public int UserId { get; set; }
        public PropertyStatus? Status { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public IEnumerable<PropertyAmenity>? Amenities { get; set; }
        public IEnumerable<PropertyMediaFile>? MediaFiles { get; set; }
        public PropertyHome? Home { get; set; }
        public PropertyLand? Land { get; set; }
    }
}
