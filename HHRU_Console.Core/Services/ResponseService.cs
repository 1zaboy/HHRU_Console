using HHApiLib.Apis;
using HHApiLib.Services;
using HHRU_Console.Core.GridBuilder;
using HHRU_Console.Core.Models;
using Microsoft.Extensions.Logging;

namespace HHRU_Console.Core.Services;

internal class ResponseService : IResponseService
{
    private readonly IAccessService _tokenService;
    private readonly ILogger<ResponseService> _logger;

    public ResponseService(ILogger<ResponseService> logger, IAccessService tokenService)
    {
        _logger = logger;
        _tokenService = tokenService;
    }

    public async Task<Grid> GetResponsesAsync()
    {
        try
        {
            var token = await _tokenService.GetAccessTokenAsync();

            var responseApi = new ResponseApi(token);
            var responses = await responseApi.GetResponsesAsync(int.MaxValue);
            var models = responses.Items.Select(x => new ResponsesGridModel()
            {
                Id = x.Id,
                CreateDate = x.CreatedAt,
                State = x.State.Name,
                VacancyTitle = x.Vacancy.Name,
                VacancyAddress = $"{x.Vacancy.Address?.City ?? ""} {x.Vacancy.Address?.Street ?? ""}",
                EmployerName = x.Vacancy.Employer.Name,
                ActionUrl = x.Vacancy.AlternateUrl,
            });

            var gridBuilder = new GridBuilder<ResponsesGridModel>();
            return gridBuilder.GetGrid(models.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception("Something went wrong");
        }
    }
}
