using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Movement
{
  public class RotateTowards : MonoBehaviour, ITargeteable
  {
    public Transform target;
    public float speed = 1f;
    //public bool onlyYAxis = false;

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

      if (target == null)
      {
        finished = true;
        onFinish.Invoke();
        return;
      }

      var targetRotation = Quaternion.LookRotation(target.position - transform.position);

      transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);

      if (transform.rotation == targetRotation)
      {
        finished = true;
        onFinish.Invoke();
      }
    }
  }
}