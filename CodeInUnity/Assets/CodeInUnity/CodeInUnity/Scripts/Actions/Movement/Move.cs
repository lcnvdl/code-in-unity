using CodeInUnity.Core.Utils;
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
            var realTarget = ArrayUtils.PickFirstNotNull(target, transform);
            realTarget.position += speed * Time.deltaTime;
        }
    }
}
