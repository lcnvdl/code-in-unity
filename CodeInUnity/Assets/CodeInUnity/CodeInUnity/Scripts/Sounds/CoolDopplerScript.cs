using UnityEngine;

namespace CodeInUnity.Scripts.Sounds
{
  public class CoolDopplerScript : MonoBehaviour
  {
    public AudioListener listener;

    public AudioHighPassFilter source;

    public float minDistance = 1f;

    public float maxDistance = 10f;

    public float maxDisort = 1000f;

    void Update()
    {
      float distance = Vector3.Distance(listener.transform.position, source.transform.position);

      if (distance > minDistance)
      {
        source.cutoffFrequency = maxDisort * Mathf.Min(maxDistance, (distance - minDistance)) / maxDistance;
      }
    }
  }
}