using UnityEditor;
using UnityEngine;

public class SwitchToMono : MonoBehaviour
{
  [MenuItem("Tools/Code in Unity/Build/Switch to Mono")]
  public static void Run()
  {
    var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
    PlayerSettings.SetScriptingBackend(targetGroup, ScriptingImplementation.Mono2x);
  }
}
