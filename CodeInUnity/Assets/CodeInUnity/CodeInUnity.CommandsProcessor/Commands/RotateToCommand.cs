using System;
using UnityEngine;
using CodeInUnity.Command;

namespace CodeInUnity.Commands
{
    [Serializable]
    public class RotateToCommand : BaseCommand
    {
        public override float Progress => 1f - Mathf.Abs(initialRotation - this.TargetRotation) / 360f;

        public float rotateSpeedMovement = 0.1f;

        public Vector3 targetPoint;

        [SerializeField]
        [HideInInspector]
        private float initialRotation;

        [SerializeField]
        [HideInInspector]
        private float rotateVelocity;

        private float TargetRotation
        {
            get
            {
                var rotationToLookAt = Quaternion.LookRotation(targetPoint - LastGameObject.transform.position);
                return rotationToLookAt.eulerAngles.y;
            }
        }

        public override void Start(GameObject gameObject)
        {
            base.Start(gameObject);

            initialRotation = gameObject.transform.eulerAngles.y;
        }

        protected override void Work(float dt, GameObject gameObject)
        {
            float target = this.TargetRotation;
            float rotationY = Mathf.SmoothDampAngle(gameObject.transform.eulerAngles.y, target, ref rotateVelocity, rotateSpeedMovement * dt);

            if (Mathf.Abs(target - rotationY) < 0.001f)
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, target, gameObject.transform.eulerAngles.z);
                this.Finish();
            }
            else
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, rotationY, gameObject.transform.eulerAngles.z);
            }
        }
    }
}
