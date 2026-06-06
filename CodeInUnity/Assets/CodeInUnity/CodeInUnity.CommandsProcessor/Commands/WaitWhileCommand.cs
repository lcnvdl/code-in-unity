using System;
using CodeInUnity.CommandsProcessor;
using UnityEngine;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class WaitWhileCommand : BaseCommand
  {
    public Func<bool> condition;
    public Action onSucceed;

    public WaitWhileCommand()
    {
    }

    public WaitWhileCommand(Func<bool> condition)
      : this(condition, null)
    {
    }

    public WaitWhileCommand(Func<bool> condition, Action onSucceed)
    {
      this.condition = condition;
      this.onSucceed = onSucceed;
    }

    protected override void Work(float dt, GameObject gameObject)
    {
      if (this.condition())
      {
        if (this.onSucceed != null)
        {
          this.onSucceed.Invoke();
        }

        return;
      }

      this.Finish();
    }
  }
}
