using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
        private readonly ILogger<AuthController> _logger;
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;
		public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			_logger = logger;
			_mapper = mapper;
			_userManager = userManager;
			_configuration = configuration;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register(UserDto userDto)
		{
			var user = _mapper.Map<ApplicationUser>(userDto);
			user.UserName = userDto.Email;
			user.NormalizedUserName = userDto.Email.ToUpper();
			user.NormalizedEmail = userDto.Email.ToUpper();

			var res = await _userManager.CreateAsync(user, userDto.Password);

			if(res.Succeeded == false)
			{
				foreach(var error in res.Errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
					
				}

				return BadRequest(ModelState); 
			}
			await _userManager.AddClaimsAsync(user, [
					new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					new Claim("uid", user.Id)
				]);
				await _userManager.AddToRoleAsync(user, "User");
			return Accepted();
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(LoginUserDto userDto)
		{
			_logger.LogInformation($"Login Attempt for {userDto.Email}");
			try
			{
				var user = await _userManager.FindByEmailAsync(userDto.Email);
				if(user == null)
				{
					return Unauthorized();
				}

				var passwordValid = await _userManager.CheckPasswordAsync(user, userDto.Password);

				if (!passwordValid)
				{
					return Unauthorized();
				}

				string tokenString = await GenerateToken(user);

				var response = new AuthResponse
				{
					Email = userDto.Email,
					Token = tokenString,
					UserId = user.Id
				};

				return Accepted(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		private async Task<string> GenerateToken(ApplicationUser user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

			//var claims = await _userManager.GetClaimsAsync(user);
			//claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
			//claims.Union(roleClaims);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("uid", user.Id)
			}.Union(roleClaims);

			var token = new JwtSecurityToken(
				issuer: _configuration["JwtSettings:Issuer"],
				audience: _configuration["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(Convert.ToInt32(_configuration["JwtSettings:Duration"])),
				signingCredentials: credentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
