using System;
using UnityEngine;

namespace CodeInUnity.Scripts.Managers
{
  public class UnalignedSlowUpdateManager : MonoBehaviour
  {
    private static bool applicationIsQuitting = false;

    private static UnalignedSlowUpdateManager instance;

    public static UnalignedSlowUpdateManager RawInstance => instance;

    public static UnalignedSlowUpdateManager Instance
    {
      get
      {
        if (instance == null && !applicationIsQuitting)
        {
          instance = FindAnyObjectByType<UnalignedSlowUpdateManager>();

          if (instance == null)
          {
            var go = new GameObject("UnalignedSlowUpdateManager");
            instance = go.AddComponent<UnalignedSlowUpdateManager>();
          }
        }

        return instance;
      }
    }

    public event Action slowQuarterSecUpdate;

    public event Action slowHalfSecUpdate;

    public event Action slowSecUpdate;

    private float slowQuarterSecUpdateDelay = 0.33f;

    private float slowHalfSecUpdateDelay = 0f;

    private float slowSecUpdateDelay = 0f;

    [RuntimeInitializeOnLoadMethod]
    private static void RunOnStart()
    {
      Application.quitting += () => applicationIsQuitting = true;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetForPlayModeWithoutDomainReload()
    {
      applicationIsQuitting = false;
      instance = null;
    }

    private void Start()
    {
      if (instance != null && instance != this)
      {
        Destroy(instance.gameObject);
      }

      instance = this;
    }

    private void Update()
    {
      if (this.slowQuarterSecUpdateDelay <= 0)
      {
        this.slowQuarterSecUpdate?.Invoke();
        this.slowQuarterSecUpdateDelay += 0.25f;

        if (this.slowHalfSecUpdateDelay <= 0)
        {
          this.slowHalfSecUpdate?.Invoke();
          this.slowHalfSecUpdateDelay += 0.5f;

          if (this.slowSecUpdateDelay <= 0)
          {
            this.slowSecUpdate?.Invoke();
            this.slowSecUpdateDelay += 1f;
          }
          else
          {
            this.slowSecUpdateDelay -= 0.5f;
          }
        }
        else
        {
          this.slowHalfSecUpdateDelay -= 0.25f;
        }
      }
      else
      {
        this.slowQuarterSecUpdateDelay -= Time.deltaTime / Time.timeScale;
      }
    }
  }
}
