using IntegratorDataAccess.Models;

namespace IntegratorDataAccess.Data
{
    public interface IInstitutionsAccess
    {
        Task<InstitutionModel?> GetInstitutionById(long Id);
        Task<IEnumerable<InstitutionModel>> GetInstitutions();
    }
}