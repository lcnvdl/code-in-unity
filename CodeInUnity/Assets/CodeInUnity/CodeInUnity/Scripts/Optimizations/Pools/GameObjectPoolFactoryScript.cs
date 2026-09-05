using System.Collections.Generic;
using CodeInUnity.Interfaces;
using UnityEngine;

namespace CodeInUnity.Scripts.Optimizations.Pools
{
  public class GameObjectPoolFactoryScript : MonoBehaviour, IGameObjectFactory, IGameObjectPoolFactory
  {
    private static Dictionary<string, GameObjectPoolFactoryScript> poolRegistry = new();

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

    private void OnEnable()
    {
      poolRegistry[this.entityName] = this;
    }

    private void OnDisable()
    {
      poolRegistry.Remove(this.entityName);
    }

    public void ChangeName(string name)
    {
      poolRegistry.Remove(this.entityName);
      this.entityName = name;
      poolRegistry[this.entityName] = this;
    }

    public static IGameObjectPoolFactory GetPoolFactory(string key)
    {
      if (poolRegistry.TryGetValue(key, out var factory))
      {
        return factory;
      }

      return null;
    }

    public static IGameObjectPoolFactory GetOrCreatePoolFactory(string key)
    {
      if (poolRegistry.TryGetValue(key, out var factory))
      {
        return factory;
      }

      var go = new GameObject($"PoolFactory_{key}");
      var poolFactory = go.AddComponent<GameObjectPoolFactoryScript>();
      poolFactory.ChangeName(key);

      return poolFactory;
    }

    public GameObject GetNewInstance(GameObject prefab, Vector3 position, Quaternion rotation)
    {
      var instance = this.GetNewInstance(prefab);
      instance.transform.SetPositionAndRotation(position, rotation);
      return instance;
    }

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

      destroyWrapper.poolContainer = this.gameObject;

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
          for (int i = this.activeInstances.Count - 1; i >= 0; i--)
          {
            var instanceInList = this.activeInstances[i];
            if (instanceInList == instance || instanceInList == null)
            {
              this.activeInstances.RemoveAt(i);
            }
          }

          this.pool.Add(instance);
          instance.transform.SetParent(this.transform);
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

    public static void ResetFactory()
    {
      poolRegistry.Clear();
    }
  }
}