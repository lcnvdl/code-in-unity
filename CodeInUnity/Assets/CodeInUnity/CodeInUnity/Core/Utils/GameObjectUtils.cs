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

        public static bool IsObjectOrChildrenOfOrParentOf(GameObject self, GameObject other)
        {
            return IsObjectOrChildrenOf(self, other) || IsObjectOrParentOf(self, other);
        }

        public static bool IsObjectOrChildrenOf(GameObject self, GameObject other)
        {
            if (self == other)
            {
                return true;
            }

            if (self.transform.parent == null)
            {
                return false;
            }

            return IsObjectOrChildrenOf(self.transform.parent.gameObject, other);
        }

        public static bool IsObjectOrParentOf(GameObject self, GameObject other)
        {
            if (self == other)
            {
                return true;
            }

            if (other.transform.parent == null)
            {
                return false;
            }

            return IsObjectOrParentOf(self, other.transform.parent.gameObject);
        }

        public static bool CompareTagInParent(Transform self, string tag)
        {
            if (self.parent == null)
            {
                return false;
            }

            if (self.parent.CompareTag(tag))
            {
                return true;
            }

            return CompareTagInParent(self.parent, tag);
        }

        public static int GetInRange<T>(Vector3 pos, float range, T[] objects, ref List<T> listToFill) where T : Component
        {
            if (listToFill == null)
            {
                listToFill = new List<T>();
            }
            else
            {
                listToFill.Clear();
            }

            if (objects == null || objects.Length == 0)
            {
                return 0;
            }

            foreach (var obj in objects)
            {
                float distance = Vector3.Distance(pos, obj.transform.position);
                if (distance <= range)
                {
                    listToFill.Add(obj);
                }
            }

            return listToFill.Count;
        }

        public static T GetCloser<T>(Vector3 pos, List<T> objects, float minDistance) where T : Component
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

        /// <summary>
        /// Como DirectionToObject pero solo eje Y rota.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static Quaternion LookToObject(Transform self, Transform other)
        {
            Vector3 target = new Vector3(other.position.x, self.position.y, other.position.z);
            return LookToPoint(self.position, target);
        }

        public static Quaternion LookToPoint(Vector3 self, Vector3 target)
        {
            Vector3 v = target - self;
            if (v == Vector3.zero)
                return Quaternion.identity;
            else
                return Quaternion.LookRotation(v);
        }

        public static Quaternion DirectionToObject(Transform self, Transform other, Vector3 offset)
        {
            return Quaternion.LookRotation(other.position - self.position + offset);
        }
    }
}
