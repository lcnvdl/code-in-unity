using CodeInUnity.StateMachine;
using CodeInUnity.StateMachine.Interfaces;
using UnityEngine;

public class StateMachineScript : StateMachineManualScript
{
    protected virtual void Update()
    {
        machine.Update(Time.deltaTime);
    }
}
