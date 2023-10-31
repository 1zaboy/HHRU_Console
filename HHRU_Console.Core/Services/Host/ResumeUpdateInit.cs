using HHRU_Console.Data.DAO;
using HHRU_Console.Data.Utils;
using Microsoft.Extensions.Logging;

namespace HHRU_Console.Core.Services.Host;

internal class ResumeUpdateInit : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly ILogger<ResumeUpdateInit> _logger;
    private readonly MongoDBContext _mongoDBContext;
    private readonly ResumeAdvancingService _resumeAdvancingService;

    public ResumeUpdateInit(ILogger<ResumeUpdateInit> logger, MongoDBContext mongoDBContext, ResumeAdvancingService resumeAdvancingService)
    {
        _logger = logger;
        _mongoDBContext = mongoDBContext;
        _resumeAdvancingService = resumeAdvancingService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var dao = _mongoDBContext.Get<IResumeUpdateDAO>();

            var models = await dao.GetAllAsync();

            if (models.Any())
                await _resumeAdvancingService.Init(models.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception("Something went wrong");
        }
    }
}
