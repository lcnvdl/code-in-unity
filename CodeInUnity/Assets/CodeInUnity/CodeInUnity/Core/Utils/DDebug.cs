using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Debug.Log(message + " " + string.Join(' ', args));
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
