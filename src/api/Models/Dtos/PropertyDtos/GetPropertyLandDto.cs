﻿using api.Models.Enums;

namespace api.Models.Dtos.PropertyDtos
{
    public class GetPropertyLandDto
    {
        public int PropertyId { get; set; }
        public double LandArea { get; set; }
        public string? ZoningInformation { get; set; }
        public LandType LandType { get; set; }
    }
}