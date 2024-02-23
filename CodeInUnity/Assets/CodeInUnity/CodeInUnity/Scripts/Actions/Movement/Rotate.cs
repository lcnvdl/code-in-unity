using CodeInUnity.Core.Utils;
using UnityEngine;

namespace CodeInUnity.Scripts.Actions.Movement
{
  public class Rotate : ActionScript
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
        var realTarget = ArrayUtils.PickFirstNotNull(target, transform);
        realTarget.rotation = Quaternion.Euler(realTarget.rotation.eulerAngles + new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime);
      }
    }
  }
}