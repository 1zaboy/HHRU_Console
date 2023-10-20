using HHRU_Console.Core.Models;
using System.Reflection;

namespace HHRU_Console.Core.GridBuilder;

internal class GridBuilder<T> where T : class
{
    public Grid GetGrid(List<T> objs)
    {
        if (objs.Count == 0) throw new ArgumentNullException(nameof(objs));

        var grid = new Grid();
        grid.Layout = new Layout();

        SetColumn(grid, objs.First());
        SetRows(grid, objs);

        return grid;
    }

    private void SetColumn(Grid grid, T obj)
    {
        var tipe = obj.GetType();
        var properties = tipe.GetProperties().ToList();

        for (int i = 0; i < properties.Count; i++)
        {
            var attribute = properties[i].GetCustomAttribute<GridColumnSettingAttribute>();
            if (attribute != null)
            {
                grid.Layout.Columns.Add(new ColumnDefinition() { Id = i, Key = $"{i}", Tag = $"{i}", Name = attribute.Name });
                grid.Layout.ColumnHeaders.Add(new ColumnHeaderDefinition() { Id = i, Tag = $"{i}", Name = attribute.Name });
            }
        }
    }

    // TODO: rework func (where(x => x != null) its can be shift)
    private void SetRows(Grid grid, List<T> rows)
    {
        var i = 0;
        foreach (T row in rows) // string
        {
            grid.Layout.Rows.Add(new RowDefinition() { Key = $"{i}", Tag = $"{i}" });

            var tipe = row.GetType();
            var properties = tipe.GetProperties().ToList();

            var rowData = properties.Select(x =>
            {
                var attribute = x.GetCustomAttribute<GridColumnSettingAttribute>();
                if (attribute != null)
                    return x.GetValue(row) ?? null;
                return null;
            }).Where(x => x != null);

            var arr = rowData.ToArray();
            grid.Data.Add(arr);
            i++;
        }
    }
}
