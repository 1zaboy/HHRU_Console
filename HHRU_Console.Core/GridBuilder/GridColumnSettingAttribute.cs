namespace HHRU_Console.Core.GridBuilder;

internal class GridColumnSettingAttribute : Attribute
{
    public GridColumnSettingAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
