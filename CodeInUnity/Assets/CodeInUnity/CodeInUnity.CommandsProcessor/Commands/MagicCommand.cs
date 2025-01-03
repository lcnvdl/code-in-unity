﻿using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;

namespace CodeInUnity.Commands
{
  public class MagicCommand : BaseCommand
  {
    public float currentProgress = 0;

    public Action<MagicCommand, float, GameObject> action;

    public override float Progress => currentProgress;

    public MagicCommand()
    {
    }

    public MagicCommand(Action<MagicCommand, float, GameObject> action)
    {
      this.action = action;
    }

    protected override void Work(float dt, GameObject gameObject)
    {
      this.action?.Invoke(this, dt, gameObject);
    }

    public void FinishCommand()
    {
      this.Finish();
    }
  }
}
