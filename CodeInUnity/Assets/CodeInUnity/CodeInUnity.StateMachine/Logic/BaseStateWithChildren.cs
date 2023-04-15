using System;
using UnityEngine;

namespace CodeInUnity.StateMachine
{
    [Serializable]
    public class BaseStateWithChildren : BaseState
    {
        [SerializeReference]
        public StatesManagerBase stateMachine;

        public override void UpdateState(StatesManagerBase manager, float deltaTime)
        {
            base.UpdateState(manager, deltaTime);

            if (this.stateMachine != null)
            {
                this.stateMachine.Update(deltaTime);
            }
        }
    }
}
