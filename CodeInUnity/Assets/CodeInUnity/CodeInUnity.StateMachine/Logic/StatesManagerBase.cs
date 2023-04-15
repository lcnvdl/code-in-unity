using System;
using System.Collections.Generic;
using CodeInUnity.Core.Collections;
using UnityEngine;

namespace CodeInUnity.StateMachine
{
    [Serializable]
    public class StatesManagerBase
    {
        [SerializeField]
        [HideInInspector]
        [SerializeReference]
        private BaseState lastState;

        [SerializeReference]
        public BaseState currentState;

        public GameObject target;

        public GameObject statesRepositoryReference;

        public SerializableDictionary<string, float> variables;

        public string initialState;

        public IStatesRepository StatesRepository
        {
            get
            {
                IStatesRepository repository = null;

                if (this.statesRepositoryReference != null)
                {
                    repository = this.statesRepositoryReference.GetComponent<IStatesRepository>();
                }

                return repository;
            }
        }

        public virtual void Initialize()
        {
            this.variables = new SerializableDictionary<string, float>();

            if (!string.IsNullOrEmpty(this.initialState))
            {
                this.currentState = this.StatesRepository?.GetState(this.initialState);
            }
        }

        public void Update(float deltaTime)
        {
            if (lastState != currentState)
            {
                this.SwitchState(lastState, currentState);
                lastState = currentState;
            }
            else if (currentState != null)
            {
                currentState.BeforeUpdateState(this, deltaTime);

                if (currentState != lastState)
                {
                    return;
                }

                currentState.UpdateState(this, deltaTime);
            }
        }

        public void SwitchState(BaseState state)
        {
            currentState = state;
        }

        protected virtual void SwitchState(BaseState lastState, BaseState newState)
        {
            if (lastState != null)
            {
                lastState.ExitState(this);
            }

            newState.EnterState(this);
        }
    }
}