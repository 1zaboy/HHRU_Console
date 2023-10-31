namespace HHRU_Console.Core.Models;

public class UserAccessData
{
    public string Email { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public TimeSpan ExpiresIn { get; set; }
}
