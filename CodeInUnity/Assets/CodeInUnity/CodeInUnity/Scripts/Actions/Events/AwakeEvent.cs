using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Events
{
  public class AwakeEvent : MonoBehaviour
  {
    public UnityEvent onAwake;

    private void Awake()
    {
      onAwake.Invoke();
    }
  }
}