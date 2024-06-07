
using IntegratorDataAccess.DbAccess;
using IntegratorDataAccess.Models;

namespace IntegratorDataAccess.Data
{
    public class GeneratedReturnsAccess : IGeneratedReturnsAccess
    {
        private readonly ISqlDataAccess _db;

        public GeneratedReturnsAccess(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<GeneratedReturnsModel>> GetGeneratedReturns()
        {
            const string storedProcedure = "[dbo].[Api_Report_Get_Generated_Returns]";
            return _db.LoadData<GeneratedReturnsModel, dynamic>(storedProcedure, new { });
        }

        public Task<IEnumerable<GeneratedReturnsModel>> GetGeneratedReturnsByDate(DateTime date)
        {
            const string storedProcedure = "[dbo].[spx_Report_Get_Generated_Returns_By_Date]";
            return _db.LoadData<GeneratedReturnsModel, dynamic>(storedProcedure, new { date });
        }


        public async Task<GeneratedReturnsModel?> GetGeneratedReturnsById(Int64 Id)
        {
            const string storedProcedure = "[dbo].[Api_Get_Generated_Returns_ById]";
            var results = await _db.LoadData<GeneratedReturnsModel, dynamic>(
                storedProcedure, new { Id });
            return results.FirstOrDefault();
        }

        public Task<IEnumerable<GeneratedReturnsModel>> GetGeneratedReturnsByInstId(Int64 InstitutionID)
        {
            const string storedProcedure = "[dbo].[Api_Get_Generated_Returns_ByInstID]";
            return _db.LoadData<GeneratedReturnsModel, dynamic>(storedProcedure, new { InstitutionID });
        }

        public async Task<IEnumerable<dynamic>> GetGeneratedReturnsCountBySubmissionPack(List<SubmissionPackModel> submissionPacks)
        {
            List<GenReturnsBySubmissionModel> results = new List<GenReturnsBySubmissionModel>();

            foreach (var pack in submissionPacks)
            {
                string returnPack = pack.ReturnPack.Replace(" V01", "").Trim();
                var sql = "select count(*) as ReturnsCount from GeneratedReturns Where SubmissionPack like '" + returnPack + "%'";
                var data = await _db.LoadDataByQuery<long>(sql);
                int returnsCount = (int)data.FirstOrDefault();
                results.Add(new GenReturnsBySubmissionModel { ReturnPack = returnPack, ReturnsCount = returnsCount });
            }

            return results;
        }

        public Task<IEnumerable<GeneratedReturnsModel>> GetGeneratedReturnsBySubmissionPack(string SubmissionPack)
        {
            const string storedProcedure = "[dbo].[Api_Get_Generated_Returns_By_PackName]";
            return _db.LoadData<GeneratedReturnsModel, dynamic>(storedProcedure, new { SubmissionPack });
        }


        public Task ReGenerate(int Id)
        {
            const string storedProcedure = "[dbo].[Api_ReGenerate_Returns]";
            return _db.SaveData(storedProcedure, new { Id });
        }


        public Task AddETLJob(ETLJob job)
        {
            const string storedProcedure = "[dbo].[spx_tbl_ETL_Jobs_Insert_One]";
            return _db.SaveData(storedProcedure, new {
                LEAD_CO_CODE = job.LEAD_CO_CODE,
                Month = job.Month,
                Year = job.Year,
                ReturnPack = job.ReturnPack,
                ReturnName = job.ReturnName,
                InstitutionID = job.InstitutionID,
                ServiceNumber = job.ServiceNumber,
                DateEntered = job.DateEntered,
                Status = job.Status,
            });
        }
    }
}
