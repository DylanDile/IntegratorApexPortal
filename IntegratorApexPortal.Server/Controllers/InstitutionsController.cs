using ApexIntegratorApi.Core;
using IntegratorApexPortal.Server.Controllers;
using IntegratorApexPortal.Server.Core;
using IntegratorDataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApexIntegratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : BaseApiController
    {
        //private readonly UserManager<IdentityUser> _userManager;

        public InstitutionsController(UserManager<IdentityUser> userManager) : base(userManager)
        {
            //_userManager = userManager;
        }

        [HttpGet]
        public async Task<IResult> Get(IInstitutionsAccess data)
        {
            try
            {
                var institutions = await data.GetInstitutions();
                var response = ApiResponse.Start()
                   .Data("data", institutions)
                   .Success(true)
                   .Message("Success")
                   .Build();

                return Results.Ok(response);
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IResult> GetById(IInstitutionsAccess data, long id)
        {
            try
            {
                var institution = await data.GetInstitutionById(id);
                if (institution == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(ApiResponse.Start()
                   .Data("data", institution)
                   .Success(true)
                   .Message("Success")
                   .Build());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/[controller]/User/Insitution")]
        public async Task<IResult> GetUserInstitution(IInstitutionsAccess data)
        {
            try
            {
                int institutionId = await GetInstitutionIdAsync();
                return Results.Ok(ApiResponse.Start()
                    .Data("instID", institutionId)
                    .Message("Insitution Found")
                    .Build());
                /*int institution = 0;
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

                                return Results.Ok(ApiResponse.Start()
                                    .Data("instID", institution)
                                    .Message("Insitution Found")
                                    .Build()
                                    );
                            }
                        }
                    }             

                }
                return Results.NotFound(ApiResponse.Start()
                   .Success(false)
                   .Message("Failed to find insitution")
                   .Build());*/
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
