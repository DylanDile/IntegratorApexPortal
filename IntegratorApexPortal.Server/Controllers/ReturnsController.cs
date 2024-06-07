using ApexIntegratorApi.Core;
using Microsoft.AspNetCore.Mvc;

namespace ApexIntegratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnsController : ControllerBase
    {
        [HttpGet]
        public async Task<IResult> Get(IReturnsAccess data)
        {
            try
            {
                var allReturns = await data.GetReturns();
                return Results.Ok(allReturns);
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IResult> GetById(IReturnsAccess data, long id)
        {
            try
            {
                var returns = await data.GetReturnsById(id);
                if (returns == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(returns);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("submissionpacks")]
        public async Task<IResult> GetSubMissionPacks(IReturnsAccess data)
        {
            try
            {
                var allReturns = await data.GetSubmissionPacks();

                List<String> submissionPacks = new List<string>();
                foreach (var item in allReturns)
                {
                    submissionPacks.Add(item.ReturnPack);
                }

                return Results.Ok(ApiResponse.Start()
                        .Data("list", submissionPacks)
                        .Success(true)
                        .Message("Submission Packs retrieved successfully")
                        .Build());
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);
            }
        }
    }

}
