using System;

public class GCCollect : ActionScript
{
  protected override void Run()
  {
    GC.Collect();
  }
}
