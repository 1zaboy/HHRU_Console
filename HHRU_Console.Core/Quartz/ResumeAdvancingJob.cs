using HHApiLib.Apis;
using HHRU_Console.Data.DAO;
using HHRU_Console.Data.Models;
using HHRU_Console.Data.Utils;
using Microsoft.Extensions.Logging;
using Quartz;

namespace HHRU_Console.Core.Quartz;

internal class ResumeAdvancingJob : IJob
{
    private readonly ILogger<ResumeAdvancingJob> _logger;
    private readonly MongoDBContext _mongoDBContext;

    public ResumeAdvancingJob(ILogger<ResumeAdvancingJob> logger, MongoDBContext mongoDBContext)
    {
        _logger = logger;
        _mongoDBContext = mongoDBContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var resumeId = context.MergedJobDataMap.GetString("ResumeId");
            var ownerEmail = context.MergedJobDataMap.GetString("OwnerEmail");

            var dao = _mongoDBContext.Get<IUserDAO>();
            var userEntity = await dao.GetAsync(ownerEmail);

            if (userEntity == null)
                return;

            var resumeApi = new ResumeApi(userEntity.AccessToken);
            await resumeApi.Republish(resumeId);

            var resume = await resumeApi.GetResumeAsync(resumeId);

            var daoRU = _mongoDBContext.Get<IResumeUpdateDAO>();
            await daoRU.UpdateAsync(resumeId, new ResumeUpdateEntity
            {
                Id = resumeId,
                IsAdcanving = true,
                AdcanvingAt = resume.NextPublishAt,
                OwnerEmail = userEntity.Email,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Up resume error: {ex.Message}");
            throw new Exception("Something went wrong");
        }
    }
}
