using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodiesCarrier : MonoBehaviour
{
    public bool useTriggerAsSensor = false;

    public Rigidbody myRigidbody;

    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    [SerializeField]
    [HideInInspector]
    private List<RigidbodiesCarrierSensor> sensors = new List<RigidbodiesCarrierSensor>();

    [SerializeField]
    [HideInInspector]
    private Vector3 lastEulerAngles;

    [SerializeField]
    [HideInInspector]
    private Vector3 lastPosition;

    [SerializeField]
    [HideInInspector]
    private Transform myTransform;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myTransform = transform;
        lastPosition = transform.position;
        lastEulerAngles = transform.rotation.eulerAngles;
        sensors = GetComponentsInChildren<RigidbodiesCarrierSensor>().ToList();

        if (useTriggerAsSensor)
        {
            foreach (var sensor in sensors)
            {
                sensor.carrier = this;
            }
        }
    }

    private void LateUpdate()
    {
        if (rigidbodies.Count > 0)
        {
            var velocity = myTransform.position - lastPosition;
            var angularVelocity = myTransform.eulerAngles - lastEulerAngles;

            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.transform.Translate(velocity, Space.World);
                RotateRigidbody(rigidbody, angularVelocity.y);
            }
        }

        lastPosition = transform.position;
        lastEulerAngles = transform.rotation.eulerAngles;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (this.useTriggerAsSensor)
        {
            return;
        }

        Add(other.rigidbody);
    }

    private void OnCollisionExit(Collision other)
    {
        if (this.useTriggerAsSensor)
        {
            return;
        }

        Remove(other.rigidbody);
    }

    public bool TryToRemoveBasedOnSensors(Rigidbody rb)
    {
        bool isBodyInSensor = this.sensors.Any(m => m.Contains(rb));

        if (isBodyInSensor)
        {
            Remove(rb);
        }

        return isBodyInSensor;
    }

    public void Add(Rigidbody rb)
    {
        if (rb != null && !rigidbodies.Contains(rb))
        {
            rigidbodies.Add(rb);
        }
    }

    public void Remove(Rigidbody rb)
    {
        if (rb != null)
        {
            rigidbodies.Remove(rb);
        }
    }

    private void RotateRigidbody(Rigidbody rb, float amount)
    {
        rb.transform.RotateAround(myTransform.position, Vector3.up, amount);
    }
}
