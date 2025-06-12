using CodeInUnity.Core.Interfaces;
using CodeInUnity.Core.Utils;
using CodeInUnity.Scripts.Managers;
using UnityEngine;

namespace CodeInUnity.Scripts.Utils
{
  /// <summary>
  /// Singleton MonoBehaviour that provides a globally accessible IKeyIndexCache instance in the Unity scene.
  /// Provides a cache for storing and retrieving string values associated with a combination of a string key and an integer index.
  /// Useful for efficiently managing and reusing values that are identified by a key-index pair, with support for lazy value generation.
  /// </summary>
  public class KeyIndexCacheScript : MonoBehaviour, IKeyIndexCacheProvider
  {
    private static KeyIndexCacheScript instance;

    private IKeyIndexCache cache;

    public IKeyIndexCache Cache => this.cache ?? (this.cache = new KeyIndexCache());

    public static KeyIndexCacheScript Instance
    {
      get
      {
        if (instance == null)
        {
          instance = FindAnyObjectByType<KeyIndexCacheScript>();

          if (instance == null)
          {
            var go = new GameObject("KeyIndexCache");
            instance = go.AddComponent<KeyIndexCacheScript>();
          }
        }

        return instance;
      }
    }

    public static KeyIndexCacheScript RawInstance => instance;

    private void OnEnable()
    {
      instance = this;

      InjectionManagerV2Script.Instance.Bind<IKeyIndexCacheProvider>(this);
    }
  }
}
