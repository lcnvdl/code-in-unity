using UnityEngine;

public class SetAnimatorIntValue : ActionScript
{
    public Animator target;

    public string variableName;

    public int targetValue;

    public void RunWithNameAndValue(string overrideName, int overrideValue)
    {
        this.variableName = overrideName;
        this.targetValue = overrideValue;

        this.ExecuteAction();
    }

    public void RunWithValue(int overrideValue)
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
            target.SetInteger(variableName, targetValue);
        }
    }
}
