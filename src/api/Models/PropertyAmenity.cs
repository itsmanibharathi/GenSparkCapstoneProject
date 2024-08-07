﻿namespace api.Models
{
    public class PropertyAmenity
    {
        public int AmenityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
        public int PropertyId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }

        // Navigation property
        public Property? Property { get; set; }
    }
}
