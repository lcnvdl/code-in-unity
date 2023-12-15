using UnityEngine;
using UnityEngine.Events;

public class OnEnableEvent : MonoBehaviour
{ 
    public UnityEvent onEnable;

    public UnityEvent onDisable;

    private void OnEnable()
    {
        onEnable.Invoke();
    }

    private void OnDisable()
    {
        onDisable.Invoke();
    }
}
