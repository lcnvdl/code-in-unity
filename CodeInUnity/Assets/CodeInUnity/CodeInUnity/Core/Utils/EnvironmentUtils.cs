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
  }
}
