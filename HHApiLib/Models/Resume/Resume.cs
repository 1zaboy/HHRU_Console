using System.Text.Json.Serialization;

namespace HHApiLib.Models.Resume;

public class Resume
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Object Area { get; set; }
    public int Age { get; set; }
    public Object Gender { get; set; }
    public double? Salary { get; set; }
    public string Photo { get; set; }
    public Object TotalExperience { get; set; }
    public List<Object> Certificate { get; set; }
    public List<Object> HiddenFields { get; set; }
    public Object Actions { get; set; }
    public string Url { get; set; }
    public string AlternateUrl { get; set; }
    public string Id { get; set; }
    public Object Download { get; set; }
    public Object Platform { get; set; }
    public Object Education { get; set; }
    public List<Object> Experience { get; set; }
    public bool Marked { get; set; }    
    public bool Finished { get; set; }
    public Object Status { get; set; }
    public Object Access { get; set; }
    public List<Object> PaidServices { get; set; }
    public bool Blocked { get; set; }
    public bool CanPublishOrUpdate { get; set; }
    public DateTime NextPublishAt { get; set; }
    public List<Object> Contact { get; set; }
    public bool Visible { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public Object SimilarVacancies { get; set; }
    public int NewViews { get; set; }
    public int TotalViews { get; set; }
    public string ViewsUrl { get; set; }
}
