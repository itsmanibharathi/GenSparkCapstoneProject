namespace api.Models.Dtos.PropertyAmenityDtos
{
    public class ReturnPropertyAmenityDto
    {
        public int AmenityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
        public int PropertyId { get; set; }
        public DateTime CreateAt { get; set; } 
        public DateTime? UpdateAt { get; set; }
    }
}
