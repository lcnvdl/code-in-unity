using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCollisionEvent : MonoBehaviour
{
    public string objectNameTest;

    public string tagTest;

    public ColliderEvent onTriggerEnter;

    public ColliderEvent onTriggerExit;

    private void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(objectNameTest) && other.gameObject.name != objectNameTest)
        {
            return;
        }

        if (!string.IsNullOrEmpty(tagTest) && !other.CompareTag(tagTest))
        {
            return;
        }
        
        if (this.onTriggerExit != null)
        {
            this.onTriggerExit.Invoke(other);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(objectNameTest) && other.gameObject.name != objectNameTest)
        {
            return;
        }

        if (!string.IsNullOrEmpty(tagTest) && !other.CompareTag(tagTest))
        {
            return;
        }

        if (this.onTriggerEnter != null)
        {
            this.onTriggerEnter.Invoke(other);
        }
    }
}
