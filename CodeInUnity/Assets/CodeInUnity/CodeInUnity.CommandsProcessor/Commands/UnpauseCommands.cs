using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class UnpauseCommands : BaseCommand
  {
    [SerializeReference]
    public BaseCommand[] commands;

    public UnpauseCommands()
    {
    }

    public UnpauseCommands(params BaseCommand[] commands)
    {
      this.commands = commands;
    }

    protected override void Work(float dt, GameObject gameObject)
    {
      foreach (var cmd in this.commands)
      {
        cmd.Unpause();
      }

      this.Finish();
    }
  }
}
