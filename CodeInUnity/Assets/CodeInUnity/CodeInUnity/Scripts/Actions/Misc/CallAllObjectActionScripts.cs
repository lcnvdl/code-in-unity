using System.Collections.Generic;
using UnityEngine;

namespace CodeInUnity.Scripts.Actions.Misc
{
  public class CallAllObjectActionScripts : ActionScript
  {
    public GameObject target;
    public bool recursive = false;

    protected override void Run()
    {
      if (target == gameObject)
      {
        Debug.LogWarning("Ignoring call to all object actions. Possible infinite cycle.");
        return;
      }

      List<ActionScript> list = new List<ActionScript>();

      if (recursive)
      {
        list.AddRange(target.GetComponentsInChildren<ActionScript>());
      }
      else
      {
        list.AddRange(target.GetComponents<ActionScript>());
      }

      list.ForEach(m => m.ExecuteAction());
    }
  }
}