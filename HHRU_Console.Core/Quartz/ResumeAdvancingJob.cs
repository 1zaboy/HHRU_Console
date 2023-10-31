using HHApiLib.Apis;
using HHApiLib.Models.Exceptions.Resume;
using HHApiLib.Models.Resume;
using HHRU_Console.Core.Services;
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
    private readonly ResumeAdvancingService _resumeAdvancingService;

    public ResumeAdvancingJob(ILogger<ResumeAdvancingJob> logger, MongoDBContext mongoDBContext, ResumeAdvancingService resumeAdvancingService)
    {
        _logger = logger;
        _mongoDBContext = mongoDBContext;
        _resumeAdvancingService = resumeAdvancingService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var resumeId = context.MergedJobDataMap.GetString("ResumeId");
            var ownerEmail = context.MergedJobDataMap.GetString("OwnerEmail");

            var dao = _mongoDBContext.Get<IUserDAO>();
            var daoRU = _mongoDBContext.Get<IResumeUpdateDAO>();

            var userEntity = await dao.GetAsync(ownerEmail);

            if (userEntity == null)
            {
                await daoRU.DeleteAsync(resumeId);
                await context.Scheduler.DeleteJob(context.JobDetail.Key);
                return;
            }

            var needUpdateTrigger = false;
            var resumeApi = new ResumeApi(userEntity.AccessToken);
            try
            {
                await resumeApi.Republish(resumeId);
            }
            catch (ResumeUpdateNotAvailableYetException ex)
            {
                needUpdateTrigger = true;
            }
            catch (ResumeNotExistOrNotAvailableException ex)
            {
                await daoRU.DeleteAsync(resumeId);
                await _resumeAdvancingService.RemoveAsync(context.JobDetail.Key.Name);
                return;
            }

            var resume = await resumeApi.GetResumeAsync(resumeId);
            await UpdateResumeUpdateDBAsync(daoRU, resumeId, resume, userEntity);

            if (needUpdateTrigger)
            {
                var newResumeUpdateEntity = await daoRU.GetAsync(resumeId);
                await _resumeAdvancingService.RemoveAsync(context.JobDetail.Key.Name);
                await _resumeAdvancingService.AddAsync(newResumeUpdateEntity);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Up resume error: {ex.Message}");
            throw new Exception("Something went wrong");
        }
    }

    private async Task UpdateResumeUpdateDBAsync(IResumeUpdateDAO dao, string resumeId, Resume resume, UserEntity userEntity)
    {
        await dao.UpdateAsync(resumeId, new ResumeUpdateEntity
        {
            Id = resumeId,
            IsAdcanving = true,
            AdcanvingAt = resume.NextPublishAt,
            OwnerEmail = userEntity.Email,
        });
    }
}
