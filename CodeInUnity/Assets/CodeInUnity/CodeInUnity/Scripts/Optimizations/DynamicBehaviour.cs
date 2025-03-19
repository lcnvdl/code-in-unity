using CodeInUnity.Interfaces;
using CodeInUnity.Scripts.Managers;
using UnityEngine;

namespace CodeInUnity.Scripts.Optimizations
{
  public abstract class DynamicBehaviour : MonoBehaviour, IDynamicBehaviour
  {
    protected virtual void OnEnable()
    {
      DynamicBehavioursManagerScript.Instance?.Register(this);
    }

    protected virtual void OnDisable()
    {
      if (DynamicBehavioursManagerScript.RawInstance != null)
      {
        DynamicBehavioursManagerScript.RawInstance.Unregister(this);
      }
    }

    public abstract void DynamicUpdate(float deltaTime);
  }
}
