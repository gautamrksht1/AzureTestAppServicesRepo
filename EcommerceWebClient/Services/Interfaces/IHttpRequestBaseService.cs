using EcommerceWebClient.Models;

namespace EcommerceWebClient.Services.Interfaces
{
    public interface IHttpRequestBaseService
    {
        Task<ResponseDto<T>> SendAsync<T>(APIRequestDto requestDto, bool withBearer = true);
    }
}
