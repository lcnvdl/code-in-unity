using System.Data;

namespace CodeInUnity.Core.IO.CSV
{
  public static class CsvParser
  {
    public static DataTable ToDataTable(CsvStreamReader reader)
    {
      var cells = reader.ReadToEnd();
      var table = new DataTable();

      for (int col = 0; col < reader.ColumnsCount; col++)
      {
        var cell = cells.Find(m => m.column == col && m.row == 0);
        if (cell != null)
        {
          table.Columns.Add(new DataColumn(cell.content));
        }
        else
        {
          table.Columns.Add(new DataColumn($"Column{col}"));
        }
      }

      for (int row = 1; row < reader.RowsCount; row++)
      {
        var newRow = table.NewRow();
        var itemArray = new object[reader.ColumnsCount];

        for (int col = 0; col < reader.ColumnsCount; col++)
        {
          var cell = cells.Find(m => m.column == col && m.row == row);
          if (cell != null)
          {
            itemArray[col] = cell.content;
          }
          else
          {
            itemArray[col] = string.Empty;
          }
        }

        newRow.ItemArray = itemArray;

        table.Rows.Add(newRow);
      }

      return table;
    }
  }
}
