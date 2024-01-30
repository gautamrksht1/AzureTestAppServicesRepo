using EcommerceWebClient.Models;
using EcommerceWebClient.Services.Interfaces;
using EcommerceWebClient.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace EcommerceWebClient.Services
{
    public class HttpRequestBaseService : IHttpRequestBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;

        public HttpRequestBaseService(ITokenProvider tokenProvider, IHttpClientFactory httpClientFactory)
        {
            _tokenProvider = tokenProvider;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto<T>> SendAsync<T>(APIRequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpResponseMessage? apiResponse = null;

                HttpClient httpClient = _httpClientFactory.CreateClient("ECommerceAPI");

                HttpRequestMessage message = new HttpRequestMessage();

                if (requestDto.ContentType == Utility.ContentType.MultipartFormData)
                {
                    message.Headers.Add("Accept", "*/*");
                }
                else { 
                    message.Headers.Add("Accept", "application/json");
                    if (requestDto.Data != null)
                    {
                        message.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestDto.Data), encoding:Encoding.UTF8, "application/json");
                    }
                }

                //token
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                switch (requestDto.ApiType)
                {
                    case HttpRequestType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case HttpRequestType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case HttpRequestType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                message.RequestUri = new Uri(requestDto.Url);

                apiResponse = await httpClient.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto<T> { IsSuccess = false, Errors = new List<string> { "Not Found"} };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDto<T> { IsSuccess = false, Errors = new List<string> { "Access Denied" } };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto<T> { IsSuccess = false, Errors = new List<string> { "Unauthorized" } };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto<T> { IsSuccess = false, Errors = new List<string> { "Internal Server Error" } };

                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto<T>>(apiContent);
                        var result = apiResponseDto.Result;
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto<T>
                {
                    Errors = new List<string> { ex.Message.ToString() },
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
