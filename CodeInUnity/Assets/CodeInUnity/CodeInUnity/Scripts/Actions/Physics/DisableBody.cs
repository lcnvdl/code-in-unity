using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBody : ActionScript
{
    public Transform target;

    protected override void Run()
    {
        foreach (var body in (target ?? transform).GetComponentsInChildren<Rigidbody>())
        {
            body.isKinematic = true;
            body.detectCollisions = false;
        }
    }
}
