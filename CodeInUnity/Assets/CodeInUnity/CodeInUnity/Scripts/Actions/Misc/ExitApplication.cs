using UnityEngine;

namespace CodeInUnity.Scripts.Actions.Misc
{
  public class ExitApplication : ActionScript
  {
    protected override void Run()
    {
      Application.Quit();
    }
  }
}