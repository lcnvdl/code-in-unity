using System;
using UnityEngine;
using CodeInUnity.CommandsProcessor;
using CodeInUnity.Extensions;

namespace CodeInUnity.Commands
{
    [Serializable]
    public class MoveToCommand : BaseCommand
    {
        public override float Progress => (TotalDistance - CurrentDistance) / TotalDistance;

        public float rotateSpeedMovement = 0.1f;

        public Vector3 targetPoint;

        public float speed = 1f;

        public bool ignoreYDelta = true;

        [SerializeField]
        [HideInInspector]
        private Vector3 initialPoint;

        [SerializeField]
        [HideInInspector]
        private Vector3 currentPoint;

        private float lastDistance = 0;

        private float sameDistanceCounter = 0;

        private float Tolerance => this.speed * 1.25f;

        protected virtual bool StuckProtection => true;

        protected virtual float StuckProtectionTime => 2f;

        protected float CurrentDistance => Vector3.Distance(
            currentPoint.Mult(new Vector3(1, ignoreYDelta ? 0 : 1, 1)),
            targetPoint.Mult(new Vector3(1, ignoreYDelta ? 0 : 1, 1)));

        protected float TotalDistance => Vector3.Distance(
            initialPoint.Mult(new Vector3(1, ignoreYDelta ? 0 : 1, 1)),
            targetPoint.Mult(new Vector3(1, ignoreYDelta ? 0 : 1, 1)));

        public MoveToCommand()
        {
        }

        public MoveToCommand(Guid uuid) : base(uuid)
        {
        }

        public override void Start(GameObject unparsedGameObject)
        {
            var gameObject = this.Pipe(unparsedGameObject);

            base.Start(gameObject);

            this.initialPoint = gameObject.transform.position;
            this.currentPoint = gameObject.transform.position;
        }

        protected override void Work(float dt, GameObject unparsedGameObject)
        {
            var gameObject = this.Pipe(unparsedGameObject);

            this.currentPoint = gameObject.transform.position;

            if (this.CurrentDistance <= this.Tolerance * dt)
            {
                this.GoalReached(gameObject);
                this.Finish();
            }
            else
            {
                if (this.StuckProtection && Mathf.Abs(this.lastDistance - this.CurrentDistance) <= float.Epsilon)
                {
                    this.sameDistanceCounter += dt;

                    if (this.sameDistanceCounter > this.StuckProtectionTime)
                    {
                        this.Cancel("got stuck");
                        return;
                    }
                }

                this.MoveTo(gameObject, this.targetPoint, speed, dt);

                this.lastDistance = this.CurrentDistance;
            }
        }

        protected virtual void GoalReached(GameObject gameObject)
        {
            gameObject.transform.position = new Vector3(this.targetPoint.x, this.currentPoint.y, this.targetPoint.z);
        }

        protected virtual void MoveTo(GameObject gameObject, Vector3 targetPoint, float speed, float dt)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPoint, speed * dt);
        }

        protected virtual GameObject Pipe(GameObject mainObject)
        {
            return mainObject;
        }
    }
}
