using ApexIntegratorApi.Core;
using IntegratorApexPortal.Server.Core;
using IntegratorDataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace IntegratorApexPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratedReturnsController : ControllerBase
    {

        [HttpGet]
        public async Task<IResult> GetReturns(IGeneratedReturnsAccess data)
        {
            try
            {
                var genReturns = await data.GetGeneratedReturns();
                return Results.Ok(genReturns);
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IResult> GetReturnById(IGeneratedReturnsAccess data, long id)
        {
            try
            {
                var genReturns = await data.GetGeneratedReturnsById(id);
                if (genReturns == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(ApiResponse.Start()
                            .Data("model", genReturns)
                            .Success(true)
                            .Message("Generated Returns retrieved successfully")
                            .Build()
                    );
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("by/institution/{id}")]
        public async Task<IResult> GetReturnsByInstId(IGeneratedReturnsAccess data, long id)
        {
            try
            {
                var genReturns = await data.GetGeneratedReturnsByInstId(id);
                if (genReturns == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(
                    ApiResponse.Start()
                        .Data("list", genReturns)
                        .Success(true)
                        .Message("Generated Returns retrieved successfully")
                        .Build());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("by/submission_pack/{submissionPack}")]
        public async Task<IResult> GetReturnsBySubmissionPack(IGeneratedReturnsAccess data, string submissionPack)
        {
            try
            {
                var genReturns = await data.GetGeneratedReturnsBySubmissionPack(submissionPack);
                if (genReturns == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(
                    ApiResponse.Start()
                        .Data("list", genReturns)
                        .Success(true)
                        .Message("Generated Returns retrieved successfully")
                        .Build());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("by/date/{date}")]
        public async Task<IResult> GetReturnsByDate(IGeneratedReturnsAccess data, DateTime date)
        {
            try
            {
                var genReturns = await data.GetGeneratedReturnsByDate(date);
                if (genReturns == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(genReturns);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("submission_pack/consolidated")]
        public async Task<IResult> GetConsolidatedReturnsBySubmissionPack(IGeneratedReturnsAccess data, IReturnsAccess returns)
        {
            try
            {
                var submissionPacks = await returns.GetSubmissionPacks();
                List<SubmissionPackModel> models = new List<SubmissionPackModel>();
                foreach (var item in submissionPacks)
                {
                    models.Add(item);
                }

                if (submissionPacks == null)
                {
                    return Results.NotFound();
                }
                var genReturns = await data.GetGeneratedReturnsCountBySubmissionPack(models);
                if (genReturns == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(ApiResponse.Start()
                        .Data("list", genReturns)
                        .Success(true)
                        .Message("Generated Returns retrieved successfully")
                        .Build());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("regenerate")]
        public async Task<IResult> ReGenerateReturn(IGeneratedReturnsAccess data, IInstitutionsAccess institutionsAccess, int id, string[] sheets)
        {
            try
            {
                if (sheets.Length == 0)
                {
                    return Results.BadRequest(
                        ApiResponse.Start()
                            .Data("model", "No sheets selected")
                            .Success(false)
                            .Message("No sheets selected")
                            .Build()
                        );
                }

                var genReturns = await data.GetGeneratedReturnsById(id);
                if (genReturns == null)
                {
                    return Results.NotFound(
                        ApiResponse.Start()
                            .Data("model", "Generated Returns not found")
                            .Success(false)
                            .Message("Generated Returns not found")
                            .Build()
                        );
                }

                GeneratedReturnsModel model = genReturns;
                var insitution = await institutionsAccess.GetInstitutionById(genReturns.InstitutionID);

                if (insitution == null)
                {
                    return Results.NotFound(
                        ApiResponse.Start()
                            .Data("model", "Institution not found")
                            .Success(false)
                            .Message("Institution not found")
                            .Build()
                      );
                }

                List<ETLJob> jobs = new List<ETLJob>();
                foreach (string sheet in sheets)
                {
                    ETLJob job = new ETLJob();
                    job.LEAD_CO_CODE = insitution.LEAD_CO_CODE;
                    job.Month = model.MonthMM;
                    job.Year = model.YearYYYY;
                    job.ReturnPack = model.SubmissionPack;
                    job.ReturnName = sheet;
                    job.InstitutionID = model.InstitutionID;
                    job.ServiceNumber = 1;
                    job.DateEntered = DateTime.Now.ToString("yyyy-MM-dd");
                    job.Status = "0";
                    job.Week_Date = DateTime.Now.ToString("dd") + model.MonthMM + "_" + model.YearYYYY;
                    jobs.Add(job);
                    await data.AddETLJob(job);
                }

                return Results.Ok(ApiResponse.Start()
                        .Data("model", model)
                        .Data("jobs", jobs)
                        .Success(true)
                        .Message("Re-Generation Job initiated successfully")
                        .Build()
                    );
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
