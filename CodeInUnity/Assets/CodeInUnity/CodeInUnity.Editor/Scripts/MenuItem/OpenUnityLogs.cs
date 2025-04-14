using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class OpenUnityLogs : MonoBehaviour
{
  [MenuItem("Tools/Code in Unity/Open Logs Folder")]
  public static void OpenLogsFolder()
  {
    string folder;
    if (Application.platform == RuntimePlatform.LinuxEditor)
    {
      string homePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      folder = Path.Combine(homePath, ".config", "unity3d", PlayerSettings.companyName, PlayerSettings.productName);
    }
    else
    {
      string localLowPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");
      folder = Path.Combine(localLowPath, PlayerSettings.companyName, PlayerSettings.productName);
    }

    UnityEngine.Debug.Log($"Opening folder: {folder}...");

    Process.Start(folder);
  }
}
