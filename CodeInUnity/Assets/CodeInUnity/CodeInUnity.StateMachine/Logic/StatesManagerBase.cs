using System;
using System.Collections.Generic;
using CodeInUnity.Core.Collections;
using CodeInUnity.StateMachine.Interfaces;
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
      if (this.lastState != this.currentState)
      {
        this.SwitchState(this.lastState, this.currentState);

        this.lastState = this.currentState;
      }

      if (this.currentState != null)
      {
        this.rootState?.BeforeUpdateState(this, deltaTime);

        if (this.currentState != this.lastState)
        {
          this.ClearTriggers();
          this.Update(deltaTime);
          return;
        }

        this.currentState.BeforeUpdateState(this, deltaTime);

        if (this.currentState != this.lastState)
        {
          this.ClearTriggers();
          this.Update(deltaTime);
          return;
        }

        this.rootState?.UpdateState(this, deltaTime);
        this.currentState.UpdateState(this, deltaTime);
      }

      this.ClearTriggers();
    }

    public void SwitchState(BaseState state)
    {
      this.currentState = state;
    }

    protected virtual void SwitchState(BaseState lastState, BaseState newState)
    {
      if (lastState != null)
      {
        lastState.ExitState(this);
      }

      if (newState != null)
      {
        newState.ResetState();
        newState.EnterState(this);
      }
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