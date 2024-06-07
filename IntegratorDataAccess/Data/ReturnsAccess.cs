using IntegratorDataAccess.DbAccess;
using IntegratorDataAccess.Models;

namespace IntegratorDataAccess.Data
{
    public class ReturnsAccess : IReturnsAccess
    {
        private readonly ISqlDataAccess _db;

        public ReturnsAccess(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<ReturnsModel>> GetReturns()
        {
            const string storedProcedure = "[dbo].[Api_Get_Returns]";
            return _db.LoadData<ReturnsModel, dynamic>(storedProcedure, new { });
        }

        public async Task<ReturnsModel?> GetReturnsById(Int64 ReturnID)
        {
            const string storedProcedure = "[dbo].[Api_Get_Return_ById]";
            var results = await _db.LoadData<ReturnsModel, dynamic>(
                storedProcedure, new { Id = ReturnID });
            return results.FirstOrDefault();
        }

        public Task<IEnumerable<SubmissionPackModel>> GetSubmissionPacks()
        {
            const string sql = "select DISTINCT ReturnPack from [RETURNS]";
            return _db.LoadDataByQuery<SubmissionPackModel>(sql);
        }
    }
}
