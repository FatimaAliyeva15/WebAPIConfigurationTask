using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiConfigurations.DTOs.AuthDTO;
using WebApiConfigurations.Entities.UserModel;

namespace WebApiConfigurations.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly TokenOption _tokenOption;

        public AuthController(UserManager<AppUser<Guid>> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
            _tokenOption = _configuration.GetSection("TokenOptions").Get<TokenOption>();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var existingUserByEmail = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (existingUserByEmail != null)
            {
                return BadRequest(new
                {
                    Message = "Email artıq istifadə olunub.",
                    Code = StatusCodes.Status400BadRequest
                });
            }

            var existingUserByUserName = await _userManager.FindByNameAsync(registerDTO.UserName);
            if (existingUserByUserName != null)
            {
                return BadRequest(new
                {
                    Message = "İstifadəçi adı artıq mövcuddur.",
                    Code = StatusCodes.Status400BadRequest
                });
            }


            var user = _mapper.Map<AppUser<Guid>>(registerDTO);
            var resultAppUser = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!resultAppUser.Succeeded)
            {
                return BadRequest(new
                {
                    errors = resultAppUser.Errors,
                    Code = 400
                });
            }

            var roleExists = await _roleManager.RoleExistsAsync("User");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            var resultRole = await _userManager.AddToRoleAsync(user, "User");

            if(!resultRole.Succeeded)
            {
                return BadRequest(new
                {
                    errors = resultRole.Errors,
                    Code = StatusCodes.Status400BadRequest
                });
            }

            return Ok(new
            {
                Message = "User registered successfully",
                Code = StatusCodes.Status200OK
            });

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            AppUser<Guid> user;
            if (loginDTO.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByNameAsync(loginDTO.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginDTO.UserNameOrEmail);
            }

            if (user == null) 
                return NotFound();


            bool isValidPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if(!isValidPassword)
                return Unauthorized();


            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
            JwtHeader header = new JwtHeader(signingCredentials);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            foreach (var userRole in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            JwtPayload payload = new JwtPayload(audience: _tokenOption.Audience, issuer: _tokenOption.Issuer, claims: claims, expires:DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration), notBefore: DateTime.UtcNow);
            JwtSecurityToken token = new JwtSecurityToken(header, payload);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string jwt = handler.WriteToken(token);

            return Ok(new
            {
                Token = jwt,
                StatusCode = 200,
                Expires = _tokenOption.AccessTokenExpiration
            });



        }
    }
}
