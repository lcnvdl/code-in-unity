using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class EnableObjectCommand : BaseCommand
  {
    public GameObject targetObject;

    public float delay = 0f;

    public bool enableTargetObject = true;

    protected override void Work(float dt, GameObject gameObject)
    {
      if (this.delay > 0f)
      {
        this.delay -= dt;
        return;
      }

      if (this.targetObject != null)
      {
        this.targetObject.SetActive(this.enableTargetObject);
      }

      this.Finish();
    }
  }
}
