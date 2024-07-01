using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInUnity.Core.IO.CSV
{
  [Serializable]
  public class CsvStreamSettings
  {
    public char cellDelimiter = ';';

    public char stringDelimiter = '"';

    public bool quoteAllTextCells = false;

    public static CsvStreamSettings Default => new CsvStreamSettings();
  }
}
