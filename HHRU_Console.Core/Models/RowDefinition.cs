namespace HHRU_Console.Core.Models;

public class RowDefinition
{
    public string Key { get; set; }
    public object Tag { get; set; }
    public List<GridAction> Actions { get; set; }
}
