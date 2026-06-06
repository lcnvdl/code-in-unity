using System;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
  public static class EnvironmentUtils
  {
    private static bool? isSteamDeck;

#if UNITY_EDITOR
    public static bool IsInTestMode => Application.isEditor && Environment.StackTrace.Contains("UnityEngine.TestRunner");
#else
    public const bool IsInTestMode = false;
#endif

#if DEVELOPMENT_BUILD
    public const bool IsDevelopmentBuild = true;
#elif UNITY_EDITOR
    public static bool IsDevelopmentBuild => false; // To remove the unreachable code warning
#else
    public const bool IsDevelopmentBuild = false;
#endif

    public static bool IsDevelopmentBuildOrUnityEditor => IsDevelopmentBuild || Application.isEditor;

    public static Func<bool> IsSteamDeck = InternalIsSteamDeck;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetForPlayModeWithoutDomainReload()
    {
      IsSteamDeck = InternalIsSteamDeck;
    }

    private static bool InternalIsSteamDeck()
    {
      if (!isSteamDeck.HasValue)
      {
        isSteamDeck = "steamdeck".Equals(SystemInfo.deviceName, StringComparison.OrdinalIgnoreCase) ||
          Environment.GetEnvironmentVariable("SteamDeck") == "1" ||
          SystemInfo.operatingSystem.Contains("SteamOS");
      }

      return isSteamDeck.Value;
    }
  }
}
