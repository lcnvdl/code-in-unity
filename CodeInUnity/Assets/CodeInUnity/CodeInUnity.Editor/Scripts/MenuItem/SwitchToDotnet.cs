using UnityEditor;
using UnityEngine;

public class SwitchToDotnet : MonoBehaviour
{
  [MenuItem("Tools/Code in Unity/Build/Switch to WinRTDotNET")]
  public static void Run()
  {
    var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
    PlayerSettings.SetScriptingBackend(targetGroup, ScriptingImplementation.WinRTDotNET);
  }
}
