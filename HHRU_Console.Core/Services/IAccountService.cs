using HHRU_Console.Core.Models;

namespace HHRU_Console.Core.Services;

public interface IAccountService
{
    Task SaveUserData(UserAccessData userAccessData);
    Task<Self> GetSelfAsync();
}
