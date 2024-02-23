using UnityEngine;

namespace CodeInUnity.Scripts.Actions.GameObjects
{
  public class DestroyInstance : ActionScript
  {
    public Transform target;

    protected override void Run()
    {
      if (target != null)
      {
        Destroy(target.gameObject);
      }
      else
      {
        Destroy(gameObject);
      }
    }
  }
}