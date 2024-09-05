using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class UnpauseCommand : BaseCommand
  {
    public Guid commandId;

    public string commandInternalId;

    public CommandsListener listener;

    public UnpauseCommand()
    {
    }

    public UnpauseCommand(CommandsListener listener, Guid id)
    {
      this.commandId = id;
      this.listener = listener;
    }

    public UnpauseCommand(CommandsListener listener, string internalId)
    {
      this.commandInternalId = internalId;
      this.listener = listener;
    }

    protected override void Work(float dt, GameObject gameObject)
    {
      BaseCommand cmd;

      if (!string.IsNullOrEmpty(this.commandInternalId))
      {
        cmd = this.listener.GetCommandByInternalId(this.commandInternalId);
      }
      else
      {
        cmd = this.listener.GetCommandById(this.commandId);
      }

      if (cmd != null)
      {
        cmd.Unpause();
      }

      this.Finish();
    }
  }
}
