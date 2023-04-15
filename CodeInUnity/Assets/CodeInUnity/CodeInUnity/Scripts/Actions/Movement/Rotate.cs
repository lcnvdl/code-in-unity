using UnityEngine;

public class Rotate : ActionScript
{
    public Transform target;

    public float xSpeed = 0;
    public float ySpeed = 0;
    public float zSpeed = 0;

    [SerializeField]
    [HideInInspector]
    private bool isEnabled = false;

    protected override void Run()
    {
        isEnabled = true;
    }

    private void Update()
    {
        if (isEnabled)
        {
            (target ?? transform).Rotate(new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime);
        }
    }
}
