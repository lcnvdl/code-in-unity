using System;
using System.Linq;
using CodeInUnity.CommandsProcessor;
using CodeInUnity.Core.Utils;
using UnityEngine;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class MultiCommand : BaseCommand
  {
    [SerializeField]
    [SerializeReference]
    [HideInInspector]
    private BaseCommand[] commands;

    public MultiCommand()
    {
    }

    public MultiCommand(params BaseCommand[] commands)
    {
      this.commands = commands;
    }

    public override float Progress => MathUtils.Media(this.commands.Select(m => m.Progress).ToArray());

    protected override void Work(float dt, GameObject gameObject)
    {
      foreach (var command in this.commands.Where(m => !m.IsInFinishedStatus && !m.IsPaused))
      {
        command.Step(dt, gameObject);
      }

      if (this.commands.All(m => m.IsInFinishedStatus))
      {
        if (this.commands.Any(m => m.IsCancelled))
        {
          this.Cancel();
        }
        else
        {
          this.Finish();
        }
      }
    }
  }
}
