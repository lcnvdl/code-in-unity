namespace CodeInUnity.Scripts.Managers
{
  using System;
  using System.Collections.Generic;
  using CodeInUnity.Interfaces;
  using UnityEngine;

  public class DynamicBehavioursManagerScript : MonoBehaviour
  {
    private static DynamicBehavioursManagerScript instance;

    private List<IDynamicBehaviour> behaviours = new List<IDynamicBehaviour>(100);
    private List<IDynamicBehaviour> currentLoopBehaviours = new List<IDynamicBehaviour>(100);

    public int targetFPS = 60;

    private int loopIndex = 0;
    private float accumulatedDeltaTime = 0f;
    private float lastDeltaTime = 0f;

    public static DynamicBehavioursManagerScript RawInstance => instance;

    public static DynamicBehavioursManagerScript Instance
    {
      get
      {
        if (instance == null)
        {
          instance = FindAnyObjectByType<DynamicBehavioursManagerScript>();
        }

        return instance;
      }
    }

    private void OnEnable()
    {
      instance = this;
    }

    private void Update()
    {
      if (this.behaviours.Count == 0 || targetFPS == 0)
      {
        return;
      }

      if (this.currentLoopBehaviours.Count > 0 && loopIndex >= this.currentLoopBehaviours.Count)
      {
        this.currentLoopBehaviours.Clear();
        this.loopIndex = 0;
        this.lastDeltaTime = 0f;
      }

      if (this.currentLoopBehaviours.Count == 0)
      {
        this.currentLoopBehaviours.AddRange(this.behaviours);
        this.loopIndex = 0;
        this.lastDeltaTime = 0f;
      }

      int initialTime = DateTime.Now.Millisecond;
      int tolerance = 1000 / targetFPS;

      if (this.lastDeltaTime == 0)
      {
        this.lastDeltaTime = Time.deltaTime + this.accumulatedDeltaTime;
        this.accumulatedDeltaTime = 0f;
      }

      this.accumulatedDeltaTime += Time.deltaTime;

      try
      {
        while (loopIndex < this.currentLoopBehaviours.Count && tolerance >= 0)
        {
          this.currentLoopBehaviours[loopIndex].DynamicUpdate(this.lastDeltaTime);

          int elapsed = DateTime.Now.Millisecond - initialTime;
          tolerance -= elapsed;

          loopIndex++;
        }
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
        loopIndex++;
      }
    }

    public void Register(IDynamicBehaviour behaviour)
    {
      this.behaviours.Add(behaviour);
    }

    public void Unregister(IDynamicBehaviour behaviour)
    {
      this.behaviours.Remove(behaviour);
    }
  }

}