using System.Collections.Generic;
using CodeInUnity.Core.System;
using UnityEngine;

public class InjectionManagerV2Script : MonoBehaviour, IInjectorV2Instance
{
  private static bool instanceWasCreated = false;

  private Dictionary<string, object> objects = new Dictionary<string, object>();

  private static IInjectorV2Instance instance;

  public static IInjectorV2Instance Instance
  {
    get
    {
      if (instance == null)
      {
        instance = FindObjectOfType<InjectionManagerV2Script>();

        if (instance == null)
        {
          if (!instanceWasCreated)
          {
            GameObject go = new GameObject("InjectionManagerV2");
            instance = go.AddComponent<InjectionManagerV2Script>();
            DontDestroyOnLoad(go);
            instanceWasCreated = true;
          }
          else
          {
            Debug.LogError("InjectionManagerV2 instance was not found and could not be created.");
          }
        }
      }

      return instance;
    }
  }

  public void Bind<T>(T instance)
  {
    objects[instance.GetType().FullName] = instance;
    objects[typeof(T).FullName] = instance;
    }

  public T GetAny<T>()
  {
    object val;

    if (!objects.TryGetValue(typeof(T).FullName, out val))
    {
      return default(T);
    }

    return (T)val;
  }

  public T GetScript<T>() where T : MonoBehaviour
  {
    object val;

    if (!objects.TryGetValue(typeof(T).FullName, out val))
    {
      T result = FindObjectOfType<T>();

      if (result != null)
      {
        objects[typeof(T).FullName] = result;
      }

      return result;
    }

    return (T)val;
  }

  public T LazyInitializeAny<T>(ref T value)
  {
    if (value == null)
    {
      value = this.GetAny<T>();
    }

    return value;
  }

  public T LazyInitializeScript<T>(ref T value) where T : MonoBehaviour
  {
    if (value == null)
    {
      value = this.GetScript<T>();
    }

    return value;
  }

  public void Clear()
  {
    objects.Clear();
  }
}
