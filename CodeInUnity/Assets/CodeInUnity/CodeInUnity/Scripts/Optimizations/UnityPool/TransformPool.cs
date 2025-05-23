using UnityEngine;
using UnityEngine.Pool;

namespace CodeInUnity.Scripts.Optimizations.UnityPool
{
  public class TransformPool : MonoBehaviour
  {
    [SerializeField]
    private bool collectionChecks = true;

    [SerializeField]
    private int defaultPoolSize = 50;

    [SerializeField]
    private int maxPoolSize = 500;

    [SerializeField]
    private GameObject prefab;

    private IObjectPool<Transform> pool;

    public IObjectPool<Transform> Pool => this.pool;

    private void OnEnable()
    {
      if (this.pool == null)
      {
        this.pool = new ObjectPool<Transform>(
          CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, this.collectionChecks, this.defaultPoolSize, this.maxPoolSize);
      }
    }

    private void OnDisable()
    {
      this.pool?.Clear();
    }

    private Transform CreatePooledItem()
    {
      var instance = Instantiate(this.prefab, Vector3.zero, Quaternion.identity);
      instance.SetActive(false);

      // Add pool item script

      TransformPoolItem poolItem;

      if (!instance.TryGetComponent(out poolItem))
      {
        poolItem = instance.AddComponent<TransformPoolItem>();
      }

      poolItem.pool = this.pool;

      return instance.transform;
    }

    private void OnReturnedToPool(Transform target)
    {
      if (target.gameObject.activeSelf)
      {
        target.gameObject.SetActive(false);
      }
    }

    private void OnTakeFromPool(Transform target)
    {
      target.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(Transform target)
    {
      Destroy(target.gameObject);
    }
  }
}
