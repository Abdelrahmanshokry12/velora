using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using velora.core.Entities.IdentityEntities;
using velora.services.Services.AuthService;
using velora.services.Services.AuthService.Dto;

namespace velora.api.Controllers
{
    public class AuthController : APIBaseController
    {
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;
        private readonly IAuthService _authService;

        public AuthController(UserManager<Person> userManager, SignInManager<Person> signInManager, IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new Person
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

           
            var role = string.IsNullOrWhiteSpace(dto.Role) ? "User" : dto.Role;
            await _userManager.AddToRoleAsync(user, role);

            return Ok($"User registered as {role}");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid credentials");

            var token = await _authService.GenerateTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new AuthResponseDto
            {
                Email = user.Email,
                Role = roles.FirstOrDefault(),
                Token = token
            });
        }
    }
}
