using System.Text.Json.Serialization;

namespace HHApiLib.Models.Resume;

public class Resume
{
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("middle_name")]
    public string MiddleName { get; set; }
    public string Title { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
    public Object Area { get; set; }
    public int Age { get; set; }
    public Object Gender { get; set; }
    public double? Salary { get; set; }
    public string Photo { get; set; }
    [JsonPropertyName("total_experience")]
    public Object TotalExperience { get; set; }
    public List<Object> Certificate { get; set; }
    [JsonPropertyName("hidden_fields")]
    public List<Object> HiddenFields { get; set; }
    public Object Actions { get; set; }
    public string Url { get; set; }
    [JsonPropertyName("alternate_url")]
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
    [JsonPropertyName("paid_services")]
    public List<Object> PaidServices { get; set; }
    public bool Blocked { get; set; }
    [JsonPropertyName("can_publish_or_update")]
    public bool CanPublishOrUpdate { get; set; }
    [JsonPropertyName("next_publish_at")]
    public DateTime NextPublishAt { get; set; }
    public List<Object> Contact { get; set; }
    public bool Visible { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    [JsonPropertyName("similar_vacancies")]
    public Object SimilarVacancies { get; set; }
    [JsonPropertyName("new_views")]
    public int NewViews { get; set; }
    [JsonPropertyName("total_views")]
    public int TotalViews { get; set; }
    [JsonPropertyName("views_url")]
    public string ViewsUrl { get; set; }
}
