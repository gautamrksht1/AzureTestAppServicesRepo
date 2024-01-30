using EcommerceWebClient.Models;
using EcommerceWebClient.Services.Interfaces;
using EcommerceWebClient.Utility;

namespace EcommerceWebClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpRequestBaseService _httpRequestBaseService;

        public AuthService(IHttpRequestBaseService httpRequestBaseService)
        {
            _httpRequestBaseService = httpRequestBaseService;
        }

        public Task<ResponseDto<T>> AssignRoleAsync<T>(RegisterUserDto registrationRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<T>> LoginAsync<T>(LoginDto loginRequestDto)
        {
            var url = AppStaticData.AuthAPIBase + "/api/AuthAPI/Login";
            return await _httpRequestBaseService.SendAsync<T>(new APIRequestDto
            {
                ApiType = Utility.HttpRequestType.POST,
                Data = loginRequestDto,
                Url = url,
                ContentType = ContentType.Json
            }, withBearer: false);
        }

        public async Task<ResponseDto<T>> RegisterAsync<T>(RegisterUserDto registrationRequestDto)
        {
            return await _httpRequestBaseService.SendAsync<T>(new APIRequestDto
            {
                ApiType = Utility.HttpRequestType.POST,
                Data = registrationRequestDto,
                Url = AppStaticData.AuthAPIBase + "/api/AuthAPI/Register",
                ContentType = ContentType.Json
            }, withBearer: false);
        }
    }
}
