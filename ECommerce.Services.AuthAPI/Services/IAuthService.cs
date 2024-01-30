using ECommerce.Services.AuthAPI.Models.Dto;

namespace ECommerce.Services.AuthAPI.Services
{
    public interface IAuthService
    {
        Task<string> Register(RegisterUserDto registerUserDto);
        Task<ServiceResponse<LoginResponseDto>> Login(LoginDto loginDto);

        Task<bool> AssignRole(string email, string roleName);
    }
}
