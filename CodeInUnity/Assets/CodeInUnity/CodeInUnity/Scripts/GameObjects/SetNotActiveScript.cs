using UnityEngine;

public class SetNotActiveScript : MonoBehaviour
{
  public void SetNotActive(bool value)
  {
    gameObject.SetActive(!value);
  }
}
