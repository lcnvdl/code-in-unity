using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.Scripts
{
  public class SnapToGround : MonoBehaviour
  {
    [MenuItem("Tools/Code in Unity/Snap To Ground %g")]
    public static void Ground()
    {
      foreach (var transform in Selection.transforms)
      {
        var hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 10f);
        foreach (var hit in hits)
        {
          if (hit.collider.gameObject == transform.gameObject)
            continue;

          transform.position = hit.point;
          break;
        }
      }
    }
  }
}