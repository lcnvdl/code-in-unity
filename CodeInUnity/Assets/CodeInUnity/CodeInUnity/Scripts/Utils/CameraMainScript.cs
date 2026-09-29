using UnityEngine;

namespace CodeInUnity.Scripts.Utils
{
  public class CameraMainScript : MonoBehaviour
  {
    public static Camera main;

    private void OnEnable()
    {
      main = Camera.main;
    }

    private void Update()
    {
      main = Camera.main;
    }
  }
}