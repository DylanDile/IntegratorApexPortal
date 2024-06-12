using ApexIntegratorApi.Core;
using IntegratorApexPortal.Dtos;
using IntegratorApexPortal.Server.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IntegratorApexPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var username = model.Firstname.ToLower() + model.Lastname.ToCharArray()[0].ToString().ToLower();
            var user = new IdentityUser { UserName = username, Email = model.Email, PhoneNumber = model.PhoneNumber };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Store the user's institution ID in the user's claims
                await _userManager.AddClaimAsync(user, new Claim("InstitutionID", model.InstitutionID.ToString()));

                return Ok(ApiResponse.Start()
                            .Message("User created successfully")
                            .Data("model",  result)
                            .Success(true)
                            .Build());
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var institutionIDClaim = await _userManager.GetClaimsAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                    SecurityAlgorithms.HmacSha256));

                return Ok(ApiResponse.Start()
                            .Data("token", new JwtSecurityTokenHandler().WriteToken(token))
                            .Data("roles", userRoles)
                            .Data("username", user.UserName!)
                            .Data("email", user.Email!)
                            .Data("claims", institutionIDClaim)                                                   
                            .Success(true)
                            .Build());
            }
            
            List<string> errors = new List<string>();
            errors.Add("Please provide corrent username or password");


            return UnprocessableEntity(
                    ApiUnprocessedResponse.Start()
                    .Errors("username", errors)
                    .Build());

      
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded)
                {
                    return Ok(new { message = "Role added successfully" });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Role already exists");
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok(ApiResponse.Start()
                            .Message("Role assigned successfully")
                            .Success(true)
                            .Build());
            }

            return BadRequest(result.Errors);
        }
    }
}
