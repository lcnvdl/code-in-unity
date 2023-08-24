using UnityEngine;

namespace CodeInUnity.Core.System
{
  public interface IInjectorV2Instance
  {
    void Bind<T>(T instance);

    T GetAny<T>();

    T GetScript<T>() where T : MonoBehaviour;

    T LazyInitializeAny<T>(ref T value);

    T LazyInitializeScript<T>(ref T value) where T : MonoBehaviour;

    void Clear();
  }
}
