using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCollisionEvent : MonoBehaviour
{
    public ColliderEvent onTriggerEnter;

    public ColliderEvent onTriggerExit;

    private void OnTriggerExit(Collider other)
    {
        if (this.onTriggerExit != null)
        {
            this.onTriggerExit.Invoke(other);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.onTriggerEnter != null)
        {
            this.onTriggerEnter.Invoke(other);
        }
    }
}
