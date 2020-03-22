using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInstance : ActionScript
{
    public GameObject prefab;
    public Vector3 position;
    public Vector3 rotation;
    public bool relative;

    public void CallAction()
    {
        ExecuteAction();
    }

    protected override void Run()
    {
        if (!relative)
        {
            GameObject.Instantiate(prefab, position, Quaternion.Euler(rotation));
        }
        else
        {
            GameObject.Instantiate(prefab, transform.position + position, Quaternion.Euler(transform.eulerAngles + rotation));
        }
    }
}
