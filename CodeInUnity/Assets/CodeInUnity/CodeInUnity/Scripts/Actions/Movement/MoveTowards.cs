using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Movement
{
  public class MoveTowards : MonoBehaviour, ITargeteable
  {
    public Transform target;
    public float speed = 1f;
    public float miniumDistance = 0.001f;
    public bool lockYAxis = false;
    public bool stopOnCloser = false;

    public UnityEvent onFinish;

    [HideInInspector]
    [SerializeField]
    private Vector3 lastTargetPosition = Vector3.zero;

    [HideInInspector]
    [SerializeField]
    private bool finished = false;

    public Transform Target { get => target; set => target = value; }

    void Update()
    {
      if (finished)
      {
        return;
      }

      Vector3 targetPosition;

      if (target == null)
      {
        targetPosition = lastTargetPosition;
      }
      else
      {
        targetPosition = new Vector3(target.position.x, lockYAxis ? transform.position.y : target.position.y, target.position.z);
        lastTargetPosition = targetPosition;
      }

      float step = speed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

      if (Vector3.Distance(transform.position, targetPosition) < miniumDistance)
      {
        if (!stopOnCloser)
        {
          transform.position = targetPosition;
        }

        finished = true;
        onFinish.Invoke();
      }
    }
  }
}