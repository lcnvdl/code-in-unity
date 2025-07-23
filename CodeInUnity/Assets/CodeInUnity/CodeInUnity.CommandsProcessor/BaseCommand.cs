using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeInUnity.CommandsProcessor
{
  [Serializable]
  public abstract class BaseCommand
  {
    [HideInInspector]
    public Guid uuid;

    public string internalId;

    public string nameForDebug;

    public string group;

    public string customTag;

    [SerializeField]
    private List<string> dependenciesByInternalId;

    public bool isAsync = false;

    [HideInInspector]
    public int internalOrder = 0;

    /// <summary>
    /// Less means more priority.
    /// </summary>
    public int priority = 0;

    public float timeout = 0;

    public float asyncDelayBeforeStart = 0f;

    [SerializeField]
    [HideInInspector]
    private CommandStatus status = CommandStatus.NotStarted;

    public bool IsRunning => this.status == CommandStatus.Running;

    /// <summary>
    /// Pausado porque se asigno CPU a otro con más prioridad.
    /// </summary>
    public bool IsPaused => this.status == CommandStatus.Paused || this.status == CommandStatus.PausedBeforeStart;

    public bool IsCancelled => this.status == CommandStatus.Cancelled;

    public bool IsFinished => this.status == CommandStatus.Finished;

    public bool IsInFinishedStatus => this.IsFinished || this.IsCancelled;

    public bool HasDependencies => this.dependenciesByInternalId != null && this.dependenciesByInternalId.Count > 0;

    public List<string> DependenciesByInternalId => this.dependenciesByInternalId;

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
      this.uuid = Guid.NewGuid();

#if UNITY_EDITOR
      this.nameForDebug = this.GetType().Name;
#endif
    }

    public BaseCommand(Guid uuid)
    {
      this.uuid = uuid;

#if UNITY_EDITOR
      this.nameForDebug = this.GetType().Name;
#endif
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
      if (this.status == CommandStatus.NotStarted)
      {
        this.status = CommandStatus.PausedBeforeStart;
      }
      else
      {
        this.status = CommandStatus.Paused;
      }
    }

    public virtual void Unpause()
    {
      if (this.status == CommandStatus.PausedBeforeStart)
      {
        this.status = CommandStatus.NotStarted;
      }
      else
      {
        this.status = CommandStatus.Running;
      }
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

    public void AddInternalIdDependency(string internalId)
    {
      if (this.dependenciesByInternalId == null)
      {
        this.dependenciesByInternalId = new List<string>();
      }

      this.dependenciesByInternalId.Add(internalId);
    }

    public void RemoveDependencyByInternalId(string internalId)
    {
      if (this.dependenciesByInternalId == null)
      {
        return;
      }

      this.dependenciesByInternalId.Remove(internalId);
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

    public BaseCommand WithDependency(string internalId)
    {
      this.AddInternalIdDependency(internalId);
      return this;
    }

    public BaseCommand AsAsync()
    {
      this.isAsync = true;
      return this;
    }

    public BaseCommand SetTimeout(float timeoutInSeconds)
    {
      this.timeout = timeoutInSeconds;
      return this;
    }

    public BaseCommand WithAsyncDelay(float delayInSeconds)
    {
      this.asyncDelayBeforeStart = delayInSeconds;
      return this;
    }

    public BaseCommand WithInternalId(string id)
    {
      this.internalId = id;
      return this;
    }

    public BaseCommand Clone()
    {
      var clone = (BaseCommand)this.MemberwiseClone();

      if (this.dependenciesByInternalId != null)
      {
        clone.dependenciesByInternalId = new List<string>();
        clone.dependenciesByInternalId.AddRange(this.dependenciesByInternalId);
      }

      return clone;
    }
  }
}
