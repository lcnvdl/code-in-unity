using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInUnity.Core.IO.CSV
{
  [Serializable]
  public class CsvCell
  {
    public int column;

    public int row;

    public bool isHeader;

    public string content;

    public CsvCell()
    {
    }

    public CsvCell(int column, int row, bool isHeader, string content = "")
    {
      this.column = column;
      this.row = row;
      this.content = content;
      this.isHeader = isHeader;
    }
  }
}
