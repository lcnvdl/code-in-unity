using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    private void Update()
    {
        transform.position = this.target.position + this.offset;
    }
}