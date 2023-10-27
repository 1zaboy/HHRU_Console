using HHRU_Console.Core.Models;

namespace HHRU_Console.Core.Services;

public interface IResumeService
{
    Task<List<Resume>> GetResumesAsynk();
    Task SetAdvancingStatusAsynk(string resumeId, bool isAdvancing);
}
