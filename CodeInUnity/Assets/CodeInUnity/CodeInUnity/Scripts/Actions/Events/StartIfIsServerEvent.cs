using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartIfIsServerEvent : ActionScript
{
    public float delay;

    public bool loop = false;

    public UnityEvent onStart;

    protected override void Run()
    {
        //  TODO    Check if is server

        onStart.Invoke();
    }
}
