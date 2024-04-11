using UnityEngine;

namespace CodeInUnity.Scripts.GameObjects
{
  public class SetNotActiveScript : MonoBehaviour
  {
    public void SetNotActive(bool value)
    {
      gameObject.SetActive(!value);
    }
  }
}