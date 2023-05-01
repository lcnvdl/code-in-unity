using UnityEngine;

public class SetActive : ActionScript
{
    public GameObject target;

    public bool enable = true;

    protected override void Run()
    {
        GameObject realTarget = (target == null) ? gameObject : target;
        realTarget.SetActive(enable);
    }
}
