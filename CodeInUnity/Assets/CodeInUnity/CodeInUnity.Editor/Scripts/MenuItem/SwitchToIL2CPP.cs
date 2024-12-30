using UnityEditor;
using UnityEngine;

public class SwitchToIL2CPP : MonoBehaviour
{
  [MenuItem("Tools/Code in Unity/Build/Switch to IL2CPP")]
  public static void Run()
  {
    var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
    PlayerSettings.SetScriptingBackend(targetGroup, ScriptingImplementation.IL2CPP);
  }
}
