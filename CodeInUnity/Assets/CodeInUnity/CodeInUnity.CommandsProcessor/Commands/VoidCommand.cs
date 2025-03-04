using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class VoidCommand : BaseCommand
  {
    protected override void Work(float dt, GameObject gameObject)
    {
      this.Finish();
    }
  }
}
