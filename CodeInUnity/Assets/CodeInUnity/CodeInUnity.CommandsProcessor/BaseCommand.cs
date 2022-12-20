using System;
using UnityEngine;

namespace CodeInUnity.Command
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

        public bool isAsync = false;

        [HideInInspector]
        public int internalOrder = 0;

        public int priority = 0;

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

        public CommandStatus Status => this.status;

        [SerializeField]
        [HideInInspector]
        private float totalTime = 0f;

        public virtual float Progress => this.IsInFinishedStatus ? 1f : 0f;

        public float TotalTime => this.totalTime;

        public GameObject manualTarget = null;

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
            uuid = Guid.NewGuid().ToString();
        }

        public virtual void Start(GameObject gameObject)
        {
            this.status = CommandStatus.Running;
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
            this.LastGameObject = this.manualTarget ?? gameObject;
            this.Work(dt, this.manualTarget ?? gameObject);
            totalTime += dt;
        }

        public virtual void Cancel(string reason = null)
        {
            this.status = CommandStatus.Cancelled;
            //Debug.Log(this.GetType().Name + " cancelled " + (reason ?? string.Empty));
        }

        protected virtual void Finish()
        {
            this.status = CommandStatus.Finished;
            //Debug.Log(this.GetType().Name + " finished");
        }

        protected abstract void Work(float dt, GameObject gameObject);

        public BaseCommand Clone()
        {
            return this.MemberwiseClone() as BaseCommand;
        }
    }
}
