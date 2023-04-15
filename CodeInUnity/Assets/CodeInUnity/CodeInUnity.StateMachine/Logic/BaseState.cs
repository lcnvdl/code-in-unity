using System;
using System.Collections.Generic;

namespace CodeInUnity.StateMachine
{
    [Serializable]
    public class BaseState
    {
        public string identifier;

        public StateTransition[] transitions;

        protected virtual string Identifier => "Unknown";

        //  Sirve para que cambie cuando una transición está en true.
        protected bool isTaskFinished = false;

        public BaseState()
        {
            this.identifier = this.Identifier;
        }

        public BaseState As(string alias)
        {
            this.identifier = alias;
            return this;
        }

        public virtual void EnterState(StatesManagerBase manager)
        {
        }

        public virtual void ExitState(StatesManagerBase manager)
        {
        }

        public virtual void BeforeUpdateState(StatesManagerBase manager, float deltaTime)
        {
            if (this.transitions != null && this.transitions.Length > 0)
            {
                for (int i = 0; i < transitions.Length; i++)
                {
                    if (transitions[i].IsEmpty && this.isTaskFinished)
                    {
                        var newState = manager.StatesRepository?.GetState(transitions[i].toState);
                        manager.SwitchState(newState);
                    }
                    else if (transitions[i].interruptState || this.isTaskFinished)
                    {
                        bool result = transitions[i].Test(manager.variables);
                        if (result)
                        {
                            var newState = manager.StatesRepository?.GetState(transitions[i].toState);
                            if (newState != null)
                            {
                                manager.SwitchState(newState);
                            }
                        }
                    }
                }
            }
        }

        public virtual void UpdateState(StatesManagerBase manager, float deltaTime)
        {
        }
    }
}
