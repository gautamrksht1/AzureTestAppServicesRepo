using EcommerceWebClient.Utility;

namespace EcommerceWebClient.Models
{
    public class APIRequestDto
    {
            public HttpRequestType ApiType { get; set; } = HttpRequestType.GET;
            public string Url { get; set; }
            public object Data { get; set; }
            public string AccessToken { get; set; }

            public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
