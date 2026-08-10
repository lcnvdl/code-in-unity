using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.Scripts
{
  public static class CreateLanguageVersionPostprocessor
  {
    private const string FileName = "LanguageVersionProjectPostprocessor.cs";

    [MenuItem("Assets/Create/Language Version Postprocessor", priority = 103)]
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
      return @"using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.Scripts
{
  public class LanguageVersionProjectPostprocessor : AssetPostprocessor
  {
    private const string LanguageVersionOption = ""langversion:"";

    private const string PredefinedAssemblyPrefix = ""Assembly-CSharp"";

    private static string OnGeneratedCSProject(string path, string content)
    {
      string languageVersion = GetLanguageVersionFromResponseFile(path);

      if (string.IsNullOrEmpty(languageVersion))
      {
        return content;
      }

      var document = XDocument.Parse(content);
      if (document.Root == null)
      {
        return content;
      }

      XNamespace ns = document.Root.Name.Namespace;
      var existingElement = document.Root.Descendants(ns + ""LangVersion"").FirstOrDefault();

      if (existingElement != null)
      {
        existingElement.Value = languageVersion;
      }
      else
      {
        var propertyGroup = document.Root.Element(ns + ""PropertyGroup"");
        if (propertyGroup == null)
        {
          return content;
        }

        propertyGroup.Add(new XElement(ns + ""LangVersion"", languageVersion));
      }

      Debug.Log($""[LanguageVersionPostprocessor] LangVersion {languageVersion} applied to {Path.GetFileName(path)}"");

      return document.ToString();
    }

    private static string GetLanguageVersionFromResponseFile(string csprojPath)
    {
      string assemblyName = Path.GetFileNameWithoutExtension(csprojPath);
      string responseFilePath = FindResponseFilePath(assemblyName);

      if (responseFilePath == null)
      {
        return null;
      }

      foreach (string line in File.ReadAllLines(responseFilePath))
      {
        string trimmedLine = line.Trim();

        if (trimmedLine.Length == 0 || (trimmedLine[0] != '-' && trimmedLine[0] != '/'))
        {
          continue;
        }

        string option = trimmedLine.Substring(1);
        if (option.StartsWith(LanguageVersionOption, StringComparison.OrdinalIgnoreCase))
        {
          return option.Substring(LanguageVersionOption.Length).Trim();
        }
      }

      return null;
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

      // Predefined assemblies (Assembly-CSharp, Assembly-CSharp-Editor, ...) read the response file at the Assets root.
      if (assemblyName.StartsWith(PredefinedAssemblyPrefix, StringComparison.Ordinal))
      {
        string rootResponseFilePath = Path.Combine(Path.GetFullPath(""Assets""), ""csc.rsp"");

        return File.Exists(rootResponseFilePath) ? rootResponseFilePath : null;
      }

      return null;
    }
  }
}
";
    }
  }
}
