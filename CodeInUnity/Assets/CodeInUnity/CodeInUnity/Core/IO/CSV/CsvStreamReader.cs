using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeInUnity.Core.IO.CSV;

namespace CodeInUnity.Core.IO
{
  public class CsvStreamReader : IDisposable
  {
    enum DelimiterStatus
    {
      None = 0,
      Open = 1,
      Closed = 2,
    }

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
        bool isEmpty = !this.FillCellCache(true);
        if (isEmpty)
        {
          return null;
        }
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

        DelimiterStatus stringDelimiterStatus = DelimiterStatus.None;

        this.cache.Insert(0, firstCharacter);

        int currentCharacterIndex = 0;

        char previousCharacter = '-';

        while (this.cache.Count > 0 && (this.cache[0] != this.settings.cellDelimiter || stringDelimiterStatus == DelimiterStatus.Open))
        {
          char character = this.cache[0];
          this.cache.RemoveAt(0);

          if ((stringDelimiterStatus == DelimiterStatus.None || stringDelimiterStatus == DelimiterStatus.Closed) && character == '\n')
          {
            //  End of line
            break;
          }
          else if (character == this.settings.stringDelimiter)
          {
            if (stringDelimiterStatus == DelimiterStatus.None)
            {
              stringDelimiterStatus = DelimiterStatus.Open;
            }
            else if (stringDelimiterStatus == DelimiterStatus.Open)
            {
              stringDelimiterStatus = DelimiterStatus.Closed;
            }
            else if (previousCharacter == this.settings.stringDelimiter)
            {
              stringDelimiterStatus = DelimiterStatus.Open;
              this.sb.Append(character);
            }
            else
            {
              throw new InvalidDataException($"Unexpected character '{character}'.");
            }
          }
          else
          {
            this.sb.Append(character);
          }

          currentCharacterIndex++;
          previousCharacter = character;

          if (this.cache.Count == 0 && !this.streamReader.EndOfStream && stringDelimiterStatus == DelimiterStatus.Open)
          {
            this.FillCellCache(false);
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

        if (stringDelimiterStatus == DelimiterStatus.Open)
        {
          throw new InvalidDataException($"A character {this.settings.stringDelimiter} is missing in cell [Row: {this.currentRow}, Col: {this.currentColumn}].");
        }

        return cell;
      }

      throw new NotImplementedException();
    }

    private bool FillCellCache(bool trimStart, int? recursiveDelimiters = null)
    {
      string currentLine = this.streamReader.ReadLine();

      if (string.IsNullOrEmpty(currentLine))
      {
        if (!this.settings.allowMultilineCells || !recursiveDelimiters.HasValue)
        {
          //  Skip empty lines
          return false;
        }
      }

      var lineAsCharArray = (trimStart ? currentLine.TrimStart() : currentLine).ToCharArray();

      if (this.settings.allowMultilineCells)
      {
        int delimiters = recursiveDelimiters.HasValue ? recursiveDelimiters.Value : this.CountCacheDelimiters();

        foreach (char c in lineAsCharArray)
        {
          if (c == this.settings.stringDelimiter)
          {
            if (delimiters == 1)
            {
              delimiters = 0;
            }
            else
            {
              delimiters = 1;
            }
          }

          this.cache.Add(c);
        }

        this.cache.Add('\n');

        if (delimiters == 1 && !this.streamReader.EndOfStream)
        {
          this.FillCellCache(false, delimiters);
        }
      }
      else
      {
        this.cache.AddRange(lineAsCharArray);
        this.cache.Add('\n');
      }

      return true;
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

      this.hasEnded = true;
    }

    private int CountCacheDelimiters()
    {
      int count = this.cache.Count;
      int delimiters = 0;

      for (int i = 0; i < count; i++)
      {
        if (this.cache[i] == this.settings.stringDelimiter)
        {
          if (delimiters == 1)
          {
            delimiters = 0;
          }
          else
          {
            delimiters = 1;
          }
        }
      }

      return delimiters;
    }
  }
}
