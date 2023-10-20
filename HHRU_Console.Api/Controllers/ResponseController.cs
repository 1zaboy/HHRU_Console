using HHRU_Console.Core.Models;
using HHRU_Console.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HHRU_Console.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ResponseController : ControllerBase
{
    private readonly IResponseService _responseService;
    public ResponseController(IResponseService responseService)
    {
        _responseService = responseService;
    }

    [HttpGet()]
    public async Task<Grid> Get()
    {
        return await _responseService.GetResponsesAsync();
    }
}
