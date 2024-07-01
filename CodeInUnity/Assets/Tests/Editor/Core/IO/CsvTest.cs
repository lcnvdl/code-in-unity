using CodeInUnity.Core.IO;
using NUnit.Framework;

namespace Tests
{
  public class CsvTest
  {
    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine_ForEmptyCSV()
    {
      var csvString = "";

      using (var reader = new CsvStreamReader(csvString))
      {
        var result = reader.ReadToEnd();
        Assert.NotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("", result[0].content);
        Assert.AreEqual(0, result[0].column);
        Assert.AreEqual(0, result[0].row);
      }
    }

    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine_ForEmptyCellsSingleRow()
    {
      var csvString = ";;;;";

      using (var reader = new CsvStreamReader(csvString))
      {
        var result = reader.ReadToEnd();
        Assert.NotNull(result);
        Assert.AreEqual(5, result.Count);
        Assert.AreEqual(1, reader.RowsCount);
        Assert.AreEqual(5, reader.ColumnsCount);
      }
    }

    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine_ForEmptyCells()
    {
      var csvString = ";;;;\n;;;;";

      using (var reader = new CsvStreamReader(csvString))
      {
        var result = reader.ReadToEnd();
        Assert.NotNull(result);
        Assert.AreEqual(10, result.Count);
        Assert.AreEqual(2, reader.RowsCount);
        Assert.AreEqual(5, reader.ColumnsCount);
      }
    }

    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine_ForCellsSingleRowNoDelimiter()
    {
      var csvString = "a;b;c";

      using (var reader = new CsvStreamReader(csvString))
      {
        var result = reader.ReadToEnd();
        Assert.NotNull(result);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(1, reader.RowsCount);
        Assert.AreEqual(3, reader.ColumnsCount);

        Assert.AreEqual("a", result[0].content);
        Assert.AreEqual("b", result[1].content);
        Assert.AreEqual("c", result[2].content);

        Assert.IsTrue(result[0].isHeader);
        Assert.IsTrue(result[1].isHeader);
        Assert.IsTrue(result[2].isHeader);
      }
    }

    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine_ForCellsMultiRowNoDelimiter()
    {
      var csvString = "a;b;c\nd;e;f";

      using (var reader = new CsvStreamReader(csvString))
      {
        var result = reader.ReadToEnd();
        Assert.NotNull(result);

        Assert.AreEqual(6, result.Count);
        Assert.AreEqual(2, reader.RowsCount);
        Assert.AreEqual(3, reader.ColumnsCount);

        Assert.AreEqual("a", result[0].content);
        Assert.AreEqual("b", result[1].content);
        Assert.AreEqual("c", result[2].content);

        Assert.AreEqual("d", result[3].content);
        Assert.AreEqual("e", result[4].content);
        Assert.AreEqual("f", result[5].content);

        Assert.IsTrue(result[0].isHeader);
        Assert.IsTrue(result[1].isHeader);
        Assert.IsTrue(result[2].isHeader);

        Assert.IsFalse(result[3].isHeader);
        Assert.IsFalse(result[4].isHeader);
        Assert.IsFalse(result[5].isHeader);
      }
    }

    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine_ForCellsSingleRowWithDelimiter()
    {
      var csvString = "\"a\";\"b\";\"c\"";

      using (var reader = new CsvStreamReader(csvString))
      {
        reader.settings.quoteAllTextCells = true;
        reader.settings.stringDelimiter = '"';

        var result = reader.ReadToEnd();
        Assert.NotNull(result);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(1, reader.RowsCount);
        Assert.AreEqual(3, reader.ColumnsCount);

        Assert.AreEqual("a", result[0].content);
        Assert.AreEqual("b", result[1].content);
        Assert.AreEqual("c", result[2].content);

        Assert.IsTrue(result[0].isHeader);
        Assert.IsTrue(result[1].isHeader);
        Assert.IsTrue(result[2].isHeader);
      }
    }

    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine_ForCellsSingleRowWithSomeDelimiters()
    {
      var csvString = "\"a\";b;\"c\"";

      using (var reader = new CsvStreamReader(csvString))
      {
        reader.settings.quoteAllTextCells = false;
        reader.settings.stringDelimiter = '"';

        var result = reader.ReadToEnd();
        Assert.NotNull(result);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(1, reader.RowsCount);
        Assert.AreEqual(3, reader.ColumnsCount);

        Assert.AreEqual("a", result[0].content);
        Assert.AreEqual("b", result[1].content);
        Assert.AreEqual("c", result[2].content);

        Assert.IsTrue(result[0].isHeader);
        Assert.IsTrue(result[1].isHeader);
        Assert.IsTrue(result[2].isHeader);
      }
    }

    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine_ForCellsWithEndOfLineInSingleRowWithDelimiter()
    {
      var csvString = "\"a\";\"b\";\"c\n\"";

      using (var reader = new CsvStreamReader(csvString))
      {
        reader.settings.quoteAllTextCells = true;
        reader.settings.stringDelimiter = '"';

        var result = reader.ReadToEnd();
        Assert.NotNull(result);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(1, reader.RowsCount);
        Assert.AreEqual(3, reader.ColumnsCount);

        Assert.AreEqual("a", result[0].content);
        Assert.AreEqual("b", result[1].content);
        Assert.AreEqual("c\n", result[2].content);

        Assert.IsTrue(result[0].isHeader);
        Assert.IsTrue(result[1].isHeader);
        Assert.IsTrue(result[2].isHeader);
      }
    }

    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine_ForCellsWithEndOfLineInSingleRowWithSomeDelimiters()
    {
      var csvString = "a;b;\"c\n\"";

      using (var reader = new CsvStreamReader(csvString))
      {
        reader.settings.quoteAllTextCells = true;
        reader.settings.stringDelimiter = '"';

        var result = reader.ReadToEnd();
        Assert.NotNull(result);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(1, reader.RowsCount);
        Assert.AreEqual(3, reader.ColumnsCount);

        Assert.AreEqual("a", result[0].content);
        Assert.AreEqual("b", result[1].content);
        Assert.AreEqual("c\n", result[2].content);

        Assert.IsTrue(result[0].isHeader);
        Assert.IsTrue(result[1].isHeader);
        Assert.IsTrue(result[2].isHeader);
      }
    }

    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine_ForCellsMultiRowWithDelimiter()
    {
      var csvString = "\"a\";\"b\";\"c\"\n\"d\";\"e\";\"f\"";

      using (var reader = new CsvStreamReader(csvString))
      {
        reader.settings.quoteAllTextCells = true;
        reader.settings.stringDelimiter = '"';

        var result = reader.ReadToEnd();
        Assert.NotNull(result);

        Assert.AreEqual(6, result.Count);
        Assert.AreEqual(2, reader.RowsCount);
        Assert.AreEqual(3, reader.ColumnsCount);

        Assert.AreEqual("a", result[0].content);
        Assert.AreEqual("b", result[1].content);
        Assert.AreEqual("c", result[2].content);

        Assert.AreEqual("d", result[3].content);
        Assert.AreEqual("e", result[4].content);
        Assert.AreEqual("f", result[5].content);

        Assert.IsTrue(result[0].isHeader);
        Assert.IsTrue(result[1].isHeader);
        Assert.IsTrue(result[2].isHeader);

        Assert.IsFalse(result[3].isHeader);
        Assert.IsFalse(result[4].isHeader);
        Assert.IsFalse(result[5].isHeader);
      }
    }
  }
}
