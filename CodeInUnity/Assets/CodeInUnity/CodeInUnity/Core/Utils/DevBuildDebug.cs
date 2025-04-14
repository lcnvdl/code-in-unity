using System.Linq;

namespace UnityEngine
{
  public static class DevBuildDebug
  {
#if !UNITY_EDITOR && DEVELOPMENT_BUILD
    public static readonly bool IsEnabled = true;
#else
    public static readonly bool IsEnabled = false;
#endif

    public static void Log(object message)
    {
#if !UNITY_EDITOR && DEVELOPMENT_BUILD
      Debug.Log(message);
#endif
    }

    public static void LogWarning(object message)
    {
#if !UNITY_EDITOR && DEVELOPMENT_BUILD
      Debug.LogWarning(message);
#endif
    }

    public static void LogError(object message)
    {
#if !UNITY_EDITOR && DEVELOPMENT_BUILD
      Debug.LogError(message);
#endif
    }

    public static void LogIf(bool isActivated, object message)
    {
#if !UNITY_EDITOR && DEVELOPMENT_BUILD
      if (isActivated)
      {
        Log(message);
      }
#endif
    }

    public static void Log(string message, params object[] args)
    {
#if !UNITY_EDITOR && DEVELOPMENT_BUILD
      Debug.Log(message + " " + string.Join(" ", args.Select(m => (m ?? "").ToString())));
#endif
    }

    public static void LogIf(bool isActivated, string message, params object[] args)
    {
#if !UNITY_EDITOR && DEVELOPMENT_BUILD
      if (isActivated)
      {
        Log(message, args);
      }
#endif
    }
  }
}
