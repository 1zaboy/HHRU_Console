using HHRU_Console.Core.Models;

namespace HHRU_Console.Core.Services;

public interface IResponseService
{
    Task<Grid> GetResponsesAsync();
}
