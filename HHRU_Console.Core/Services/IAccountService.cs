using HHRU_Console.Core.Models;

namespace HHRU_Console.Core.Services;

public interface IAccountService
{
    Task<Self> GetSelfAsync();
}
