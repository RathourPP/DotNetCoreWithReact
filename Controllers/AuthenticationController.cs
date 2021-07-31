
using DummyProjectApi.BusinessModel.Model;
using DummyProjectApi.BusinessModel.Response;
using DummyProjectApi.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DummyProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("RegisterNewUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Register([FromBody] DtRegistration model)
        {
            var userExist = await _userManager.FindByNameAsync(model.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { Status = "Error", ErrorMessage = "User Already Exists" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                Name=model.Name,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                City = model.City,
                Contact = model.Contact,
                CreatedDate = model.CreatedDate,
                UpdatedDate=model.UpdatedDate,
                IsActive=model.IsActive
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { Status = "Error", ErrorMessage = "User Already Exists" });
            }
            return Ok(new BaseResponse { Status = "Success", ErrorMessage = "User Created Succesfully" });
        }

        [HttpPost("RegisterAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RegisterAdmin([FromBody] DtRegistration model)
        {
            var userExist = await _userManager.FindByNameAsync(model.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { Status = "Error", ErrorMessage = "User Already Exists" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                Name = model.Name,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                City = model.City,
                Contact = model.Contact,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                IsActive = model.IsActive
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { Status = "Error", ErrorMessage = "User Already Exists" });
            }
            if (!await _roleManager.RoleExistsAsync(UserRole.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRole.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRole.User));
            if (!await _roleManager.RoleExistsAsync(UserRole.Vendor))
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Vendor));
            if (await _roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await _userManager.AddToRoleAsync(user,UserRole.Admin);
            }
            return Ok(new BaseResponse { Status = "Success", ErrorMessage = "User Created Succesfully" });
        }

        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Login([FromBody] DtLogin model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if(user!=null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim("Key",user.Id),
                    new Claim("Name",user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                     issuer: _configuration["JWT:ValidIssuer"],
                     audience: _configuration["JWT:ValidAudience"],
                     expires: DateTime.UtcNow.AddHours(24),
                     claims: authClaims,
                     signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                     );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }
    }
}
