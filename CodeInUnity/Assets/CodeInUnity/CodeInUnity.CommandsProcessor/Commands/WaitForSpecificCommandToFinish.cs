using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class WaitForSpecificCommandToFinish : BaseCommand
  {
    public BaseCommand otherCommand;

    public WaitForSpecificCommandToFinish()
    {
    }

    public WaitForSpecificCommandToFinish(BaseCommand otherCommand)
    {
      this.otherCommand = otherCommand;
    }

    protected override void Work(float dt, GameObject gameObject)
    {
      if (this.otherCommand == null)
      {
        this.Cancel("No command to wait for");
        return;
      }

      if (!this.otherCommand.IsInFinishedStatus)
      {
        return;
      }

      this.Finish();
    }
  }
}
