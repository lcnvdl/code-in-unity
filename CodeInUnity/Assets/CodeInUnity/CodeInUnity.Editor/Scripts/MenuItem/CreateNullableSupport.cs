using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.Scripts
{
  public static class CreateNullableSupport
  {
    [MenuItem("Assets/Create/Nullable Support", priority = 100)]
    public static void CreateCscRsp()
    {
      string folderPath = GetSelectedFolderPath();
      string filePath = Path.Combine(folderPath, "csc.rsp");

      if (File.Exists(filePath))
      {
        Debug.LogWarning($"csc.rsp already exists at: {filePath}");
        return;
      }

      File.WriteAllText(filePath, "-nullable:enable\n");
      AssetDatabase.Refresh();
      Debug.Log($"Created csc.rsp at: {filePath}");
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
