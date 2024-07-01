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
  }
}
