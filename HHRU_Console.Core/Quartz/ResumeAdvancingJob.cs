using HHApiLib.Apis;
using HHRU_Console.Data.DAO;
using HHRU_Console.Data.Models;
using HHRU_Console.Data.Utils;
using Quartz;

namespace HHRU_Console.Core.Quartz;

internal class ResumeAdvancingJob : IJob
{
    private readonly MongoDBContext _mongoDBContext;
    public ResumeAdvancingJob(MongoDBContext mongoDBContext)
    {
        _mongoDBContext = mongoDBContext;
    }

    public async Task Execute(IJobExecutionContext context)
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
}
