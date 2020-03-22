using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : ActionScript
{
    public Transform target;

    public Vector3 speed;

    [HideInInspector]
    [SerializeField]
    private bool isEnabled = false;

    protected override void Run()
    {
        isEnabled = true;
    }

    private void Update()
    {
        if (isEnabled)
        {
            (target ?? transform).position += speed * Time.deltaTime;
        }
    }
}
