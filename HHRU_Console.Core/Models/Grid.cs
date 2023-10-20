namespace HHRU_Console.Core.Models;

public class Grid
{
    public Layout Layout { get; set; }

    public List<object[]> Data { get; } = new();
}
