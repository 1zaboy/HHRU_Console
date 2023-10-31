using FluentValidation;
using HHRU_Console.Api.Models;
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
    private readonly IValidator<SetAdvancingParams> _validator;

    public ResumeController(IResumeService resumeService, IValidator<SetAdvancingParams> validator)
    {
        _resumeService = resumeService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<List<Resume>> GetResumesAsync()
    {
        return await _resumeService.GetResumesAsynk();
    }

    [HttpPost("advancing")]
    public async Task SetAdvancingAsync([FromBody] SetAdvancingParams param)
    {
        await _validator.ValidateAndThrowAsync(param);
        await _resumeService.SetAdvancingStatusAsynk(param.ResumeId, param.IsAdvancing);
    }
}
