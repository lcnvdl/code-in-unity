using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Events
{
  public class AlarmEvent : ActionScript
  {
    public float delay;

    public bool loop = false;

    public UnityEvent onTimeout;

    protected override void Run()
    {
      StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
      do
      {
        yield return new WaitForSeconds(delay);

        onTimeout.Invoke();
      }
      while (loop);
    }
  }
}