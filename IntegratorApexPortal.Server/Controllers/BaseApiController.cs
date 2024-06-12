using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntegratorApexPortal.Server.Controllers
{ 
    public abstract class BaseApiController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public BaseApiController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected async Task<int> GetInstitutionIdAsync()
        {
            int institution = 0;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _userManager.FindByEmailAsync(userId);
                if (user != null)
                {
                    var claims = await _userManager.GetClaimsAsync(user);
                    if (claims != null)
                    {
                        var institutionIDClaim = claims.FirstOrDefault(c => c.Type == "InstitutionID");
                        var institutionID = institutionIDClaim?.Value;
                        if (institutionID != null)
                        {
                            institution = int.Parse(institutionID);
                        }
                    }
                }
            }
            return institution;
        }
    }
}
