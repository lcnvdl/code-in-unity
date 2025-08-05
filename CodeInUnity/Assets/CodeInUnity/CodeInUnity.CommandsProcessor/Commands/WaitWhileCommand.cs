using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class WaitWhileCommand : BaseCommand
  {
    public Func<bool> condition;

    public WaitWhileCommand()
    {
    }

    public WaitWhileCommand(Func<bool> condition)
    {
      this.condition = condition;
    }

    protected override void Work(float dt, GameObject gameObject)
    {
      if (this.condition())
      {
        return;
      }

      this.Finish();
    }
  }
}
