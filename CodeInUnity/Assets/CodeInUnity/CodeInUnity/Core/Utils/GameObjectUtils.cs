using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
    public static class GameObjectUtils
    {
        private static MethodInfo findObjectFromInstanceIDMethod;

        private static MethodInfo FindObjectFromInstanceIDMethod
        {
            get
            {
                if (findObjectFromInstanceIDMethod == null)
                {
                    findObjectFromInstanceIDMethod = typeof(UnityEngine.Object)
                        .GetMethod("FindObjectFromInstanceID", BindingFlags.NonPublic | BindingFlags.Static);
                }

                return findObjectFromInstanceIDMethod;
            }
        }

        public static UnityEngine.Object FindObjectFromInstanceID(int iid)
        {
            return (UnityEngine.Object)FindObjectFromInstanceIDMethod.Invoke(null, new object[] { iid });
        }

        public static T GetCloser<T>(Vector3 pos, List<T> objects, float minDistance) where T : MonoBehaviour
        {
            if (objects == null || objects.Count == 0)
                return null;

            T result = null;
            float lastDistance = minDistance;

            foreach (var obj in objects)
            {
                float distance = Vector3.Distance(pos, obj.transform.position);
                if (distance < lastDistance)
                {
                    lastDistance = distance;
                    result = obj;
                }
            }

            return result;
        }

        public static Transform GetCloser(Vector3 pos, List<Transform> ts)
        {
            if (ts == null || ts.Count == 0)
                return null;

            Transform t = ts[0];
            float d = Vector3.Distance(pos, t.position);

            for (int i = 1; i < ts.Count; i++)
            {
                float d2 = Vector3.Distance(pos, t.position);
                if (d2 < d)
                {
                    d = d2;
                    t = ts[i];
                }
            }

            return t;
        }

        public static Transform GetCloser(Vector3 pos, params Transform[] ts)
        {
            if (ts == null || ts.Length == 0)
                return null;

            Transform t = ts[0];
            float d = Vector3.Distance(pos, t.position);

            for (int i = 1; i < ts.Length; i++)
            {
                float d2 = Vector3.Distance(pos, t.position);
                if (d2 < d)
                {
                    d = d2;
                    t = ts[i];
                }
            }

            return t;
        }
    }
}
