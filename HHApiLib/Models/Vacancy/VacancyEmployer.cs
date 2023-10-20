namespace HHApiLib.Models.Vacancy;

public class VacancyEmployer
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string AlternateUrl { get; set; }
    public object LogoUrls { get; set; }
    public string VacanciesUrl { get; set; }
    public bool AccreditedItEmployer { get; set; }
    public bool Trusted { get; set; }
}
