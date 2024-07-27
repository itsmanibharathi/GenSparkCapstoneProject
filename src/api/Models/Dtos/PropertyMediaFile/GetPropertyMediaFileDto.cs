using api.Models.Enums;

namespace api.Models.Dtos.PropertyMediaFile
{
    public class GetPropertyMediaFileDto
    {
        public int MediaFileId { get; set; }
        public string? Title { get; set; }
        public MediaType Type { get; set; }
        public IFormFile File { get; set; }
        public int PropertyId { get; set; }
    }
}
