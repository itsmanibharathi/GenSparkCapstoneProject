using api.Models.Enums;

namespace api.Models.Dtos.PropertyMediaFile
{
    public class ReturnPropertyMediaFileDto
    {
        public int MediaFileId { get; set; }
        public string? Title { get; set; }
        public MediaType Type { get; set; }
        public string? Url { get; set; }
        public int PropertyId { get; set; }
        public DateTime CreateAt { get; set; } 
        public DateTime? UpdateAt { get; set; }
    }
}
