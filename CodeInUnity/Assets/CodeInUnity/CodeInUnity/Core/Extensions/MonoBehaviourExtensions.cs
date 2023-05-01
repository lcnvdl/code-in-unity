using System.Linq;
using UnityEngine;

namespace CodeInUnity.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static bool HasCustomTag(this Component self, string tag, string value = null)
        {
            var tags = self.GetComponents<CustomTagScript>();
            return tags.Any(customTag => customTag.tagId.Equals(tag) && (value == null || value.Equals(customTag.additionalValue)));
        }

        public static CustomTagScript GetCustomTag(this Component self, string tag, string value = null)
        {
            var tags = self.GetComponents<CustomTagScript>();
            return tags.FirstOrDefault(customTag => customTag.tagId.Equals(tag) && (value == null || value.Equals(customTag.additionalValue)));
        }

        public static T LazyGetInstance<T>(this MonoBehaviour self, ref T value, bool lookInChildren = true) where T : Component
        {
            if (value == null)
            {
                if (!self.TryGetComponent<T>(out value) && lookInChildren)
                {
                    value = self.GetComponentInChildren<T>();
                }
            }

            return value;
        }

        /// <summary>
        /// Fix for "unity objects should not use null coalescing".
        /// </summary>
        public static T GetComponentRealNull<T>(this GameObject self) where T : Component
        {
            T component = self.GetComponent<T>();

            if (component == null)
            {
                return null;
            }

            return component;
        }

        /// <summary>
        /// Fix for "unity objects should not use null coalescing".
        /// </summary>
        public static T GetComponentOrAddIfMissing<T>(this GameObject self) where T : Component
        {
            T component = self.GetComponent<T>();

            if (component == null)
            {
                component = self.AddComponent<T>();
            }

            return component;
        }
    }
}
