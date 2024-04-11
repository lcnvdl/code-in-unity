using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeInUnity.CommandsProcessor
{
  public enum CommandStatus
  {
    NotStarted,
    Running,
    Paused,
    Finished,
    Cancelled
  }

  [Serializable]
  public abstract class BaseCommand
  {
    [HideInInspector]
    public string uuid;

    public string internalId;

    [SerializeField]
    private List<string> dependencies;

    public bool isAsync = false;

    [HideInInspector]
    public int internalOrder = 0;

    /// <summary>
    /// Less means more priority.
    /// </summary>
    public int priority = 0;

    public float timeout = 0;

    [SerializeField]
    [HideInInspector]
    private CommandStatus status = CommandStatus.NotStarted;

    public bool IsRunning => this.status == CommandStatus.Running;

    /// <summary>
    /// Pausado porque se asigno CPU a otro con más prioridad.
    /// </summary>
    public bool IsPaused => this.status == CommandStatus.Paused;

    public bool IsCancelled => this.status == CommandStatus.Cancelled;

    public bool IsFinished => this.status == CommandStatus.Finished;

    public bool IsInFinishedStatus => this.IsFinished || this.IsCancelled;

    public bool HasDependencies => this.dependencies != null && this.dependencies.Count > 0;

    public List<string> Dependencies => this.dependencies;

    public CommandStatus Status => this.status;

    [SerializeField]
    [HideInInspector]
    private float totalTime = 0f;

    public virtual float Progress => this.IsInFinishedStatus ? 1f : 0f;

    public float TotalTime => this.totalTime;

    public GameObject manualTarget = null;

    [SerializeField]
    [HideInInspector]
    private GameObject initialGameObject = null;

    public GameObject InitialGameObject => this.initialGameObject;

    [SerializeField]
    [HideInInspector]
    private GameObject lastGameObject;

    public GameObject LastGameObject
    {
      get => this.lastGameObject;
      private set => this.lastGameObject = value;
    }

    public BaseCommand()
    {
      this.uuid = Guid.NewGuid().ToString();
    }

    public BaseCommand(string uuid)
    {
      this.uuid = uuid;
    }

    public virtual void Start(GameObject gameObject)
    {
      this.status = CommandStatus.Running;
      this.initialGameObject = gameObject;
      this.LastGameObject = gameObject;
      //Debug.Log(this.GetType().Name + " started");
    }

    public virtual void Pause()
    {
      this.status = CommandStatus.Paused;
      //Debug.Log(this.GetType().Name + " paused");
    }

    public virtual void Unpause()
    {
      this.status = CommandStatus.Running;
      //Debug.Log(this.GetType().Name + " unpaused");
    }

    public void Step(float dt, GameObject gameObject)
    {
      GameObject target = this.manualTarget == null ? gameObject : this.manualTarget;
      this.LastGameObject = target;
      this.Work(dt, target);
      totalTime += dt;

      if (this.timeout > 0 && this.IsRunning && this.totalTime > this.timeout)
      {
        this.Cancel("Timeout");
      }
    }

    public virtual void Cancel(string reason = null)
    {
      this.status = CommandStatus.Cancelled;
      //Debug.Log(this.GetType().Name + " cancelled " + (reason ?? string.Empty));
      this.OnCancelOrFinish();
    }

    public void AddDependency(string name)
    {
      if (this.dependencies == null)
      {
        this.dependencies = new List<string>();
      }

      this.dependencies.Add(name);
    }

    public void RemoveDependency(string name)
    {
      if (this.dependencies == null)
      {
        return;
      }

      this.dependencies.Remove(name);
    }

    protected virtual void Finish()
    {
      this.status = CommandStatus.Finished;
      //Debug.Log(this.GetType().Name + " finished");
      this.OnCancelOrFinish();
    }

    protected virtual void OnCancelOrFinish()
    {
    }

    protected abstract void Work(float dt, GameObject gameObject);

    public BaseCommand Clone()
    {
      return this.MemberwiseClone() as BaseCommand;
    }
  }
}
