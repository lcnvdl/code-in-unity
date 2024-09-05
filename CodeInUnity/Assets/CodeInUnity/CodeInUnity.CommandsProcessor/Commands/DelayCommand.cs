using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;

namespace CodeInUnity.Commands
{
    [Serializable]
    public class DelayCommand : BaseCommand
    {
        public override float Progress => ((this.totalDelay - this.timeLeft) / this.totalDelay);

        public float totalDelay;

        public float timeLeft;

        public DelayCommand()
        {
        }

        public DelayCommand(float delayInSeconds)
        {
            this.totalDelay = delayInSeconds;
            this.timeLeft = delayInSeconds;
        }

        protected override void Work(float dt, GameObject gameObject)
        {
            this.timeLeft = Mathf.Max(0, this.timeLeft - dt);
            if (this.timeLeft == 0)
            {
                this.Finish();
            }
        }
    }
}
