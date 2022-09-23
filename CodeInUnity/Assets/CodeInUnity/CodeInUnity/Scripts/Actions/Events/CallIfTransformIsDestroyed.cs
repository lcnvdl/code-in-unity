using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CallIfTransformIsDestroyed : MonoBehaviour
{
    public UnityEvent onDestroy;

    public Transform target;

    void Update () 
    {
        if (target == null)
        {
            onDestroy.Invoke();
        }
    }
}