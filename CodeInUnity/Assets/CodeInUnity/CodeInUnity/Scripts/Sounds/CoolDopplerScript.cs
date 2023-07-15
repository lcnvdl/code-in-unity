using UnityEngine;

public class CoolDopplerScript : MonoBehaviour
{
    public AudioListener listener;

    public AudioHighPassFilter source;

    public float maxDistance = 10f;

    public float maxDisort = 1000f;

    void Update()
    {
        source.cutoffFrequency = maxDisort * Mathf.Min(maxDistance, Vector3.Distance(listener.transform.position, source.transform.position)) / maxDistance;
    }
}
