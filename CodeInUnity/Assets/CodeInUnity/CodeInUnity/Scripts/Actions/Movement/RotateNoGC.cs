using UnityEngine;

namespace CodeInUnity.Scripts.Actions.Movement
{
  public class RotateNoGC : ActionScript
  {
    public Transform target;

    public float xSpeed = 0;
    public float ySpeed = 0;
    public float zSpeed = 0;

    [SerializeField]
    [HideInInspector]
    private bool isEnabled = false;

    protected override void Run()
    {
      isEnabled = true;
    }

    private void Update()
    {
      if (isEnabled)
      {
        if (target == null)
        {
          target = transform;
        }

        target.rotation = Quaternion.Euler(target.rotation.eulerAngles + new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime);
      }
    }
  }
}