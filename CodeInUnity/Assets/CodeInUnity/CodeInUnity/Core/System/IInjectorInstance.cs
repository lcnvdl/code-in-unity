using UnityEngine;

namespace CodeInUnity.Core.System
{
    public interface IInjectorInstance
    {
        T LazyInitialize<T>(ref T value) where T : MonoBehaviour;

        T GetScriptInstance<T>() where T : MonoBehaviour;

        X GetScriptInstance<X, T>() where T : MonoBehaviour;

        void SetScriptInstance<T>(T instance) where T : MonoBehaviour;

        T GetInstance<T>();

        void SetInstance<T>(T instance);

        void Clear();
    }
}
