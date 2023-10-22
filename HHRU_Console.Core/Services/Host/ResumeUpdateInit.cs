using HHRU_Console.Data.DAO;
using HHRU_Console.Data.Utils;

namespace HHRU_Console.Core.Services.Host;

internal class ResumeUpdateInit : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly MongoDBContext _mongoDBContext;
    private readonly ResumeAdvancingService _resumeAdvancingService;

    public ResumeUpdateInit(MongoDBContext mongoDBContext, ResumeAdvancingService resumeAdvancingService)
    {
        _mongoDBContext = mongoDBContext;
        _resumeAdvancingService = resumeAdvancingService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var dao = _mongoDBContext.Get<IResumeUpdateDAO>();

        var models = await dao.GetAllAsync();

        if (models.Any())
            await _resumeAdvancingService.Init(models.ToList());
    }
}
