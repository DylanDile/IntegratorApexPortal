using ApexIntegratorApi.Core;
using IntegratorDataAccess.Models;
using Microsoft.AspNetCore.Builder;

namespace ApexIntegratorApi
{
    public static class Api
    {
        public static void ConfigureApi(this WebApplication app)
        {
            // All of route mappings are defined here
            app.MapGet("/generated_returns", GetReturns);
            app.MapGet("/generated_returns/{id}", GetReturnById);
            app.MapGet("/generated_returns/by/institution/{id}", GetReturnsByInstId);
            app.MapGet("/generated_returns/by/date/{date}", GetReturnsByDate);
            app.MapGet("/generated_returns/submission_pack/consolidated", GetConsolidatedReturnsBySubmissionPack);
            app.MapGet("/generated_returns/by/submission_pack/{submissionPack}", GetReturnsBySubmissionPack);
            app.MapPost("/generated_returns/regenerate", ReGenerateReturn);
           
        }

        private static async Task<IResult> GetReturns(IGeneratedReturnsAccess data)
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

        private static async Task<IResult> GetReturnById(IGeneratedReturnsAccess data, long id)
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


        private static async Task<IResult> GetReturnsByInstId(IGeneratedReturnsAccess data, long id)
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

        private static async Task<IResult> GetReturnsBySubmissionPack(IGeneratedReturnsAccess data, string submissionPack)
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


        private static async Task<IResult> GetReturnsByDate(IGeneratedReturnsAccess data, DateTime date)
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

        private static async Task<IResult> GetConsolidatedReturnsBySubmissionPack(IGeneratedReturnsAccess data, IReturnsAccess returns)
        {
            try
            {
                var submissionPacks = await returns.GetSubmissionPacks();
                List<SubmissionPackModel> models = new List<SubmissionPackModel>();
                foreach (var item in submissionPacks)
                {
                    models.Add(item);
                }

                if(submissionPacks == null)
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


        private static async Task<IResult> ReGenerateReturn(IGeneratedReturnsAccess data, IInstitutionsAccess institutionsAccess, int id, string[] sheets)
        {
            try
            {
                if(sheets.Length == 0)
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

                if(insitution == null)
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
                foreach(string sheet in sheets)
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
