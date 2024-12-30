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
    string localLowPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");
    string folder = Path.Combine(localLowPath, PlayerSettings.companyName, PlayerSettings.productName);
    Process.Start(folder);
  }
}
