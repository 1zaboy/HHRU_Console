using HHRU_Console.Core.Quartz;
using HHRU_Console.Data.Models;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace HHRU_Console.Core.Services;

internal class ResumeAdvancingService
{
    private readonly ILogger<ResumeAdvancingJob> _logger;
    private List<ResumeUpdateEntity> _resumeUpdateEntities;
    private IScheduler _scheduler;
    private readonly IJobFactory _jobFactory;

    public ResumeAdvancingService(ILogger<ResumeAdvancingJob> logger, IJobFactory jobFactory)
    {
        _logger = logger;
        _jobFactory = jobFactory;
    }

    public async Task Init(List<ResumeUpdateEntity> entities)
    {
        try
        {
            _resumeUpdateEntities = new List<ResumeUpdateEntity>(entities);

            _scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            _scheduler.JobFactory = _jobFactory;
            await _scheduler.Start();

            await InitAddItems();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception("Something went wrong");
        }
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
        try
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

            var trigerTime = model.AdcanvingAt.HasValue && model.AdcanvingAt.Value > DateTime.UtcNow
                ? model.AdcanvingAt.Value
                : DateTime.UtcNow;

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(model.Id)
                .StartAt(trigerTime)
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(250)
                    .RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception("Something went wrong");
        }
    }

    public async Task RemoveAsync(string id)
    {
        try
        {
            var data = await _scheduler.GetJobDetail(new JobKey(id));
            if (data != null)
                await _scheduler.DeleteJob(new JobKey(id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception("Something went wrong");
        }
    }

}
