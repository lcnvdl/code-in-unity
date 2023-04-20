using UnityEngine;

public class SetAnimatorBoolValue : ActionScript
{
    public Animator target;

    public string variableName;

    public bool targetValue;

    public void RunWithNameAndValue(string overrideName, bool overrideValue)
    {
        this.variableName = overrideName;
        this.targetValue = overrideValue;

        this.ExecuteAction();
    }

    public void RunWithValue(bool overrideValue)
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
            target.SetBool(variableName, targetValue);
        }
    }
}
