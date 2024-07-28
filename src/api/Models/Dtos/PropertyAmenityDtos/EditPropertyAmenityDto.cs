namespace api.Models.Dtos.PropertyAmenityDtos
{
    public class EditPropertyAmenityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
        public int PropertyId { get; set; }
    }
}
