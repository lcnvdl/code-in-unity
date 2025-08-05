using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;
using System.Collections.Generic;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class WaitForAsyncCommandsToFinish : BaseCommand
  {
    public CommandsListener listener;

    [SerializeReference]
    public List<BaseCommand> subcommands = new List<BaseCommand>();

    public WaitForAsyncCommandsToFinish()
    {
    }

    public WaitForAsyncCommandsToFinish(CommandsListener listener)
    {
      this.listener = listener;
    }

    protected override void OnStart(GameObject gameObject)
    {
      foreach (var command in this.listener.commands)
      {
        if (command.isAsync)
        {
          this.subcommands.Add(command);
        }
      }
    }

    protected override void Work(float dt, GameObject gameObject)
    {
      foreach (var cmd in this.subcommands)
      {
        if (!cmd.IsInFinishedStatus)
        {
          return;
        }
      }

      this.Finish();
    }
  }
}
