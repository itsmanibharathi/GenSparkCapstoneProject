namespace api.Models.Dtos.ResponseDtos
{
    public class ResponseDto<TData> where TData : class
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public TData? Data { get; set; }
        /// <summary>
        /// ResponseDto Constructor
        /// </summary>
        public ResponseDto(int status200OK)
        {
            StatusCode = 200;
            Message = "Success";
            Data = null;
        }

        /// <summary>
        /// ResponseDto Constructor
        /// </summary>
        /// <param name="statusCode"> </param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ResponseDto(int statusCode, string message, TData data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
