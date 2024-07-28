using api.Models.Enums;

namespace api.Models.Dtos.PropertyDtos
{
    public class CreatePropertyDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public PropertyCategory Category { get; set; }
        public PropertyType Type { get; set; }
        public int? UserId { get; set; }
    }
}
