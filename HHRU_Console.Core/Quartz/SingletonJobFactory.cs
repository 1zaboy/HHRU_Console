using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace HHRU_Console.Core.Quartz;

internal class SingletonJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SingletonJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return _serviceProvider.GetRequiredService<ResumeAdvancingJob>();
    }

    public void ReturnJob(IJob job) { }
}
