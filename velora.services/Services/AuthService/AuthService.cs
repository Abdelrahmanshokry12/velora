using Microsoft.AspNetCore.Identity;
using velora.core.Entities.IdentityEntities;
using velora.services.Services.AuthService.Dto;
using velora.services.Services.TokenService;

namespace velora.services.Services.AuthService
{
    public enum Role
    {
        User = 0,
        Admin = 1,
        Guest = 2
    }
    public class AuthService : IAuthService
    {
        private readonly SignInManager<Person> _signInManager;
        private readonly UserManager<Person> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(SignInManager<Person> signInManager,
                           UserManager<Person> userManager,
                           ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<PersonDto> LoginAsync(LoginDto loginDto , Role role)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                throw new Exception("Login failed");

            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault() ?? "User";

            return new PersonDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = await _tokenService.GenerateTokenAsync(user),
                Role = userRole
            };
        }

        public async Task<PersonDto> RegisterAsync(RegisterDto registerDto , Role role)
        {
            var existing = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existing != null)
                return null;

            var person = new Person
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(person, registerDto.Password);
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);


            await _userManager.AddToRoleAsync(person, role.ToString());

            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Token = await _tokenService.GenerateTokenAsync(person),
                Role = role.ToString()
            };
        }

    }
}

