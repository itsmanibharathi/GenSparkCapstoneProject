using api.Models.Enums;

namespace api.Models.Dtos.PropertyDtos
{
    public class PropertyQueryDto
    {
        public PropertyCategory? Category { get; set; }
        public PropertyType? Type { get; set; }
        public string SearchQuery { get; set; } = "";
        public bool GetMyProperty { get; set; } = false;
    }
}
