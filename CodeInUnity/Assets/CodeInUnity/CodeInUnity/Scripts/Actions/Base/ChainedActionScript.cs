using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions
{
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
}