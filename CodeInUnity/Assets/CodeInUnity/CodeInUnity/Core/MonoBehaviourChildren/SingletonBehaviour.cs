using UnityEngine;

namespace CodeInUnity.Core.MonoBehaviourChildren
{
    public class SingletonBehaviour<T>: MonoBehaviour where T: UnityEngine.Object
    {
        protected virtual bool AutoInstantiate { get { return false; } }

        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null && AutoInstantiate)
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
