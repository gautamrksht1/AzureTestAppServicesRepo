namespace EcommerceWebClient.Models
{
    public class ResponseDto<T>
    {
        public T? Result { get; set; }

        public bool IsSuccess { get; set; } = true;

        public List<string> Errors { get; set; } = new List<string>();
    }
}
