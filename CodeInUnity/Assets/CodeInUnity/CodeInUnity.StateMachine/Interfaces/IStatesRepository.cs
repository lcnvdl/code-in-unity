using System.Collections;
using System.Collections.Generic;
using CodeInUnity.StateMachine;
using UnityEngine;

public interface IStatesRepository
{
    BaseState GetState(string id);
}
