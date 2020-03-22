using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyEvent : MonoBehaviour
{
    public UnityEvent destroy;

    private void OnDestroy()
    {
        destroy.Invoke();
    }
}
