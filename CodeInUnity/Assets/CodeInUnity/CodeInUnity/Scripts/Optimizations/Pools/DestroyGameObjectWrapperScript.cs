using CodeInUnity.Interfaces;
using UnityEngine;

namespace CodeInUnity.Scripts.Optimizations.Pools
{
  public class DestroyGameObjectWrapperScript : MonoBehaviour
  {
    [HideInInspector]
    public GameObject poolContainer;

    private IGameObjectPoolFactory pool;

    private IGameObjectPoolFactory Pool
    {
      get
      {
        if (this.pool == null)
        {
          this.pool = this.poolContainer.GetComponent<IGameObjectPoolFactory>();
        }

        return this.pool;
      }
    }

    public void DestroySelf()
    {
      var pool = this.Pool;
      if (pool == null)
      {
        Destroy(gameObject);
      }
      else
      {
        pool.DestroyInstance(gameObject);
      }
    }
  }
}