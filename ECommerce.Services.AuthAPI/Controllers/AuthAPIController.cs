using ECommerce.Services.AuthAPI.Models.Dto;
using ECommerce.Services.AuthAPI.Services;
using ECommerce.Services.MessageBus.RabbitMQ;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.AuthAPI.Controllers
{
    [Route("api/AuthAPI")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRabbitMQAuthMessageSender _rabbitMQAuthMessageSender;
        public AuthAPIController(IAuthService authService, IRabbitMQAuthMessageSender rabbitMQAuthMessageSender)
        {
            this._authService = authService;
            this._rabbitMQAuthMessageSender = rabbitMQAuthMessageSender;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var response = new ResponseDto<string>();
            var result = await _authService.Register(dto);
            if (!string.IsNullOrWhiteSpace(result))
            {
                response.IsSuccess = false;
                response.Result = result;
                return BadRequest(response);
               
            }
            this._rabbitMQAuthMessageSender.SendMessage(dto.Email, "newUserRegistered");
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var response = new ResponseDto<LoginResponseDto>();
            var result = await this._authService.Login(login);
            if (result.Result.User == null || result.HasErrors == true)
            {
                response.IsSuccess =false;
                response.Errors.Add("Username or password is incorrect.");
                return BadRequest(response);
            }

            response.Result = result.Result;
            return Ok(response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterUserDto register)
        {
            var response = new ResponseDto<bool>();
            var result = await this._authService.AssignRole(register.Email, register.Role);
            if (result == false)
            {
                response.IsSuccess = false;
                response.Errors.Add("InValid role.");
                return BadRequest(response);
            }

            response.Result = result;
            return new OkObjectResult(new object);
            return Ok(response);
        }

    }
}
