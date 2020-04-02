using UnityEngine;

public class SetActive : ActionScript
{
    public GameObject target;

    public bool enable = true;

    protected override void Run()
    {
        (target ?? gameObject).SetActive(enable);
    }
}
