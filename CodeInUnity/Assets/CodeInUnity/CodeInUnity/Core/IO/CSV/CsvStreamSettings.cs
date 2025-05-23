using System;

namespace CodeInUnity.Core.IO.CSV
{
  [Serializable]
  public class CsvStreamSettings
  {
    public char cellDelimiter = ';';

    public char stringDelimiter = '"';

    public bool quoteAllTextCells = false;

    public bool allowMultilineCells;

    public static CsvStreamSettings Default => new CsvStreamSettings()
    {
      cellDelimiter = ';',
      stringDelimiter = '"',
      quoteAllTextCells = false,
      allowMultilineCells = true,
    };
  }
}
