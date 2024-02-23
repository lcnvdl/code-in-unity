using CodeInUnity.Core.Events;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public BoolUnityEvent pauseChanged;

    public bool IsPaused => Time.timeScale == 0;

    private void Start()
    {
        Instance = this;
    }

    public void Pause()
    {
        pauseChanged.Invoke(true);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        pauseChanged.Invoke(false);
        Time.timeScale = 1;
    }
}
