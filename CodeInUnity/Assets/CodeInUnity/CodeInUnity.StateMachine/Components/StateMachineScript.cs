using UnityEngine;

namespace CodeInUnity.StateMachine.Components
{
  public class StateMachineScript : StateMachineManualScript
  {
    protected virtual void Update()
    {
      machine.Update(Time.deltaTime);
    }
  }
}