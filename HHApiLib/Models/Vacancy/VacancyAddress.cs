namespace HHApiLib.Models.Vacancy;

public class VacancyAddress
{
    public string City { get; set; }
    public string Street { get; set; }
    public string Building { get; set; }
    public double? Lat { get; set; }
    public double? Lng { get; set; }
    public string Description { get; set; }
    public string Raw { get; set; }
    public VacancyMetroStation[] MetroStations { get; set; }
    public string Id { get; set; }
}
