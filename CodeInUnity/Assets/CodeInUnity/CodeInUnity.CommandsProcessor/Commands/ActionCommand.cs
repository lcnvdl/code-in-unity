using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class ActionCommand : BaseCommand
  {
    public Action action;

    public ActionCommand()
    {
    }

    public ActionCommand(Action action)
    {
      this.action = action;
    }

    protected override void Work(float dt, GameObject gameObject)
    {
      try
      {
        this.action();

        this.Finish();
      }
      catch (Exception e)
      {
        this.Cancel(e.Message);
      }
    }
  }
}
