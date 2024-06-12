using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IntegratorApexPortal.Server.Core
{
    public interface IApiAuthenticatedUser
    {
        Task<IdentityUser?> GetUserAsync(string userId);
        Task<IList<Claim>> GetUserClaimsAsync(IdentityUser user);
    }
}