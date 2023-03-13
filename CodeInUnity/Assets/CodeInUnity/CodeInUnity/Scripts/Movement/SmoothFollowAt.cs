using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowAt : MonoBehaviour
{
    public Transform target;

    public Vector3 deltaPosition;

    public Vector3 deltaRotation;

    public float speed = 10f;

    public float angularSpeed = 180f;

    public bool autoCalculateDeltas = true;

    void Start()
    {
        if (autoCalculateDeltas)
        {
            deltaPosition = transform.position - target.position;

            deltaRotation = transform.rotation.eulerAngles - target.rotation.eulerAngles;
        }
    }

    void Update()
    {
        var targetRotation = Quaternion.Euler(target.rotation.eulerAngles + deltaRotation);

        transform.SetPositionAndRotation(
            Vector3.MoveTowards(transform.position, target.position + deltaPosition, speed * Time.deltaTime),
            Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime));
    }
}
