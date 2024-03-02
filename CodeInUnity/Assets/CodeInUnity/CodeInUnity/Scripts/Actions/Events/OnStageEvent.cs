using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Events
{
  public class OnStageEvent : MonoBehaviour
  {
    public UnityEvent onProduction;
    
    public UnityEvent onEditor;
    
    public UnityEvent onTest;

    private void Awake()
    {
#if UNITY_EDITOR
      bool isTest = Environment.StackTrace.Contains("UnityEngine.TestRunner");

      if (isTest)
      {
        this.onTest.Invoke();
      }
      else
      {
        this.onEditor.Invoke();
      }
#else
      this.onProduction.Invoke();
#endif
    }
  }
}
