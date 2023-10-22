using HHRU_Console.Data.Models;

namespace HHRU_Console.Data.DAO;

public interface IResumeUpdateDAO : IDataAccessObject
{
    Task<ResumeUpdateEntity> GetAsync(string id);
    Task<IEnumerable<ResumeUpdateEntity>> GetAllAsync();
    Task<string> SetAsync(ResumeUpdateEntity model);
    Task<bool> UpdateAsync(string id, ResumeUpdateEntity entity);
    Task<bool> CheckAsync(string id);
}
