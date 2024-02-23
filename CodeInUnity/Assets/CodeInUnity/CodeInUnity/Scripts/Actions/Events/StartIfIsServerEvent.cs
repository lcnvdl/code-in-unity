using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Events
{
  public class StartIfIsServerEvent : ActionScript
  {
    public UnityEvent onStart;

    protected override void Run()
    {
      //  TODO    Check if is server

      onStart.Invoke();
    }
  }
}