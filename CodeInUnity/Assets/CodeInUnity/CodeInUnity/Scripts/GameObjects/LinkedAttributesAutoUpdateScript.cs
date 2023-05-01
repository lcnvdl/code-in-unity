using UnityEngine;

namespace CodeInUnity.Scripts.GameObjects
{
    public class LinkedAttributesAutoUpdateScript : MonoBehaviour
    {
        public float delayBetweenUpdates = 0f;

        private float currentDelay = 0f;

        private LinkedAttributesScript[] linkedAttributes;

        private void OnEnable()
        {
            this.linkedAttributes = GetComponents<LinkedAttributesScript>();
        }

        private void Update()
        {
            if (this.currentDelay > 0)
            {
                this.currentDelay -= Time.deltaTime;
                return;
            }

            foreach (var attribute in this.linkedAttributes)
            {
                attribute.Sync();
            }

            this.currentDelay += this.delayBetweenUpdates;
        }
    }
}
