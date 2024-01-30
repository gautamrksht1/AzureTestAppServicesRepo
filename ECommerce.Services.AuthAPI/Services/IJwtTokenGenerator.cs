using ECommerce.Services.AuthAPI.Models;

namespace ECommerce.Services.AuthAPI.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}
