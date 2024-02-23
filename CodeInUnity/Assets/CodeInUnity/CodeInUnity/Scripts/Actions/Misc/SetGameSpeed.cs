using UnityEngine;

namespace CodeInUnity.Scripts.Actions.Misc
{
  public class SetGameSpeed : ActionScript
  {
    public float speed = 1;

    protected override void Run()
    {
      Time.timeScale = speed;
    }
  }
}