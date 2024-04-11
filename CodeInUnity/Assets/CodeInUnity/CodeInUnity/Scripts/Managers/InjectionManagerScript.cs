using System.Collections.Generic;
using CodeInUnity.Core.System;
using UnityEngine;


namespace CodeInUnity.Scripts.Managers
{
  public class InjectionManagerScript : MonoBehaviour, IInjectorInstance
  {
    [HideInInspector]
    public Dictionary<string, object> objects = new Dictionary<string, object>();

    public T LazyInitialize<T>(ref T value) where T : MonoBehaviour
    {
      if (value == null)
      {
        value = this.GetScriptInstance<T>();
      }

      return value;
    }

    public X GetScriptInstance<X, T>() where T : MonoBehaviour
    {
      object val;

      if (!objects.TryGetValue(typeof(X).FullName, out val))
      {
        T result = FindAnyObjectByType<T>();

        if (result != null)
        {
          objects[typeof(T).FullName] = result;
          objects[typeof(X).FullName] = result;
        }

        return (X)(object)result;
      }

      return (X)val;
    }

    public T GetScriptInstance<T>() where T : MonoBehaviour
    {
      object val;

      if (!objects.TryGetValue(typeof(T).FullName, out val))
      {
        T result = FindAnyObjectByType<T>();

        if (result != null)
        {
          objects[typeof(T).FullName] = result;
        }

        return result;
      }

      return (T)val;
    }

    public void SetScriptInstance<T>(T instance) where T : MonoBehaviour
    {
      objects[instance.GetType().FullName] = instance;
    }

    public T GetInstance<T>()
    {
      object val;

      if (!objects.TryGetValue(typeof(T).FullName, out val))
      {
        return default(T);
      }

      return (T)val;
    }

    public void SetInstance<T>(T instance)
    {
      objects[instance.GetType().FullName] = instance;
    }

    public void Clear()
    {
      objects.Clear();
    }
  }
}