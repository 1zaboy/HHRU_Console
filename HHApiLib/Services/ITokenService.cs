namespace HHApiLib.Services;

public interface ITokenService
{
    Task<string> GetAccessToken();
}
