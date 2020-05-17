using UnityEngine;

public class CallActionScripts : ActionScript
{
    public ActionScript[] targetActions;

    protected override void Run()
    {
        foreach (var action in targetActions)
        {
            action.ExecuteAction();
        }
    }
}
