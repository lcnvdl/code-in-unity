using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInstance : ActionScript
{
    public Transform target;

    protected override void Run()
    {
        if (target != null)
        {
            Destroy(target.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
