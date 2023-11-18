using System;
using System.Collections.Generic;
using CodeInUnity.Core.Collections;
using UnityEngine;

namespace CodeInUnity.StateMachine
{
    [Serializable]
    public class StatesManagerBase
    {
        public string id;

        [SerializeField]
        [HideInInspector]
        [SerializeReference]
        private BaseState lastState;

        [SerializeField]
        [HideInInspector]
        [SerializeReference]
        private AnyState rootState;

        [SerializeReference]
        public BaseState currentState;

        public GameObject target;

        public GameObject statesRepositoryReference;

        [SerializeField]
        [HideInInspector]
        private List<string> newTriggers;

        [SerializeField]
        [HideInInspector]
        private List<string> triggers;

        public SerializableDictionary<string, float> variables;

        public string initialState;

        public List<string> Triggers => this.triggers;

        public virtual IStatesRepository StatesRepository
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

        public StatesManagerBase()
        {
            this.id = Guid.NewGuid().ToString();
        }

        public virtual void Initialize()
        {
            this.variables = new SerializableDictionary<string, float>();
            this.triggers = new List<string>();
            this.newTriggers = new List<string>();

            if (!string.IsNullOrEmpty(this.initialState))
            {
                this.currentState = this.StatesRepository?.GetState(this.initialState);
            }

            this.rootState = this.StatesRepository?.GetRootState();
        }

        public virtual void Update(float deltaTime)
        {
            if (lastState != currentState)
            {
                this.SwitchState(lastState, currentState);
                lastState = currentState;
            }
            else if (currentState != null)
            {
                rootState?.BeforeUpdateState(this, deltaTime);

                if (currentState != lastState)
                {
                    this.ClearTriggers();
                    return;
                }

                currentState.BeforeUpdateState(this, deltaTime);

                if (currentState != lastState)
                {
                    this.ClearTriggers();
                    return;
                }

                rootState?.UpdateState(this, deltaTime);
                currentState.UpdateState(this, deltaTime);
            }

            this.ClearTriggers();
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

            newState.ResetState();
            newState.EnterState(this);
        }

        public void Trigger(string triggerId)
        {
            //if (!this.triggers.Contains(triggerId))
            //{
            //    this.triggers.Add(triggerId);
            //}

            if (!this.newTriggers.Contains(triggerId))
            {
                this.newTriggers.Add(triggerId);
            }
        }

        private void ClearTriggers()
        {
            if (this.triggers.Count > 0)
            {
                this.triggers.Clear();
            }

            if (this.newTriggers.Count > 0)
            {
                this.triggers.AddRange(this.newTriggers);
                this.newTriggers.Clear();
            }
        }
    }
}