using api.Models.Enums;

namespace api.Models
{
    public class PropertyMediaFile
    {
        public int MediaFileId { get; set; }
        public string? Title { get; set; }
        public MediaType Type { get; set; }
        public string? Url { get; set; }
        public int PropertyId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        // Navigation property
        public Property? Property { get; set; }
    }

}
