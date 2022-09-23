using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PauseEvent : UnityEvent<bool>
{
}

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public PauseEvent pauseChanged;

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
