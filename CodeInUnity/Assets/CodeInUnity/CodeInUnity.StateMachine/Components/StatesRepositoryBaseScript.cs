using System;
using System.Collections.Generic;
using CodeInUnity.StateMachine.Interfaces;
using UnityEngine;

namespace CodeInUnity.StateMachine.Components
{
  public abstract class StatesRepositoryBaseScript : MonoBehaviour, IStatesRepository
  {
    [HideInInspector]
    [SerializeField]
    [SerializeReference]
    private List<BaseState> baseStates;

    void Awake()
    {
      this.baseStates = this.GenerateStates();
    }

    public BaseState GetState(string id)
    {
      int count = this.baseStates.Count;

      for (int i = 0; i < count; i++)
      {
        var state = this.baseStates[i];
        if (!string.IsNullOrEmpty(state.identifier) && state.identifier.Equals(id, StringComparison.OrdinalIgnoreCase))
        {
          return state;
        }
      }

      return null;
    }

    public T GetState<T>() where T : BaseState
    {
      int count = this.baseStates.Count;

      for (int i = 0; i < count; i++)
      {
        var baseState = this.baseStates[i];
        if (!string.IsNullOrEmpty(baseState.identifier))
        {
          T m = baseState as T;
          if (m != null)
          {
            return m;
          }
        }
      }

      return null;
    }

    public virtual AnyState GetRootState()
    {
      return null;
    }

    protected abstract List<BaseState> GenerateStates();
  }
}