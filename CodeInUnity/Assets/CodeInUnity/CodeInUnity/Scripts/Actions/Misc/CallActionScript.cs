﻿namespace CodeInUnity.Scripts.Actions.Misc
{
  public class CallActionScript : ActionScript
  {
    public ActionScript targetAction;

    protected override void Run()
    {
      var action = (ActionScript)FindObjectOfType(targetAction.GetType());
      action.ExecuteAction();
    }
  }
}