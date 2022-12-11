using System;
using System.Linq;
using CodeInUnity.Core.Utils;
using UnityEngine;
using CodeInUnity.Command;

namespace CodeInUnity.Commands
{
    [Serializable]
    public class MultiCommand : BaseCommand
    {
        [SerializeField]
        [HideInInspector]
        private BaseCommand[] commands;

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
