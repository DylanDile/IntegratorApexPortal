using IntegratorDataAccess.Models;

namespace IntegratorDataAccess.Data
{
    public interface IReturnsAccess
    {
        Task<IEnumerable<ReturnsModel>> GetReturns();
        Task<ReturnsModel?> GetReturnsById(long ReturnID);
        Task<IEnumerable<SubmissionPackModel>> GetSubmissionPacks();
    }
}