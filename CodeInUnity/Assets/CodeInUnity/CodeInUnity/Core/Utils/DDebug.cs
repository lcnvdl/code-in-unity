using System.Linq;

namespace UnityEngine
{
    public static class EditorDebug
    {
        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }

        public static void LogWarning(object message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
        }

        public static void LogIf(bool isActivated, object message)
        {
#if UNITY_EDITOR
            if (isActivated)
            {
                Log(message);
            }
#endif
        }

        public static void Log(string message, params object[] args)
        {
#if UNITY_EDITOR
            Debug.Log(message + " " + string.Join(" ", args.Select(m => (m ?? "").ToString())));
#endif
        }

        public static void LogIf(bool isActivated, string message, params object[] args)
        {
#if UNITY_EDITOR
            if (isActivated)
            {
                Log(message, args);
            }
#endif
        }
    }
}
