using System;
using System.Collections.Generic;
using CodeInUnity.StateMachine;
using UnityEngine;

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
        var state = this.baseStates.Find(m => !string.IsNullOrEmpty(m.identifier) && m.identifier.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        return state;
    }

    public T GetState<T>() where T : BaseState
    {
        T state = this.baseStates.Find(m => !string.IsNullOrEmpty(m.identifier) && m is T) as T;
        return state;
    }

    public virtual AnyState GetRootState()
    {
        return null;
    }

    protected abstract List<BaseState> GenerateStates();
}