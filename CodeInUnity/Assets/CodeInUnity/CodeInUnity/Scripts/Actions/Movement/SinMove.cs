using UnityEngine;

namespace CodeInUnity.Scripts.Actions.Movement
{
  public class SinMove : MonoBehaviour
  {
    public float frequency = 1f;

    public Vector3 amplitude = new Vector3(0, 1, 0);

    public Vector3 initialPosition = new Vector3(0, 0, 0);

    public bool relativeToInitialPosition = false;

    private void Start()
    {
      this.initialPosition = transform.position;
    }

    void FixedUpdate()
    {
      if (relativeToInitialPosition)
      {
        transform.position = this.initialPosition + this.amplitude * Mathf.Sin(Time.fixedTime * this.frequency);
      }
      else
      {
        transform.localPosition = this.amplitude * Mathf.Sin(Time.fixedTime * this.frequency);
      }
    }
  }
}