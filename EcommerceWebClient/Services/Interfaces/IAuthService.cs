using EcommerceWebClient.Models;

namespace EcommerceWebClient.Services.Interfaces
{
    public interface IAuthService
    {

        Task<ResponseDto<T>> LoginAsync<T>(LoginDto loginRequestDto);
         Task<ResponseDto<T>> RegisterAsync<T>(RegisterUserDto registrationRequestDto);
        Task<ResponseDto<T>> AssignRoleAsync<T>(RegisterUserDto registrationRequestDto);
    }
}
