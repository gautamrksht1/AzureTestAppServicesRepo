using EcommerceWebClient.Services.Interfaces;
using EcommerceWebClient.Utility;

namespace EcommerceWebClient.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public void ClearToken()
        {
            _contextAccessor.HttpContext.Response.Cookies.Delete(AppStaticData.TokenCookie);
        }

        public string? GetToken()
        {
           string? token = null;
           bool? hasToken = _contextAccessor.HttpContext.Request.Cookies.TryGetValue(AppStaticData.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(AppStaticData.TokenCookie, token);
        }
    }
}
