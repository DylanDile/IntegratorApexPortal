using ApexIntegratorApi.Core;
using IntegratorDataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApexIntegratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {
        [HttpGet]
        public async Task<IResult> Get(IInstitutionsAccess data)
        {
            try
            {
                //var institutions = await data.GetInstitutions();

                List<InstitutionModel> institutions = new List<InstitutionModel>();
                institutions.Add(new InstitutionModel { 
                    InstitutionID = 1,
                    InstName = "Institution 1", LEAD_CO_CODE = "INS001", LEAD_CO_NAME = "Institution 1 Description" });

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
    }
}
