using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableIfEditor : MonoBehaviour
{
    public bool enable = true;

    public bool disable = false;

    public Transform[] positive;

    public Transform[] negative;

    private Transform[] InstancesToEnable
    {
        get
        {
#if UNITY_EDITOR
            return positive;
#else
            return negative;
#endif
        }
    }

    private Transform[] InstancesToDisable
    {
        get
        {
#if UNITY_EDITOR
            return negative;
#else
            return positive;
#endif
        }
    }

    void OnEnable()
    {
        foreach (var instance in InstancesToEnable)
        {
            if (enable)
            {
                instance.gameObject.SetActive(true);
            }
        }
        
        foreach (var instance in InstancesToDisable)
        {
            if (disable)
            {
                instance.gameObject.SetActive(false);
            }
        }
    }
}
