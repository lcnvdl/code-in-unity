using UnityEngine;
using UnityEngine.Pool;

namespace CodeInUnity.Scripts.Optimizations.UnityPool
{
  public class TransformPoolItem : MonoBehaviour
  {
    public IObjectPool<Transform> pool;

    private void OnDisable()
    {
      this.pool.Release(this.transform);
    }
  }
}
