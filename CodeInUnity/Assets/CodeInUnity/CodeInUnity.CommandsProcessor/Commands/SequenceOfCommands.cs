using System;
using CodeInUnity.CommandsProcessor;
using UnityEngine;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class SequenceOfCommands : BaseCommand
  {
    [SerializeField]
    [SerializeReference]
    [HideInInspector]
    private BaseCommand[] commands;

    [SerializeField]
    [SerializeReference]
    [HideInInspector]
    private int currentIndex = 0;

    public SequenceOfCommands()
    {
    }

    public SequenceOfCommands(params BaseCommand[] commands)
    {
      this.commands = commands;
    }

    public override float Progress => this.currentIndex / (float)this.commands.Length;

    protected override void Work(float dt, GameObject gameObject)
    {
      if (this.currentIndex >= this.commands.Length)
      {
        this.Finish();
        return;
      }

      var cmd = this.commands[this.currentIndex];

      if (!cmd.IsInFinishedStatus && !cmd.IsCancelled)
      {
        cmd.Step(dt, gameObject);
      }

      if (cmd.IsInFinishedStatus || cmd.IsCancelled)
      {
        this.currentIndex++;
      }
    }
  }
}
