using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private bool calculateInitialOffset;

    private void Start()
    {
        if (this.calculateInitialOffset && this.target != null && this.offset == Vector3.zero)
        {
            this.offset = transform.position - this.target.position;
        }
    }

    private void OnValidate()
    {
        if (this.calculateInitialOffset && this.target != null)
        {
            this.offset = transform.position - this.target.position;
        }
    }

    private void Update()
    {
        if (this.target != null)
        {
            transform.position = this.target.position + this.offset;
        }
        else
        {
            EditorDebug.LogWarning($"FollowTarget: target is null. Disabled in {gameObject.name}.");
            this.enabled = false;
        }
    }
}