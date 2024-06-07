using IntegratorDataAccess.DbAccess;
using IntegratorDataAccess.Models;

namespace IntegratorDataAccess.Data
{
    public class InstitutionsAccess : IInstitutionsAccess
    {
        private readonly ISqlDataAccess _db;
        public InstitutionsAccess(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<InstitutionModel>> GetInstitutions()
        {
            const string storedProcedure = "[dbo].[Api_Get_Institutions]";
            return _db.LoadData<InstitutionModel, dynamic>(storedProcedure, new { });
        }

        public async Task<InstitutionModel?> GetInstitutionById(Int64 InstitutionID)
        {
            const string storedProcedure = "[dbo].[Api_Get_Institution_ById]";
            var results = await _db.LoadData<InstitutionModel, dynamic>(
                storedProcedure, new { InstitutionID });
            return results.FirstOrDefault();
        }
    }
}
