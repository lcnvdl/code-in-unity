using UnityEditor;
using UnityEngine;

namespace CodeInUnity.Editor.Scripts
{
  /// <summary>
  /// Menú de utilidades del editor para el paquete Code in Unity.
  /// </summary>
  public static class CodeInUnityEditorMenu
  {
    private const string MenuRoot = "Tools/Code in Unity/Domain Reload/";

    [MenuItem(MenuRoot + "Enable Domain Reload", priority = 0)]
    public static void EnableDomainReload()
    {
      EnableDomainReloadOnEnterPlayMode();
    }

    [MenuItem(MenuRoot + "Disable Domain Reload", priority = 1)]
    public static void DisableDomainReload()
    {
      DisableDomainReloadOnEnterPlayMode();
    }

    [MenuItem(MenuRoot + "Toggle Domain Reload", priority = 2)]
    public static void ToggleDomainReload()
    {
      if (IsDomainReloadDisabledOnEnterPlayMode())
      {
        EnableDomainReloadOnEnterPlayMode();
      }
      else
      {
        DisableDomainReloadOnEnterPlayMode();
      }
    }

    [MenuItem(MenuRoot + "Force Domain Reload", priority = 11)]
    public static void ForceDomainReload()
    {
      Debug.Log("Calling Domain Reload...");
      EditorUtility.RequestScriptReload();
    }

    /// <summary>
    /// Domain reload al entrar en Play está desactivado cuando
    /// <see cref="EditorSettings.enterPlayModeOptionsEnabled"/> es true y el flag
    /// <see cref="EnterPlayModeOptions.DisableDomainReload"/> está activo.
    /// </summary>
    private static bool IsDomainReloadDisabledOnEnterPlayMode()
    {
      return EditorSettings.enterPlayModeOptionsEnabled
          && (EditorSettings.enterPlayModeOptions & EnterPlayModeOptions.DisableDomainReload) != 0;
    }

    private static void EnableDomainReloadOnEnterPlayMode()
    {
      var options = EditorSettings.enterPlayModeOptions & ~EnterPlayModeOptions.DisableDomainReload;
      EditorSettings.enterPlayModeOptions = options;

      if (options == EnterPlayModeOptions.None)
        EditorSettings.enterPlayModeOptionsEnabled = false;

      Debug.Log("Domain Reload ACTIVATED");
    }

    private static void DisableDomainReloadOnEnterPlayMode()
    {
      EditorSettings.enterPlayModeOptionsEnabled = true;
      EditorSettings.enterPlayModeOptions |= EnterPlayModeOptions.DisableDomainReload;
      Debug.Log("Domain Reload DEACTIVATED");
    }
  }
}
