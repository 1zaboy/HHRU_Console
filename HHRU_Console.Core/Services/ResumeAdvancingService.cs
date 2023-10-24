using HHRU_Console.Core.Quartz;
using HHRU_Console.Data.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace HHRU_Console.Core.Services;

internal class ResumeAdvancingService
{
    private List<ResumeUpdateEntity> _resumeUpdateEntities;
    private IScheduler _scheduler;
    private readonly IJobFactory _jobFactory;

    public ResumeAdvancingService(IJobFactory jobFactory)
    {
        _jobFactory = jobFactory;
    }

    public async Task Init(List<ResumeUpdateEntity> entities)
    {
        _resumeUpdateEntities = new List<ResumeUpdateEntity>(entities);

        _scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        _scheduler.JobFactory = _jobFactory;
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
        var jobParams = new Dictionary<string, string>
        {
            {"ResumeId",  model.Id},
            {"OwnerEmail",  model.OwnerEmail},
        };

        IJobDetail job = JobBuilder.Create<ResumeAdvancingJob>()
            .SetJobData(new JobDataMap(jobParams))
            .WithIdentity(model.Id)
            .Build();

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(model.Id)
            .StartAt(model.AdcanvingAt.HasValue ? model.AdcanvingAt.Value : DateTime.UtcNow)
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(250)
                .RepeatForever())
            .Build();

        await _scheduler.ScheduleJob(job, trigger);
    }

    public async Task RemoveAsync(string id)
    {
        var data = await _scheduler.GetJobDetail(new JobKey(id));
        if (data != null)
            await _scheduler.DeleteJob(new JobKey(id));
    }

}
