using System;
using System.Collections.Generic;
using CodeInUnity.Core.Collections;
using CodeInUnity.StateMachine.Interfaces;

namespace CodeInUnity.StateMachine
{
  public delegate bool StateDynamicTestFunction(SerializableDictionary<string, float> variables, List<string> triggers);

  [Serializable]
  public class StateDynamicTransition : ITransitionState
  {
    public string toState;

    public bool interruptState;

    public StateDynamicTestFunction testFunction;

    public bool IsEmpty => this.testFunction == null;

    public bool InterruptState => this.interruptState;

    public string ToState => this.toState;

    public StateDynamicTransition(string toState, StateDynamicTestFunction testFunction = null, bool interruptState = false)
    {
      this.toState = toState;
      this.interruptState = interruptState;
    }

    public bool Test(SerializableDictionary<string, float> variables, List<string> triggers)
    {
      if (this.IsEmpty)
      {
        return true;
      }

      return this.testFunction(variables, triggers);
    }
  }
}
