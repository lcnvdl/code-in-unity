using System;
using System.Collections;
using System.Collections.Generic;
using CodeInUnity.Interfaces;
using UnityEngine;

namespace CodeInUnity.Scripts.Managers
{
  public class SmoothTasksProcessorScript : MonoBehaviour
  {

    private static SmoothTasksProcessorScript instance;

    public static SmoothTasksProcessorScript RawInstance => instance;

    public static SmoothTasksProcessorScript Instance
    {
      get
      {
        if (instance == null)
        {
          instance = FindAnyObjectByType<SmoothTasksProcessorScript>();

          if (instance == null)
          {
            var go = new GameObject("SmoothTasksProcessorScript");
            instance = go.AddComponent<SmoothTasksProcessorScript>();
          }
        }

        return instance;
      }
    }

    [SerializeField]
    [HideInInspector]
    [SerializeReference]
    private List<ISmoothTask> tasks = new List<ISmoothTask>();

    [SerializeField]
    [HideInInspector]
    [SerializeReference]
    private List<ISmoothTask> newTasks = new List<ISmoothTask>();

    private void OnEnable()
    {
      instance = this;

      StartCoroutine(this.Process());
    }

    private void OnDisable()
    {
      StopCoroutine(this.Process());
    }

    private IEnumerator Process()
    {
      int MAX_MILLIS = 5; // tweak this to prevent frame rate reduction

      var watch = new System.Diagnostics.Stopwatch();

      while (true)
      {
        watch.Start();

        if (this.newTasks.Count > 0)
        {
          this.tasks.AddRange(this.newTasks);
          this.newTasks.Clear();
        }

        int count = tasks.Count;

        for (int i = count - 1; i >= 0; i--)
        {
          if (watch.ElapsedMilliseconds > MAX_MILLIS)
          {
            watch.Reset();
            yield return null;
            watch.Start();
          }

          var task = this.tasks[i];

          try
          {
            bool wasFinished = task.NextTick();

            if (wasFinished)
            {
              this.tasks.RemoveAt(i);
            }
          }
          catch (Exception ex)
          {
            Debug.LogException(ex);
            this.tasks.RemoveAt(i);
          }
        }

        watch.Stop();

        yield return new WaitForSeconds(0.5f);
      }
    }

    public void AddTasks(ISmoothTask[] tasks)
    {
      foreach (var task in tasks)
      {
        if (!this.newTasks.Contains(task) && !this.tasks.Contains(task))
        {
          this.newTasks.Add(task);
        }
      }
    }
  }
}