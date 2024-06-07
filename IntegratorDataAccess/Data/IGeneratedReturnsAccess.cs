using IntegratorDataAccess.Models;

namespace IntegratorDataAccess.Data
{
    public interface IGeneratedReturnsAccess
    {
        Task AddETLJob(ETLJob job);
        Task<IEnumerable<GeneratedReturnsModel>> GetGeneratedReturns();
        Task<IEnumerable<GeneratedReturnsModel>> GetGeneratedReturnsByDate(DateTime date);
        Task<GeneratedReturnsModel?> GetGeneratedReturnsById(long Id);
        Task<IEnumerable<GeneratedReturnsModel>> GetGeneratedReturnsByInstId(long InstitutionID);
        Task<IEnumerable<GeneratedReturnsModel>> GetGeneratedReturnsBySubmissionPack(string SubmissionPack);
        Task<IEnumerable<dynamic>> GetGeneratedReturnsCountBySubmissionPack(List<SubmissionPackModel> submissionPacks);
        Task ReGenerate(int Id);
    }
}