using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.Scripts
{
  public static class CreateNullablePostprocessor
  {
    private const string FileName = "NullableProjectPostprocessor.cs";

    [MenuItem("Assets/Create/Nullable Postprocessor", priority = 101)]
    public static void CreateScript()
    {
      string folderPath = GetSelectedFolderPath();
      string filePath = Path.Combine(folderPath, FileName);

      if (File.Exists(filePath))
      {
        Debug.LogWarning($"{FileName} already exists at: {filePath}");
        return;
      }

      File.WriteAllText(filePath, GenerateScriptContent());
      AssetDatabase.Refresh();
      Debug.Log($"Created {FileName} at: {filePath}");
    }

    private static string GetSelectedFolderPath()
    {
      if (Selection.activeObject == null)
      {
        return "Assets";
      }

      string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
      return Directory.Exists(assetPath) ? assetPath : Path.GetDirectoryName(assetPath);
    }

    private static string GenerateScriptContent()
    {
      return @"using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.Scripts
{
  public class NullableProjectPostprocessor : AssetPostprocessor
  {
    private static string OnGeneratedCSProject(string path, string content)
    {
      if (!IsNullableEnabledViaResponseFile(path))
      {
        return content;
      }

      var document = XDocument.Parse(content);
      XNamespace ns = ""http://schemas.microsoft.com/developer/msbuild/2003"";
      var propertyGroup = document.Root?.Element(ns + ""PropertyGroup"");
      if (propertyGroup != null)
      {
        propertyGroup.Add(new XElement(ns + ""Nullable"", ""enable""));
        Debug.Log($""[NullablePostprocessor] Nullable enabled in {System.IO.Path.GetFileName(path)}"");
      }

      return document.ToString();
    }

    private static bool IsNullableEnabledViaResponseFile(string csprojPath)
    {
      string assemblyName = Path.GetFileNameWithoutExtension(csprojPath);
      string responseFilePath = FindResponseFilePath(assemblyName);

      if (responseFilePath == null)
      {
        return false;
      }

      return File.ReadAllLines(responseFilePath)
          .Any(line => line.Trim().StartsWith(""-nullable"", System.StringComparison.OrdinalIgnoreCase)
                    && line.Contains(""enable"", System.StringComparison.OrdinalIgnoreCase));
    }

    private static string FindResponseFilePath(string assemblyName)
    {
      foreach (string guid in AssetDatabase.FindAssets(""t:AssemblyDefinitionAsset""))
      {
        string asmdefPath = AssetDatabase.GUIDToAssetPath(guid);
        if (Path.GetFileNameWithoutExtension(asmdefPath) != assemblyName)
        {
          continue;
        }

        string assemblyDirectory = Path.GetDirectoryName(Path.GetFullPath(asmdefPath));
        string responseFilePath = Path.Combine(assemblyDirectory, ""csc.rsp"");

        return File.Exists(responseFilePath) ? responseFilePath : null;
      }

      return null;
    }
  }
}
";
    }
  }
}
