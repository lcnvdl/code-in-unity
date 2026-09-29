using UnityEngine;

namespace CodeInUnity.Scripts.Utils
{
  public class UnpackScript : MonoBehaviour
  {
    public bool destroyContainer = true;

    private void Start()
    {
      while (this.transform.childCount > 0)
      {
        this.transform.GetChild(0).SetParent(transform.parent, true);
      }

      if (this.destroyContainer)
      {
        Destroy(gameObject);
      }
      else
      {
        Destroy(this);
      }
    }
  }
}
