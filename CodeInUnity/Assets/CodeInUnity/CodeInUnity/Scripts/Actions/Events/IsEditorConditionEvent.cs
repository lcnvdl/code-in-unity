using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Events
{
  public class IsEditorConditionEvent : MonoBehaviour
  {
    public UnityEvent onTrue;

    public UnityEvent onFalse;

    public bool callOnAwake = false;

    private void Awake()
    {
      if (callOnAwake)
      {
        Call();
      }
    }

    private void Start()
    {
      if (!callOnAwake)
      {
        Call();
      }
    }

    private void Call()
    {
      if (Application.isEditor)
      {
        onTrue.Invoke();
      }
      else
      {
        onFalse.Invoke();
      }
    }
  }
}
