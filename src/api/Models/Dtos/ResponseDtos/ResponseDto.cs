namespace api.Models.Dtos.ResponseDtos
{
    public class ResponseDto
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ResponseDto()
        {
            StatusCode = 200;
            Message = "Success";
        }
        public ResponseDto(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
