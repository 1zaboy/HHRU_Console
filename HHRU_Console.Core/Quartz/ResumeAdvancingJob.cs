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
        var token = context.MergedJobDataMap.GetString("token");
        var resumeId = context.MergedJobDataMap.GetString("resumeId");

        var resumeApi = new ResumeApi(token);
        await resumeApi.Republish(resumeId);

        var resume = await resumeApi.GetResumeAsync(resumeId);

        var dao = _mongoDBContext.Get<IResumeUpdateDAO>();
        await dao.UpdateAsync(resumeId, new ResumeUpdateEntity
        {
            Id = resumeId,
            IsAdcanving = true,
            AdcanvingAt = resume.NextPublishAt
        });
    }
}
