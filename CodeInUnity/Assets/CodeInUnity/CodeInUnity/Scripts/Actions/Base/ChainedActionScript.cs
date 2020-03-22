using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ChainedActionScript : ActionScript
{
    public UnityEvent chain;

    public override void ExecuteAction()
    {
        base.ExecuteAction();

        if (chain != null)
        {
            chain.Invoke();
        }
    }
}
