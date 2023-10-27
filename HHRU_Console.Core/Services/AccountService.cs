using AutoMapper;
using HHApiLib.Apis;
using HHApiLib.Services;
using HHRU_Console.Core.Models;
using HHRU_Console.Data.DAO;
using HHRU_Console.Data.Models;
using HHRU_Console.Data.Utils;

namespace HHRU_Console.Core.Services;

internal class AccountService : IAccountService
{
    private readonly IAccessService _tokenService;
    private readonly MongoDBContext _mongoDBContext;
    private readonly IMapper _mapper;

    public AccountService(IAccessService tokenService, MongoDBContext mongoDBContext, IMapper mapper)
    {
        _tokenService = tokenService;
        _mongoDBContext = mongoDBContext;
        _mapper = mapper;
    }

    public async Task SaveUserData(UserAccessData userAccessData)
    {
        try
        {
            var dao = _mongoDBContext.Get<IUserDAO>();
            var entity = _mapper.Map<UserEntity>(userAccessData);
            await dao.SetAsync(entity);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<Self> GetSelfAsync()
    {
        var token = await _tokenService.GetAccessTokenAsync();

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
