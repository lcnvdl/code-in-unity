using System.Collections.Generic;
using UnityEngine;

namespace CodeInUnity.Scripts.Physicz
{
  public class RigidbodiesCarrierSensor : MonoBehaviour
  {
    [HideInInspector]
    public RigidbodiesCarrier carrier;

    [HideInInspector]
    [SerializeField]
    private List<Rigidbody> rigidbodies = new List<Rigidbody>();

    public bool Contains(Rigidbody rb)
    {
      return this.rigidbodies.Contains(rb);
    }

    private void OnTriggerEnter(Collider other)
    {
      var rb = other.GetComponent<Rigidbody>();
      if (rb != null && rb != carrier.myRigidbody)
      {
        if (!rigidbodies.Contains(rb))
        {
          rigidbodies.Add(rb);
        }

        carrier.Add(rb);
      }
    }

    private void OnTriggerExit(Collider other)
    {
      var rb = other.GetComponent<Rigidbody>();
      if (rb != null && rb != carrier.myRigidbody)
      {
        rigidbodies.Remove(rb);
        //carrier.Remove(rb);

        carrier.TryToRemoveBasedOnSensors(rb);
      }
    }
  }
}