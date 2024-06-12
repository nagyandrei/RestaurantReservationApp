using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservationApp.Dto;
using RestaurantReservationApp.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public LoginController(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            // Authenticate user based on email and password
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            // Fetch user's role from the Roles table
            var role = await _roleRepository.GetRoleByIdAsync(user.RoleId);

            if (role == null)
            {
                return BadRequest("User role not found");
            }

            var claims = new List<Claim>
                         {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, role.RoleName) 
                        };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(30), // Token valid for 30 minutes
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Generate JWT token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return token as response
            return Ok(new { Token = tokenString });
        }
    }
}
