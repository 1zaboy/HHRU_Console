namespace HHApiLib.Apis;

public abstract class ApiBase
{
    private readonly string _token;
    public ApiBase(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));

        _token = token;
    }

    protected string Token { get => _token; }
}
