using System;
using System.Collections.Generic;
using System.Linq;
using CodeInUnity.Core.Collections;
using CodeInUnity.Core.System;
using UnityEngine;

public class InjectionManagerV2Script : MonoBehaviour, IInjectorV2Instance
{
  private static bool instanceWasCreated = false;

  private Dictionary<string, object> objects = new Dictionary<string, object>();

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

        if (implementationKey != interfaceKey)
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
      else
      {
        Debug.LogWarning("Binding value of " + interfaceKey + " couldn't be serialized.");
      }
    }
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
      catch (MissingReferenceException)
      {
        Debug.LogWarning($"Null script for key {key}. Trying to infer...");
        tryingToInfer = true;
        hasValue = false;
        val = null;
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

  public void Clear()
  {
    objects.Clear();
  }
}
