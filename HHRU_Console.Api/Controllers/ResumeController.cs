using HHRU_Console.Core.Models;
using HHRU_Console.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HHRU_Console.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ResumeController : ControllerBase
{
    private readonly IResumeService _resumeService;

    public ResumeController(IResumeService resumeService)
    {
        _resumeService = resumeService;
    }

    [HttpGet]
    public async Task<List<Resume>> GetResumesAsync()
    {
        return await _resumeService.GetResumesAsynk();
    }

    [HttpPost("adcancing")]
    public async Task SetAdcancingAsync([FromBody] SetAdcancingParams param)
    {
        await _resumeService.SetAdcancingStatusAsynk(param.ResumeId, param.IsAdcanving);
    }
}
