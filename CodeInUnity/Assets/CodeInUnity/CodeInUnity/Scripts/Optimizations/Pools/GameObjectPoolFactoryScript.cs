using System.Collections.Generic;
using CodeInUnity.Interfaces;
using UnityEngine;

namespace CodeInUnity.Scripts.Optimizations.Pools
{
  public class GameObjectPoolFactoryScript : MonoBehaviour, IGameObjectFactory, IGameObjectPoolFactory
  {
    [SerializeField]
    private string entityName = "generic";

    [SerializeField]
    private int poolSize = 10;

    [SerializeField]
    [HideInInspector]
    private List<GameObject> activeInstances = new List<GameObject>();

    [SerializeField]
    [HideInInspector]
    private List<GameObject> pool = new List<GameObject>();

    public string EntityName => this.entityName;

    public GameObject GetNewInstance(GameObject prefab)
    {
      GameObject newInstance;

      if (this.pool.Count > 0)
      {
        newInstance = this.pool[0];
        newInstance.transform.SetParent(null);
        this.pool.RemoveAt(0);
      }
      else
      {
        newInstance = Instantiate(prefab);
      }

      DestroyGameObjectWrapperScript destroyWrapper;

      if (!newInstance.TryGetComponent(out destroyWrapper))
      {
        destroyWrapper = newInstance.AddComponent<DestroyGameObjectWrapperScript>();
      }

      destroyWrapper.poolContainer = gameObject;

      newInstance.SetActive(true);
      this.activeInstances.Add(newInstance);

      return newInstance;
    }

    public void DestroyInstance(GameObject instance)
    {
      if (instance == null)
      {
        return;
      }

      if (this.pool.Count < this.poolSize)
      {
        instance.SetActive(false);

        if (this.activeInstances.Contains(instance))
        {
          //  TODO    Ver si limpiar con el removeAll en otra parte
          this.activeInstances.RemoveAll(m => m == null || m == instance);
          this.pool.Add(instance);
          instance.transform.SetParent(transform);
        }
        else
        {
          Destroy(instance);
        }
      }
      else
      {
        Destroy(instance);
      }
    }

    public void Clear()
    {
      this.activeInstances.Clear();

      foreach (var instance in this.pool)
      {
        if (instance != null)
        {
          Destroy(instance);
        }
      }

      this.pool.Clear();
    }
  }
}