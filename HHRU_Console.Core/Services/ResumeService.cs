using HHApiLib.Apis;
using HHApiLib.Services;
using HHRU_Console.Core.Models;
using HHRU_Console.Data.DAO;
using HHRU_Console.Data.Utils;

namespace HHRU_Console.Core.Services;

internal class ResumeService : IResumeService
{
    private readonly ITokenService _tokenService;
    private readonly MongoDBContext _mongoDBContext;
    private readonly ResumeAdvancingService _resumeAdvancingService;

    public ResumeService(ITokenService tokenService, MongoDBContext mongoDBContext, ResumeAdvancingService resumeAdvancingService)
    {
        _tokenService = tokenService;
        _mongoDBContext = mongoDBContext;
        _resumeAdvancingService = resumeAdvancingService;
    }

    public async Task<List<Resume>> GetResumesAsynk()
    {
        var token = await _tokenService.GetAccessTokenAsync();

        var resumeApi = new ResumeApi(token);
        var resumes = await resumeApi.GetResumesAsync();

        var dao = _mongoDBContext.Get<IResumeUpdateDAO>();
        return resumes.Items.Select(x =>
        {
            var obj = dao.GetAsync(x.Id);
            return new Resume(x.Title, obj != null);
        }).ToList();
    }

    public async Task SetAdcancingStatusAsynk(string resumeId, bool isAdcancing)
    {

        var model = new Data.Models.ResumeUpdateEntity
        {
            Id = resumeId,
            IsAdcanving = isAdcancing,
        };

        var dao = _mongoDBContext.Get<IResumeUpdateDAO>();
        if (await dao.CheckAsync(resumeId))
        {
            await dao.UpdateAsync(resumeId, model);
        }
        else
        {
            await dao.SetAsync(model);
        }

        model = await dao.GetAsync(resumeId);
        if (isAdcancing)
        {
            await _resumeAdvancingService.AddAsync(model);
        }
        else
        {
            await _resumeAdvancingService.RemoveAsync(model.Id);
        }
    }
}
