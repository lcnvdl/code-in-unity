using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Events
{
  public class DestroyEvent : MonoBehaviour
  {
    public UnityEvent destroy;

    private void OnDestroy()
    {
      destroy.Invoke();
    }
  }
}