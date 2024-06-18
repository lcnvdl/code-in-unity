using UnityEngine;

namespace CodeInUnity.Scripts.Cameras
{
  public class CameraLookAtTarget : MonoBehaviour
  {
    public Transform target;

    public float speed = 0;

    private void Update()
    {
      var targetRotation = Quaternion.LookRotation(target.position - transform.position);

      transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
  }
}