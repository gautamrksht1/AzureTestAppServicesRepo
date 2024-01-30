using ECommerce.Services.AuthAPI.Models;
using ECommerce.Services.AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Services.AuthAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtGenerator;
        public AuthService(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtGenerator)
        {
            _db = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = this._db.applicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                { 
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }

            return false;
        }

        public async Task<ServiceResponse<LoginResponseDto>> Login(LoginDto loginDto)
        {
            var response = new ServiceResponse<LoginResponseDto>();
            var user = this._db.applicationUsers.First(U => U.UserName.ToLower() == loginDto.Email.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (user == null || isValid == false)
            {
                response.Result = new LoginResponseDto() { User = null, Token = "" };
                return response;
            }

            UserDto userDto = new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber
            };

            var token = this._jwtGenerator.GenerateToken(user);
            LoginResponseDto loginResponseDto = new LoginResponseDto() { User = userDto, Token = token };

            response.Result = loginResponseDto;
            return response;
        }

        public async Task<string> Register(RegisterUserDto registerUserDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                Name = registerUserDto.Name,
                Email = registerUserDto.Email,
                PhoneNumber = registerUserDto.PhoneNumber,
                NormalizedEmail = registerUserDto.Email.ToUpper(),
                UserName = registerUserDto.Email.ToLower()

            };
            try
            {
                var result = await this._userManager.CreateAsync(user, registerUserDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = this._db.applicationUsers.First(u => u.UserName == registerUserDto.Email);

                    return "";
                }
            }
            catch (Exception e)
            {
            }
            return "Error Encountered";
        }
    }
}
