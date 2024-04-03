using System;
using CodeInUnity.StateMachine;
using UnityEngine;

[Serializable]
public abstract class StateMachineNoScript
{
  [SerializeReference]
  [HideInInspector]
  public StatesManagerBase machine;

  public void Initialize()
  {
    this.machine = this.GenerateNewMachine();
    this.machine.Initialize();
  }

  public void SetTrigger(string name)
  {
    this.machine.Trigger(name);
  }

  public void SetValue(string name, float value)
  {
    this.machine.variables[name] = value;
  }

  protected abstract StatesManagerBase GenerateNewMachine();
}
