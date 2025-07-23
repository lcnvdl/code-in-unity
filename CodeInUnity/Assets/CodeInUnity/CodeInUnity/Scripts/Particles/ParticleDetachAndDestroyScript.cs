using UnityEngine;
using System.Collections;

namespace CodeInUnity.Scripts.Particles
{
  public class ParticleDetachAndDestroyScript : MonoBehaviour
  {
    public ParticleSystem ps;

    public bool destroyAfterPSIsNotAlive = true;

    private void OnValidate()
    {
      if (ps == null)
      {
        ps = GetComponentInChildren<ParticleSystem>();
      }
    }

    public void DetachAndDestroy()
    {
      transform.parent = null;

      ps.Stop();

      if (this.destroyAfterPSIsNotAlive)
      {
        StartCoroutine(CheckIfAlive());
      }
    }

    private IEnumerator CheckIfAlive()
    {
      while (ps.IsAlive())
      {
        yield return null;
      }

      Destroy(ps.gameObject);
    }
  }
}