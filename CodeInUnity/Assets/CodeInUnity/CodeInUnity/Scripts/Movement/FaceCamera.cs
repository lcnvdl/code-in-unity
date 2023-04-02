using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        if (this.cam == null)
        {
            this.cam = Camera.main;
        }
    }

    private void Update()
    {
        transform.LookAt(cam.transform, Vector3.up);
    }
}