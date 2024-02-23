using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Misc
{
  public class ToggleGameSpeed : ActionScript
  {
    public List<float> speeds;
    public UnityEvent onChangeSpeed;

    protected override void Run()
    {
      if (speeds.Count == 0)
      {
        return;
      }

      int currentSpeed = speeds.FindIndex(m => Mathf.Approximately(m, Time.timeScale));

      Time.timeScale = speeds[(currentSpeed + 1) % speeds.Count];

      onChangeSpeed.Invoke();
    }
  }
}