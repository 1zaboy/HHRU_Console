using HHRU_Console.Data.Models;

namespace HHRU_Console.Data.DAO;

public interface IUserDAO : IDataAccessObject
{
    Task<UserEntity> GetAsync(string id);
    Task<string> SetAsync(UserEntity entity);
}
