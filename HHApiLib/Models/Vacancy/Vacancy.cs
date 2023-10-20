namespace HHApiLib.Models.Vacancy;

public class Vacancy
{
    public string Id { get; set; }
    public bool Premium { get; set; }
    public string Name { get; set; }
    public object Department { get; set; }
    public bool HasTest { get; set; }
    public bool ResponseLetterRequired { get; set; }
    public VacancyArea Area { get; set; }
    public object Salary { get; set; }
    public VacancyType Type { get; set; }
    public VacancyAddress Address { get; set; }
    public object ResponseUrl { get; set; }
    public object SortPointDistance { get; set; }
    public DateTime PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Archived { get; set; }
    public string ApplyAlternateUrl { get; set; }
    public object ShowLogoInSearch { get; set; }
    public object InsiderInterview { get; set; }
    public string Url { get; set; }
    public string AlternateUrl { get; set; }
    public VacancyEmployer Employer { get; set; }
    public VacancyProfessionalRole[] ProfessionalRoles { get; set; }
}
