using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Tooltip("Transform of the camera to shake. Grabs the gameObject's transform if null.")]
    public Transform camTransform;

    public Transform positionTransform;

    [Tooltip("How long the object should shake for.")]
    public float shakeDuration = 0f;

    [Tooltip("Amplitude of the shake. A larger value shakes the camera harder.")]
    public float shakeAmount = 0.7f;

    public float decreaseFactor = 1.0f;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent<Transform>();
        }
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = positionTransform.transform.position + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
            if (shakeDuration <= 0)
            {
                camTransform.localPosition = positionTransform.transform.position;
            }
        }
        else
        {
            shakeDuration = 0f;
            //camTransform.localPosition = positionTransform.transform.position;
        }
    }
}