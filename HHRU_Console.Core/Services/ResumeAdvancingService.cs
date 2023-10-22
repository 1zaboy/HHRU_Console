using HHApiLib.Services;
using HHRU_Console.Core.Quartz;
using HHRU_Console.Data.Models;
using Quartz;
using Quartz.Impl;

namespace HHRU_Console.Core.Services;

internal class ResumeAdvancingService
{
    private const string GROUP_NAME = "RA_GROUP";
    private List<ResumeUpdateEntity> _resumeUpdateEntities;
    private IScheduler _scheduler;

    private readonly ITokenService _tokenService;
    public ResumeAdvancingService(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task Init(List<ResumeUpdateEntity> entities)
    {
        _resumeUpdateEntities = new List<ResumeUpdateEntity>(entities);

        _scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        await _scheduler.Start();

        await InitAddItems();
    }

    private async Task InitAddItems()
    {
        List<Task> tasks = new List<Task>();

        foreach (var entity in _resumeUpdateEntities)
            tasks.Add(AddAsync(entity));

        await Task.WhenAll(tasks);
    }

    public async Task AddAsync(ResumeUpdateEntity model)
    {
        var token = await _tokenService.GetAccessTokenAsync();

        var jobParams = new Dictionary<string, string>
        {
            {"token",  token},
            {"resumeId",  model.Id},
        };

        IJobDetail job = JobBuilder.Create<ResumeAdvancingJob>()
            .SetJobData(new JobDataMap(jobParams))
            .WithIdentity(model.Id)
            .Build();

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(model.Id, GROUP_NAME)
            .StartAt(model.AdcanvingAt.HasValue ? model.AdcanvingAt.Value : DateTime.UtcNow)
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(250)
                .RepeatForever())
            .Build();

        await _scheduler.ScheduleJob(job, trigger);
    }

    public async Task RemoveAsync(string id)
    {
        await _scheduler.DeleteJob(new JobKey(id));
    }

}
