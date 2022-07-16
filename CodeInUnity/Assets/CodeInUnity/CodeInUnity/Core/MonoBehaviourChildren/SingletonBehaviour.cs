using UnityEngine;

namespace CodeInUnity.Core.MonoBehaviourChildren
{
    public class SingletonBehaviour<T>: MonoBehaviour where T: UnityEngine.Object
    {
        protected static bool autoInstantiate = true;

        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null && autoInstantiate)
                    {
                        var go = new GameObject(typeof(T).Name);
                        instance = go.AddComponent(typeof(T)) as T;
                    }
                }

                return instance;
            }
        }
    }
}
