using api.Models.Enums;

namespace api.Models
{
    public class MediaFile
    {
        public int MediaFileId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public MediaType Type { get; set; }
        public string? Url { get; set; }
        public int PropertyId { get; set; }
        // Navigation property
        public Property? Property { get; set; }
    }

}
