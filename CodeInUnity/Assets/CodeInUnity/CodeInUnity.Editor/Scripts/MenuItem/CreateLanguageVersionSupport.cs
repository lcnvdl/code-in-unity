using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.Scripts
{
  public static class CreateLanguageVersionSupport
  {
    private const string LanguageVersion = "10";

    private const string LanguageVersionOption = "langversion:";

    [MenuItem("Assets/Create/C# Language Version " + LanguageVersion + " (csc.rsp)", priority = 102)]
    public static void CreateCscRsp()
    {
      string folderPath = GetSelectedFolderPath();
      string filePath = Path.Combine(folderPath, "csc.rsp");
      string optionLine = "-" + LanguageVersionOption + LanguageVersion;

      if (!File.Exists(filePath))
      {
        File.WriteAllText(filePath, optionLine + "\n");
        AssetDatabase.Refresh();
        Debug.Log($"Created csc.rsp with {optionLine} at: {filePath}");
        return;
      }

      if (HasLanguageVersionOption(filePath))
      {
        Debug.LogWarning($"csc.rsp already declares a language version at: {filePath}");
        return;
      }

      string content = File.ReadAllText(filePath);
      if (content.Length > 0 && !content.EndsWith("\n", StringComparison.Ordinal))
      {
        content += "\n";
      }

      File.WriteAllText(filePath, content + optionLine + "\n");
      AssetDatabase.Refresh();
      Debug.Log($"Added {optionLine} to csc.rsp at: {filePath}");
    }

    private static bool HasLanguageVersionOption(string responseFilePath)
    {
      return File.ReadAllLines(responseFilePath)
          .Select(line => line.Trim())
          .Any(line => line.Length > 0
                    && (line[0] == '-' || line[0] == '/')
                    && line.Substring(1).StartsWith(LanguageVersionOption, StringComparison.OrdinalIgnoreCase));
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
  }
}
