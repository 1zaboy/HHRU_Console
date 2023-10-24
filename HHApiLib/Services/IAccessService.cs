namespace HHApiLib.Services;

public interface IAccessService
{
    Task<string> GetAccessTokenAsync();
    Task<string> GetEmailAsync();
}
