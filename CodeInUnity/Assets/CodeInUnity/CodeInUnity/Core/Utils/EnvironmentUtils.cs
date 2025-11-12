using System;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
  public static class EnvironmentUtils
  {
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
  }
}
