namespace HHRU_Console.Core.Models;

public class Layout
{
    public List<ColumnHeaderDefinition> ColumnHeaders { get; set; } = new List<ColumnHeaderDefinition>();
    public List<RowDefinition> Rows { get; set; } = new List<RowDefinition>();
    public List<ColumnDefinition> Columns { get; set; } = new List<ColumnDefinition>();

}
