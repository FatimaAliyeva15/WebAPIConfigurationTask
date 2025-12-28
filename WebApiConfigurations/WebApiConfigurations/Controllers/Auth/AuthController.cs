using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiConfigurations.DTOs;
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

        public AuthController(UserManager<AppUser<Guid>> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
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
    }
}
