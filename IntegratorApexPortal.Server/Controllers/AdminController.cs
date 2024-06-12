using ApexIntegratorApi.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntegratorApexPortal.Controllers
{
    [Authorize(Roles = "Admin,ApexChecker")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            // Get Authenticated user
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByEmailAsync(userId!);
                if(user != null)
                {
                    return Results.Ok(ApiResponse.Start()
                        .Data("user", user)
                        .Success(true)
                        .Message("Success")
                        .Build());
                }


                return Results.Ok(ApiResponse.Start()
                    .Data("userId", userId!)                  
                    .Success(true)
                    .Message("Success")
                    .Build());

            }
            catch(Exception ex) {                 
                return Results.Problem(ex.Message);
            }


        
        }
    }
}
