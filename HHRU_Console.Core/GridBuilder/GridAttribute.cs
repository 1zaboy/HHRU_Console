using HHRU_Console.Core.Models;

namespace HHRU_Console.Core.GridBuilder;

internal class GridColumnSettingAttribute : Attribute
{
    public GridColumnSettingAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

internal class GridRowActioinAttrivute : Attribute
{
    public GridRowActioinAttrivute(GridActionType type)
    {
        Type = type;
    }

    public GridActionType Type { get; }
}
