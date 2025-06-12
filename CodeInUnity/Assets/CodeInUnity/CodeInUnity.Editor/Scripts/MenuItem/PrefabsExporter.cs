using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrefabExporter
{
  [MenuItem("Tools/Code in Unity/Export All Prefabs")]
  public static void ExportAllPrefabs()
  {
    var selectedFolders = Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.Assets);
    if (selectedFolders.Length == 0)
    {
      Debug.LogWarning("Seleccioná al menos una carpeta en el Project para exportar.");
      return;
    }

    var sb = new StringBuilder();
    sb.AppendLine("Path;Name;Parent;Tag;Layer");


    // --- SCENE ---

    int sceneCount = SceneManager.sceneCount;
    for (int i = 0; i < sceneCount; i++)
    {
      var scene = SceneManager.GetSceneAt(i);
      if (!scene.isLoaded) continue;

      string sceneName = scene.name;
      GameObject[] roots = scene.GetRootGameObjects();
      foreach (var root in roots)
      {
        string path = $"Scene/{sceneName}/{root.name}";
        sb.AppendLine($"{Escape(path)};{Escape(root.name)};;{Escape(root.tag)};{LayerMask.LayerToName(root.layer)}");

        foreach (Transform child in root.transform)
        {
          string childPath = path + "/" + child.name;
          sb.AppendLine($"{Escape(childPath)};{Escape(child.name)};{Escape(root.name)};{Escape(child.tag)};{LayerMask.LayerToName(child.gameObject.layer)}");
        }
      }
    }


    //  --- PREFABS ---

    foreach (var folder in selectedFolders)
    {
      string folderPath = AssetDatabase.GetAssetPath(folder);
      string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });

      foreach (string guid in prefabGuids)
      {
        string prefabPath = AssetDatabase.GUIDToAssetPath(guid);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (prefab == null) continue;

        string parentName = prefab.name;
        string rootPath = prefabPath;

        sb.AppendLine($"{Escape(rootPath)};{Escape(parentName)};;{Escape(prefab.tag)};{LayerMask.LayerToName(prefab.layer)}");

        // Primer nivel de hijos
        foreach (Transform child in prefab.transform)
        {
          string childName = child.name;
          string childPath = rootPath + "/" + childName;
          sb.AppendLine($"{Escape(childPath)};{Escape(childName)};{Escape(parentName)};{Escape(child.tag)};{LayerMask.LayerToName(child.gameObject.layer)}");
        }
      }
    }

    string outputPath = "Assets/ExportedPrefabs.csv";
    File.WriteAllText(outputPath, sb.ToString());
    AssetDatabase.Refresh();
    Debug.Log("Exported to: " + outputPath);
  }

  private static string Escape(string text)
  {
    if (string.IsNullOrEmpty(text)) return "";
    if (text.Contains(";") || text.Contains("\""))
    {
      return $"\"{text.Replace("\"", "\"\"")}\"";
    }
    return text;
  }
}
