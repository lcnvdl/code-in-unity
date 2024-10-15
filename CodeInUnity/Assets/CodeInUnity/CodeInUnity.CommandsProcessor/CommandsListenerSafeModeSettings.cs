using System;

namespace CodeInUnity.CommandsProcessor
{
  [Serializable]
  public struct CommandsListenerSafeModeSettings
  {
    public bool enabled;

    public bool logErrorsInUnityConsole;

    public static CommandsListenerSafeModeSettings Default => Unsafe;

    public static CommandsListenerSafeModeSettings Unsafe => new CommandsListenerSafeModeSettings()
    {
      enabled = false,
      logErrorsInUnityConsole = false,
    };

    public static CommandsListenerSafeModeSettings Safe => new CommandsListenerSafeModeSettings()
    {
      enabled = true,
      logErrorsInUnityConsole = false,
    };

    public static CommandsListenerSafeModeSettings SafeWithErrorLog => new CommandsListenerSafeModeSettings()
    {
      enabled = true,
      logErrorsInUnityConsole = true,
    };
  }
}
