using System;

namespace CodeInUnity.StateMachine
{
    [Serializable]
    public sealed class AnyState : BaseState
    {
        protected override string Identifier => "Any";

        public override void BeforeUpdateState(StatesManagerBase manager, float deltaTime)
        {
            this.isTaskFinished = true;
            base.BeforeUpdateState(manager, deltaTime);
        }
    }
}
