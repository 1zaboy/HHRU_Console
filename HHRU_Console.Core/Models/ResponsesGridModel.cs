using HHRU_Console.Core.GridBuilder;

namespace HHRU_Console.Core.Models;

partial class ResponsesGridModel
{
    [GridColumnSetting("Id")]
    public string Id { get; set; }

    [GridColumnSetting("State")]
    public string State { get; set; }

    [GridColumnSetting("Employer Name")]
    public string EmployerName { get; set; }

    [GridColumnSetting("Vacancy Title")]
    public string VacancyTitle { get; set; }

    [GridColumnSetting("Address")]
    public string VacancyAddress { get; set; }

    [GridColumnSetting("Create Date")]
    public DateTime CreateDate { get; set; }

    [GridRowActioinAttrivute(GridActionType.GoByUrl)]
    public string ActionUrl { get; set; }

}
