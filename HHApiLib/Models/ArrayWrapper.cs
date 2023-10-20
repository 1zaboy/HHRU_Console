using System.Text.Json.Serialization;

namespace HHApiLib.Models;

public class ArrayWrapper<T>
{
    public List<T> Items { get; set; }
    public int Found { get; set; }
    public int Pages { get; set; }
    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }
    public int Page { get; set; }
}
