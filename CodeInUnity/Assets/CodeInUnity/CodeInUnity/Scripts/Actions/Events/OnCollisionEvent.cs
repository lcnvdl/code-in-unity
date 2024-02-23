using UnityEngine;

namespace CodeInUnity.Scripts.Actions.Events
{
  public class OnCollisionEvent : MonoBehaviour
  {
    public string objectNameTest;

    public string tagTest;

    public CollisionEvent onCollisionEnter;

    public CollisionEvent onCollisionExit;

    private void OnCollisionExit(Collision other)
    {
      if (!string.IsNullOrEmpty(objectNameTest) && other.gameObject.name != objectNameTest)
      {
        return;
      }

      if (!string.IsNullOrEmpty(tagTest) && !other.gameObject.CompareTag(tagTest))
      {
        return;
      }

      if (this.onCollisionExit != null)
      {
        this.onCollisionExit.Invoke(other);
      }
    }

    private void OnCollisionEnter(Collision other)
    {
      if (!string.IsNullOrEmpty(objectNameTest) && other.gameObject.name != objectNameTest)
      {
        return;
      }

      if (!string.IsNullOrEmpty(tagTest) && !other.gameObject.CompareTag(tagTest))
      {
        return;
      }

      if (this.onCollisionEnter != null)
      {
        this.onCollisionEnter.Invoke(other);
      }
    }
  }
}