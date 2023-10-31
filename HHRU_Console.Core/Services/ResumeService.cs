using HHApiLib.Apis;
using HHApiLib.Services;
using HHRU_Console.Core.Models;
using HHRU_Console.Data.DAO;
using HHRU_Console.Data.Utils;
using Microsoft.Extensions.Logging;

namespace HHRU_Console.Core.Services;

internal class ResumeService : IResumeService
{
    private readonly ILogger<ResumeService> _logger;
    private readonly IAccessService _tokenService;
    private readonly MongoDBContext _mongoDBContext;
    private readonly ResumeAdvancingService _resumeAdvancingService;

    public ResumeService(ILogger<ResumeService> logger, IAccessService tokenService, MongoDBContext mongoDBContext, ResumeAdvancingService resumeAdvancingService)
    {
        _logger = logger;
        _tokenService = tokenService;
        _mongoDBContext = mongoDBContext;
        _resumeAdvancingService = resumeAdvancingService;
    }

    public async Task<List<Resume>> GetResumesAsynk()
    {
        try
        {
            var token = await _tokenService.GetAccessTokenAsync();

            var resumeApi = new ResumeApi(token);
            var resumes = await resumeApi.GetResumesAsync();

            var dao = _mongoDBContext.Get<IResumeUpdateDAO>();
            var resumeList = new List<Resume>();

            foreach (var x in resumes.Items)
            {
                var obj = await dao.GetAsync(x.Id);
                var resume = new Resume
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = "",
                    IsAdvancing = obj != null,
                };
                resumeList.Add(resume);
            }

            return resumeList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception("Something went wrong");
        }
    }

    public async Task SetAdvancingStatusAsynk(string resumeId, bool isAdvancing)
    {
        try
        {
            var email = await _tokenService.GetEmailAsync();
            var model = new Data.Models.ResumeUpdateEntity
            {
                Id = resumeId,
                IsAdcanving = isAdvancing,
                OwnerEmail = email,
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
            if (isAdvancing)
            {
                await _resumeAdvancingService.AddAsync(model);
            }
            else
            {
                await _resumeAdvancingService.RemoveAsync(model.Id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception("Something went wrong");
        }
    }
}
