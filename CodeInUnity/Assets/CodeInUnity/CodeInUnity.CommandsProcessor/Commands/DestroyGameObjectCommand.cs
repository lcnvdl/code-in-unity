using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;

namespace CodeInUnity.Commands
{
  [Serializable]
  public class DestroyGameObjectCommand : BaseCommand
  {
    public GameObject targetObject;

    public float delay = 0f;

    protected override void Work(float dt, GameObject gameObject)
    {
      if (this.delay > 0f)
      {
        this.delay -= dt;
        return;
      }

      GameObject target = this.targetObject == null ? gameObject : this.targetObject;

      GameObject.Destroy(target);

      this.Finish();
    }
  }
}
