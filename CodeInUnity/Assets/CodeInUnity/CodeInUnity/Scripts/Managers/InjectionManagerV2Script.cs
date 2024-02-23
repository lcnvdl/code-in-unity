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

    foreach (var bt in bindingTransforms)
    {
      //Type t = ass.GetType(bt.key);

      //if (t == null)
      //{
      //Debug.LogWarning($"Optimization throuble: Null type for key {bt.key}.");
      //}

      var asInterface = this.bindingInterfaces.Find(m => m.key == bt.key);

      var allComponents = bt.transform.GetComponents<MonoBehaviour>();

      var component = allComponents.FirstOrDefault(m => asInterface == null ? m.GetType().FullName == bt.key : m.GetType().FullName == asInterface.value);
      if (component == null)
      {
        Debug.LogWarning($"Null component for key {bt.key}.");
        continue;
      }

      objects[bt.key] = component;
    }
  }

  void OnDisable()
  {
    this.bindingInterfaces.Clear();
    this.bindingTransforms.Clear();

    foreach (var kv in this.objects)
    {
      if (kv.Value is Component)
      {
        var component = (Component)kv.Value;
        if (component == null)
        {
          continue;
        }

        if (kv.Value.GetType().Name != kv.Key)
        {
          bindingInterfaces.Add(new SerializableKeyPairStrings() { key = kv.Key, value = kv.Value.GetType().Name });
        }

        var serializable = new SerializableKeyPairTransform()
        {
          key = kv.Key,
          transform = component.transform,
        };

        this.bindingTransforms.Add(serializable);

        // Debug.Log($"Transform binding {kv.Key} saved");
      }
      else
      {
        Debug.LogWarning("Binding value of " + kv.Key + " couldn't be serialized.");
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
    object val;

    if (!objects.TryGetValue(typeof(T).FullName, out val))
    {
      T result = FindAnyObjectByType<T>(FindObjectsInactive.Exclude);

      if (result == null)
      {
        result = FindAnyObjectByType<T>(FindObjectsInactive.Include);
      }

      if (result != null)
      {
        objects[typeof(T).FullName] = result;
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
