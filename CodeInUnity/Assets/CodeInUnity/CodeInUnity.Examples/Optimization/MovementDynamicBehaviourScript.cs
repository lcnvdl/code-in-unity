using System.Collections;
using System.Collections.Generic;
using CodeInUnity.Core.Utils;
using CodeInUnity.Scripts.Optimizations;
using UnityEngine;

public class MovementDynamicBehaviourScript : DynamicBehaviour
{
  private Vector3 initialPosition;

  private float initialTime;

  private void Start()
  {
    initialPosition = transform.position;

    initialTime = Time.time;
  }

  void OnMouseDown()
  {
    for (int i = 0; i < 10; i++)
    {
      var newPosition = 
        transform.position + 
        ArrayUtils.Choose(Vector3.left, Vector3.right, Vector3.forward, Vector3.back) * Random.Range(2f, 5f) * transform.localScale.x
      ;
      GameObject clone = Instantiate(gameObject, newPosition, Quaternion.identity);

      clone.GetComponent<MovementDynamicBehaviourScript>().initialPosition = newPosition;

      clone.name = "Cube";
      clone.transform.localScale = transform.localScale * 0.5f;
    }
  }

  public override void DynamicUpdate(float deltaTime)
  {
    const float frequency = 1.0f;
    const float amplitude = 1.0f;

    float timeSinceStart = Time.time - initialTime;
    float sineValue = Mathf.Sin(timeSinceStart * frequency);

    Vector3 newPosition = initialPosition + new Vector3(0, sineValue * amplitude, 0);
    transform.position = newPosition;
  }
}
