using UnityEngine;

namespace CodeInUnity.Scripts.Movement
{
  public class FollowTargetHidingDelay : MonoBehaviour
  {
    public Transform target;

    public Vector3 offset;

    [SerializeField]
    private bool calculateInitialOffset;

    [SerializeField]
    [Min(1f)]
    private float deltaDivider = 1f;

    private Vector3 targetPrevPosition;

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
        var delta = this.target.position - targetPrevPosition;
        transform.position = (this.target.position + this.offset) + delta / this.deltaDivider;

        targetPrevPosition = this.target.position;
      }
      else
      {
        this.enabled = false;
      }
    }
  }
}