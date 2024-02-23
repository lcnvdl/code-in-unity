using CodeInUnity.Core.Utils;
using UnityEngine;

namespace CodeInUnity.Scripts.Movement
{
  public class FaceCamera : MonoBehaviour
  {
    public UnityEngine.Camera cam;

    public Vector3Bool lockRotation = Vector3Bool.False;

    private void Start()
    {
      if (this.cam == null)
      {
        this.cam = UnityEngine.Camera.main;
      }
    }

    private void Update()
    {
      if (lockRotation.IsAllFalse)
      {
        transform.LookAt(cam.transform, Vector3.up);
      }
      else
      {
        var rotation = transform.rotation;

        transform.LookAt(cam.transform, Vector3.up);

        transform.rotation = Quaternion.Euler(
            lockRotation.x ? rotation.x : transform.rotation.x,
            lockRotation.y ? rotation.y : transform.rotation.y,
            lockRotation.z ? rotation.z : transform.rotation.z
        );
      }
    }
  }
}