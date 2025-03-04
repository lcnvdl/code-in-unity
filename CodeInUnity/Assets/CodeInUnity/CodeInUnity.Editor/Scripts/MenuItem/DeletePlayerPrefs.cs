using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.Scripts
{
  public class DeletePlayerPrefs : MonoBehaviour
  {
    [MenuItem("Tools/Code in Unity/Delete PlayerPrefs")]
    public static void DoDeletePlayerPrefs()
    {
      PlayerPrefs.DeleteAll();
      Debug.Log("PlayerPrefs deleted");
    }
  }
}
