using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.Scripts
{
  public static class CreateWorkspaceDefaultFolders
  {
    private static readonly string[] DefaultFolders = { "Scripts", "Entities", "Interfaces", "Enums", "Logic", "Scriptables" };

    [MenuItem("Assets/Create/Workspace/Default Folders", priority = 200)]
    public static void CreateFolders()
    {
      string folderPath = GetSelectedFolderPath();

      foreach (string folder in DefaultFolders)
      {
        string fullPath = Path.Combine(folderPath, folder);
        if (!Directory.Exists(fullPath))
          Directory.CreateDirectory(fullPath);
      }

      AssetDatabase.Refresh();
      Debug.Log($"Created default workspace folders at: {folderPath}");
    }

    private static string GetSelectedFolderPath()
    {
      if (Selection.activeObject == null)
        return "Assets";

      string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
      return Directory.Exists(assetPath) ? assetPath : Path.GetDirectoryName(assetPath);
    }
  }
}
