using EcommerceWebClient.Models;
using EcommerceWebClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EcommerceWebClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        { 
            LoginDto loginDto = new LoginDto();
            return View(loginDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        { 
            ResponseDto<LoginResponseDto> response = await _authService.LoginAsync<LoginResponseDto>(loginDto);
            if (response != null && response.IsSuccess)
            {
                LoginResponseDto loginResponseDto = (LoginResponseDto)response.Result; //JsonSerializer.Serialize<LoginResponseDto>(response.Result);

                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = response.Errors;
                return View(loginDto);
            }

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
