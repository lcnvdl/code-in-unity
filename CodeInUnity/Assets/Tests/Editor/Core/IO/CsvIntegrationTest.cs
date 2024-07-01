using System.IO;
using CodeInUnity.Core.IO;
using CodeInUnity.Core.IO.CSV;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
  public class CsvIntegrationTest
  {
    [Test]
    public void CsvStreamReader_ReadToEnd_ShouldWorkFine()
    {
      var csvString = File.ReadAllText(Application.dataPath + "\\Tests\\Editor\\Files\\Csv\\test-1.csv");

      using (var reader = new CsvStreamReader(csvString))
      {
        var result = reader.ReadToEnd();
        Assert.NotNull(result);
        Assert.AreEqual(6, result.Count);
        Assert.AreEqual("*{0, 0} = ID", result[0].ToString());
        Assert.AreEqual("*{1, 0} = Texto", result[1].ToString());
        Assert.AreEqual("{0, 1} = 1", result[2].ToString());
        Assert.AreEqual("{1, 1} = Tu eres \"bueno\"", result[3].ToString());
        Assert.AreEqual("{0, 2} = 2", result[4].ToString());
        Assert.AreEqual("{1, 2} = Tu eres \\malo\\", result[5].ToString());
      }
    }

    [Test]
    public void CsvParser_ToDataTable()
    {
      var csvString = File.ReadAllText(Application.dataPath + "\\Tests\\Editor\\Files\\Csv\\test-1.csv");

      using (var reader = new CsvStreamReader(csvString))
      {
        var dt = CsvParser.ToDataTable(reader);

        Assert.AreEqual(2, dt.Rows.Count);
        Assert.AreEqual(2, dt.Columns.Count);

        Assert.AreEqual("1", dt.Rows[0].ItemArray[0].ToString());
        Assert.AreEqual("Tu eres \"bueno\"", dt.Rows[0].ItemArray[1].ToString());

        Assert.AreEqual("2", dt.Rows[1].ItemArray[0].ToString());
        Assert.AreEqual("Tu eres \\malo\\", dt.Rows[1].ItemArray[1].ToString());
      }
    }
  }
}
