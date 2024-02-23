using CodeInUnity.Core.Utils;
using UnityEngine;

namespace CodeInUnity.Scripts.Actions.Physics
{
  public class DisableBody : ActionScript
  {
    public Transform target;

    protected override void Run()
    {
      var bodies = ArrayUtils.PickFirstNotNull(target, transform).GetComponentsInChildren<Rigidbody>();
      foreach (var body in bodies)
      {
        body.isKinematic = true;
        body.detectCollisions = false;
      }
    }
  }
}