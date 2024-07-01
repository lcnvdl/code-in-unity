using System;
using System.Collections.Generic;
using CodeInUnity.Core.Collections;

namespace CodeInUnity.StateMachine.Interfaces
{
  public interface ITransitionState
  {
    bool IsEmpty { get; }

    bool Test(SerializableDictionary<string, float> variables, List<string> triggers);
  }
}
