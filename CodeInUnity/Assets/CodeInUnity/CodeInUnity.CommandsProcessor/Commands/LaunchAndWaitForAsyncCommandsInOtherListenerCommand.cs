using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;
using System.Collections.Generic;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class LaunchAndWaitForAsyncCommandsInOtherListenerCommand : BaseCommand
  {
    public CommandsListener listener;

    [SerializeReference]
    public List<BaseCommand> subcommands = new List<BaseCommand>();

    public LaunchAndWaitForAsyncCommandsInOtherListenerCommand()
    {
    }

    public LaunchAndWaitForAsyncCommandsInOtherListenerCommand(CommandsListener listener)
    {
      this.listener = listener;
    }

    public LaunchAndWaitForAsyncCommandsInOtherListenerCommand(CommandsListener listener, params BaseCommand[] commands)
    {
      this.listener = listener;
      this.subcommands.AddRange(commands);
    }

    public override void Start(GameObject gameObject)
    {
      base.Start(gameObject);

      foreach (var cmd in subcommands)
      {
        cmd.isAsync = true;
        this.listener.AddCommand(cmd);
      }
    }

    protected override void Work(float dt, GameObject gameObject)
    {
      foreach (var cmd in subcommands)
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
