using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameSpeed : ActionScript
{
    public float speed = 1;

    protected override void Run()
    {
        Time.timeScale = speed;
    }
}
