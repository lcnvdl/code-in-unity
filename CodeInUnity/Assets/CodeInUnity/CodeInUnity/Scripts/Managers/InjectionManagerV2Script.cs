using System;
using System.Collections.Generic;
using System.Linq;
using CodeInUnity.Core.Collections;
using CodeInUnity.Core.System;
using UnityEngine;

namespace CodeInUnity.Scripts.Managers
{
  public class InjectionManagerV2Script : MonoBehaviour, IInjectorV2Instance
  {
    private static bool instanceWasCreated = false;

    private Dictionary<Type, string> emptyArrayKeyCache = new Dictionary<Type, string>();

    private Dictionary<string, object> objects = new Dictionary<string, object>();

    private List<Type> nonSerializables = new List<Type>();

    #region OnlyForSerializationPurposes
    [SerializeField]
    [HideInInspector]
    private List<SerializableKeyPairStrings> bindingInterfaces = new List<SerializableKeyPairStrings>();

    [SerializeField]
    [HideInInspector]
    private List<SerializableKeyPairTransform> bindingTransforms = new List<SerializableKeyPairTransform>();
    #endregion

    private static IInjectorV2Instance instance;

    public static IInjectorV2Instance RawInstance => instance;

    public static IInjectorV2Instance Instance
    {
      get
      {
        if (instance == null)
        {
          instance = FindAnyObjectByType<InjectionManagerV2Script>();

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

      set
      {
        instance = value;
      }
    }

    void OnEnable()
    {
      //var ass = System.Reflection.Assembly.GetExecutingAssembly();
      List<string> keysToDelete = new List<string>();

      foreach (var kv in this.bindingTransforms)
      {
        var asInterface = this.bindingInterfaces.Find(m => m.key == kv.key);

        var allComponents = kv.transform.GetComponents<MonoBehaviour>();

        var component = allComponents.FirstOrDefault(m =>
          asInterface == null ?
          m.GetType().FullName == kv.key :
          m.GetType().FullName == asInterface.value);

        if (component == null)
        {
          keysToDelete.Add(kv.key);
          Debug.LogWarning($"Null component for key {kv.key}.");
          continue;
        }

        try
        {
          bool isActiveAndEnabled = component.isActiveAndEnabled;
        }
        catch (MissingReferenceException)
        {
          keysToDelete.Add(kv.key);
          Debug.LogWarning($"Null (destroyed) component for key {kv.key}.");
          continue;
        }

        objects[kv.key] = component;
      }

      foreach (var toDelete in keysToDelete)
      {
        objects.Remove(toDelete);
        this.bindingTransforms.RemoveAll(m => m.key == toDelete);
      }
    }

    void OnDisable()
    {
      this.bindingInterfaces.Clear();
      this.bindingTransforms.Clear();

      foreach (var kv in this.objects)
      {
        string interfaceKey = kv.Key;

        if (kv.Value is Component)
        {
          var component = (Component)kv.Value;
          if (component == null)
          {
            continue;
          }

          string implementationKey = kv.Value.GetType().Name;

          if (!implementationKey.Equals(interfaceKey, StringComparison.Ordinal))
          {
            bindingInterfaces.Add(new SerializableKeyPairStrings() { key = interfaceKey, value = implementationKey });
          }

          var serializable = new SerializableKeyPairTransform()
          {
            key = interfaceKey,
            transform = component.transform,
          };

          this.bindingTransforms.Add(serializable);

          // Debug.Log($"Transform binding {interfaceKey} saved");
        }
        else if (Application.isEditor && !this.nonSerializables.Contains(kv.Value.GetType()))
        {
          Debug.LogWarning($"Binding value of {interfaceKey} couldn't be serialized.");
        }
      }
    }

    public T[] GetEmptyArray<T>()
    {
      string key;
      Type t = typeof(T);

      if (!this.emptyArrayKeyCache.TryGetValue(t, out key))
      {
        key = $"$array_{t.FullName}";
        this.emptyArrayKeyCache[t] = key;
      }

      return this.GetEmptyArray<T>(key);
    }

    public T[] GetEmptyArray<T>(string key)
    {
      object val;

      if (!objects.TryGetValue(key, out val))
      {
        val = new T[0];
        objects[key] = val;
      }

      return (T[])val;
    }

    public void BindMany(object instance, Type[] types)
    {
      objects[instance.GetType().FullName] = instance;

      for (int i = 0; i < types.Length; i++)
      {
        objects[types[i].FullName] = instance;
      }
    }

    public void Bind<T>(T instance)
    {
      objects[instance.GetType().FullName] = instance;
      objects[typeof(T).FullName] = instance;
    }

    public void BindAsNonSerializable<T>(T instance)
    {
      this.Bind(instance);

      this.nonSerializables.Add(instance.GetType());
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
      string key = typeof(T).FullName;
      object val;

      bool hasValue = objects.TryGetValue(key, out val);
      bool tryingToInfer = false;

      if (hasValue)
      {
        try
        {
          bool isActiveAndEnabled = (val as MonoBehaviour).isActiveAndEnabled;
        }
        catch (Exception ex)
        {
          if ((ex is NullReferenceException) || (ex is MissingReferenceException))
          {
            Debug.LogWarning($"Null script for key {key}. Trying to infer...");
            tryingToInfer = true;
            hasValue = false;
            val = null;
          }
          else
          {
            throw;
          }
        }
      }

      if (!hasValue || val == null)
      {
        T result = FindAnyObjectByType<T>(FindObjectsInactive.Exclude);

        if (result == null)
        {
          result = FindAnyObjectByType<T>(FindObjectsInactive.Include);
        }

        if (result != null)
        {
          objects[typeof(T).FullName] = result;

          if (tryingToInfer)
          {
            Debug.LogWarning($"Infer SUCCESS for key {key}.");
          }
        }
        else
        {
          if (tryingToInfer)
          {
            Debug.LogWarning($"Infer FAILED for key {key}.");
          }
        }

        return result;
      }

      return (T)val;
    }

    public T GetAny<T>(ref T value) => LazyInitializeAny<T>(ref value);

    public T LazyInitializeAny<T>(ref T value)
    {
      if (value == null)
      {
        value = this.GetAny<T>();
      }

      return value;
    }

    public T GetScript<T>(ref T value) where T : MonoBehaviour
    {
      return LazyInitializeScript<T>(ref value);
    }

    public T LazyInitializeScript<T>(ref T value) where T : MonoBehaviour
    {
      if (value == null)
      {
        value = this.GetScript<T>();
      }

      return value;
    }

    public T LazyInitializeAnyOrScript<T, TScript>(ref T value) where TScript : MonoBehaviour, T
    {
      LazyInitializeAny(ref value);

      if (value == null)
      {
        return FindAnyObjectByType<TScript>(FindObjectsInactive.Exclude);
      }

      return value;
    }

    public void Clear()
    {
      objects.Clear();
    }
  }
}
