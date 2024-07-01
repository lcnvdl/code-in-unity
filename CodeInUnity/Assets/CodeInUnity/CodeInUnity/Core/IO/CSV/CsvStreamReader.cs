using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeInUnity.Core.IO.CSV;

namespace CodeInUnity.Core.IO
{
  public class CsvStreamReader : IDisposable
  {
    private Stream stream;
    private StreamReader streamReader;

    //private bool endOfStream = false;

    private List<char> cache = new List<char>();

    private int currentColumn = 0;

    private int currentRow = 0;

    private int columnsCount = 0;

    private int rowsCount = 0;

    private bool hasEnded = false;

    private StringBuilder sb = new StringBuilder();

    public CsvStreamSettings settings = CsvStreamSettings.Default;

    public bool EndOfStream => this.streamReader.EndOfStream && this.cache.Count == 0 && this.hasEnded;

    public int ColumnsCount => this.columnsCount;

    public int RowsCount => this.rowsCount;

    private bool IsCurrentCellAHeader => this.currentRow == 0;

    public CsvStreamReader(string content) : this(content, Encoding.UTF8)
    {
    }

    public CsvStreamReader(string content, Encoding encoding)
    {
      var streamContent = new MemoryStream(encoding.GetBytes(content));
      this.Begin(streamContent);
    }

    public CsvStreamReader(Stream content)
    {
      this.Begin(content);
    }

    private void Begin(Stream stream)
    {
      this.stream = stream;
      this.streamReader = new StreamReader(stream);
    }

    public IEnumerable<CsvCell> ToEnumerable()
    {
      while (!this.EndOfStream)
      {
        yield return this.Next();
      }
    }

    public List<CsvCell> ReadToEnd()
    {
      var list = new List<CsvCell>();

      while (!this.EndOfStream)
      {
        var cell = this.Next();
        if (cell != null)
        {
          list.Add(cell);
        }
      }

      return list;
    }

    public CsvCell Next()
    {
      bool realEndOfStream = this.streamReader.EndOfStream && this.cache.Count == 0;

      if (realEndOfStream)
      {
        if (!this.hasEnded)
        {
          this.hasEnded = true;

          if (this.currentRow > 0)
          {
            return null;
          }

          return new CsvCell(this.currentColumn, this.currentRow, this.IsCurrentCellAHeader);
        }
        else
        {
          return null;
        }
      }

      if (this.cache.Count == 0)
      {
        string currentLine = this.streamReader.ReadLine();

        if (string.IsNullOrEmpty(currentLine))
        {
          //  Skip empty lines
          return null;
        }

        this.cache.AddRange(currentLine.TrimStart().ToCharArray());
        this.cache.Add('\n');
      }
      else if (this.currentColumn == 0 && this.cache.Count == 1 && this.cache[0] == '\n')
      {
        //  Skip empty injected lines
        this.cache.Clear();
        return null;
      }

      bool isHeader = this.IsCurrentCellAHeader;

      if (this.currentColumn == 0)
      {
        this.rowsCount++;
      }

      if (this.currentRow == 0)
      {
        this.columnsCount++;
      }

      char firstCharacter = this.cache[0];
      this.cache.RemoveAt(0);

      //  IF: Empty cell
      if (firstCharacter == this.settings.cellDelimiter || this.EndOfStream)
      {
        this.currentColumn++;
        return new CsvCell(this.currentColumn, this.currentRow, isHeader);
      }
      //  IF: Header ends (Multiline header NOT supported)
      else if (isHeader && firstCharacter == '\n')
      {
        this.currentColumn = 0;
        this.currentRow++;
        return new CsvCell(this.currentColumn, this.currentRow, isHeader);
      }
      //  IF: Is an empty row
      else if (this.currentColumn == 0 && firstCharacter == '\n')
      {
        return new CsvCell(this.currentColumn, this.currentRow, isHeader);
      }
      //  Normal cell
      else
      {
        this.sb.Clear();

        bool stringDelimiterOpen = false;
        bool stringDelimiterClosed = false;
        bool invertedBarOpen = false;

        this.cache.Insert(0, firstCharacter);
        //this.sb.Append(firstCharacter);

        int currentCharacterIndex = 0;

        while (this.cache.Count > 0 && this.cache[0] != this.settings.cellDelimiter)
        {
          char character = this.cache[0];
          this.cache.RemoveAt(0);

          if ((!stringDelimiterOpen || stringDelimiterClosed) && character == '\n')
          {
            //  End of line
            break;
          }
          else if (!invertedBarOpen && character == '\\')
          {
            this.sb.Append(character);
          }
          else if (!invertedBarOpen && character == this.settings.stringDelimiter)
          {
            if (!stringDelimiterOpen)
            {
              stringDelimiterOpen = true;
            }
            else if (!stringDelimiterClosed)
            {
              stringDelimiterClosed = true;
            }
            else
            {
              throw new InvalidDataException($"Unexpected character {character}.");
            }
          }
          else
          {
            this.sb.Append(character);

            invertedBarOpen = false;
          }

          currentCharacterIndex++;

          if (this.cache.Count == 0 && !this.streamReader.EndOfStream && stringDelimiterOpen && !stringDelimiterClosed)
          {
            string currentLine = this.streamReader.ReadLine();

            if (!string.IsNullOrEmpty(currentLine))
            {
              this.cache.AddRange(currentLine.TrimStart().ToCharArray());
              this.cache.Add('\n');
            }
          }
        }

        if (this.cache.Count > 0 && this.cache[0] == this.settings.cellDelimiter)
        {
          this.cache.RemoveAt(0);
        }

        var cell = new CsvCell(this.currentColumn, this.currentRow, isHeader, this.sb.ToString());

        if (this.cache.Count == 0)
        {
          this.currentRow++;
          this.currentColumn = 0;
        }
        else
        {
          this.currentColumn++;
        }

        if (stringDelimiterOpen && !stringDelimiterClosed)
        {
          throw new InvalidDataException($"A character {this.settings.stringDelimiter} is missing.");
        }

        if (invertedBarOpen)
        {
          throw new InvalidDataException($"A character is missing after the \"\\\".");
        }

        return cell;
      }

      throw new NotImplementedException();
    }

    public void Dispose()
    {
      if (this.streamReader != null)
      {
        this.streamReader.Dispose();
        this.streamReader = null;
      }

      if (this.stream != null)
      {
        this.stream.Dispose();
        this.stream = null;
      }

      //this.endOfStream = false;
    }
  }
}
