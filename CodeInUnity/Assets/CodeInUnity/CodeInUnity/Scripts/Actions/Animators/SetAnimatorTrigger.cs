using UnityEngine;

public class SetAnimatorTrigger : ActionScript
{
    public Animator target;

    public string variableName;

    public string targetValue;

    public void RunWithNameAndValue(string overrideName, string overrideValue)
    {
        this.variableName = overrideName;
        this.targetValue = overrideValue;

        this.ExecuteAction();
    }

    public void RunWithValue(string overrideValue)
    {
        this.targetValue = overrideValue;
        this.ExecuteAction();
    }

    protected override void Run()
    {
        if (target == null)
        {
            target = GetComponent<Animator>();
        }

        if (target != null)
        {
            target.SetTrigger(variableName);
        }
    }
}
