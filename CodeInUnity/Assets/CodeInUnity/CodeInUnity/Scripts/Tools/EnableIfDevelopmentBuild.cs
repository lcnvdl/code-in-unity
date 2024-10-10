using UnityEngine;

namespace CodeInUnity.Scripts.Tools
{
  public class EnableIfDevelopmentBuild : MonoBehaviour
  {
    public bool enable = true;

    public bool disable = false;

    public Transform[] positive;

    public Transform[] negative;

    private Transform[] InstancesToEnable
    {
      get
      {
#if DEVELOPMENT_BUILD
        return positive;
#else
        return negative;
#endif
      }
    }

    private Transform[] InstancesToDisable
    {
      get
      {
#if DEVELOPMENT_BUILD
        return negative;
#else
        return positive;
#endif
      }
    }

    void OnEnable()
    {
      foreach (var instance in InstancesToEnable)
      {
        if (enable)
        {
          instance.gameObject.SetActive(true);
        }
      }

      foreach (var instance in InstancesToDisable)
      {
        if (disable)
        {
          instance.gameObject.SetActive(false);
        }
      }
    }
  }
}