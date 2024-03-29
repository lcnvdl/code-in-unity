﻿using UnityEngine;

namespace CodeInUnity.Scripts.Actions
{
  public abstract class ActionScript : MonoBehaviour
  {
    public bool callOnStart;

    protected virtual void Start()
    {
      if (callOnStart)
      {
        ExecuteAction();
      }
    }

    public virtual void ExecuteAction()
    {
      Run();
    }

    protected abstract void Run();
  }
}