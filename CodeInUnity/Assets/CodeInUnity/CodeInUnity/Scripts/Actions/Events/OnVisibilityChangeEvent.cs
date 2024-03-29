using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Events
{
  public class OnVisibilityChangeEvent : MonoBehaviour
  {
    public UnityEvent onVisible;

    public UnityEvent onInvisible;

    void OnBecameInvisible()
    {
      this.onInvisible?.Invoke();
    }

    void OnBecameVisible()
    {
      this.onVisible?.Invoke();
    }
  }
}