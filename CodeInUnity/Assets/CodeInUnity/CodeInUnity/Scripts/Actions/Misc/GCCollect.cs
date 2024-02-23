using System;

namespace CodeInUnity.Scripts.Actions.Misc
{
  public class GCCollect : ActionScript
  {
    protected override void Run()
    {
      GC.Collect();
    }
  }
}