using HHApiLib.Apis;
using HHApiLib.Services;
using HHRU_Console.Core.Models;

namespace HHRU_Console.Core.Services;

internal class AccountService : IAccountService
{
    private readonly ITokenService _tokenService;
    public AccountService(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<Self> GetSelfAsync()
    {
        var token = await _tokenService.GetAccessToken();

        var responseApi = new ActiveClientApi(token);
        var client = await responseApi.GetClientAsync();

        return new Self()
        {
            Email = client.Email,
            FirstName = client.FirstName,
            LastName = client.LastName,
        };
    }
}
