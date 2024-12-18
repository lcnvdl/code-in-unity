using CodeInUnity.Core.Interfaces;
using CodeInUnity.Core.Utils;
using CodeInUnity.Scripts.Managers;
using UnityEngine;

namespace CodeInUnity.Scripts.Utils
{
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
