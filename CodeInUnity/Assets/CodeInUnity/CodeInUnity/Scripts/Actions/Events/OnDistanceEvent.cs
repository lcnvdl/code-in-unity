using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Events
{
  public class OnDistanceEvent : MonoBehaviour
  {
    //  3 Different options
    public Transform objectToTest;

    public string objectNameTest;

    public string tagTest;

    //  Distance and center
    public float distance = 400;

    public Transform center;

    //  Delay (optimization)
    public float delayBetweenTests = 1f;

    //  Events
    public UnityEvent<Transform> onObjectEnter;

    public UnityEvent<Transform> onObjectExit;

    //  Privates

    private float currentDelay = 0f;

    [SerializeField]
    private List<Transform> objectsInRange = new List<Transform>();

    //  Methods

    private void Start()
    {
      if (this.center == null)
      {
        this.center = transform;
      }
    }

    private void Update()
    {
      //  Delay (optimization)
      if (this.delayBetweenTests > 0f)
      {
        if (this.currentDelay > 0f)
        {
          this.currentDelay -= Time.deltaTime;
          return;
        }

        this.currentDelay += this.delayBetweenTests;
      }

      //  Test current objects
      for (int i = this.objectsInRange.Count - 1; i >= 0; i--)
      {
        var go = this.objectsInRange[i];
        if (go == null)
        {
          //EditorDebug.Log($"Null object removed from OnDistanceEvent in {this.name}");
          this.objectsInRange.RemoveAt(i);
        }
        else if (!this.IsInRange(go))
        {
          //EditorDebug.Log($"Existing object {go.name} removed from OnDistanceEvent in {this.name}");
          this.onObjectExit?.Invoke(go);
          this.objectsInRange.RemoveAt(i);
        }
      }

      //  Find new objects
      if (this.objectToTest != null)
      {
        this.ProcessDetectedObject(this.objectToTest);
      }

      if (!string.IsNullOrEmpty(this.objectNameTest))
      {
        var go = GameObject.Find(this.objectNameTest);
        if (go != null)
        {
          this.ProcessDetectedObject(go.transform);
        }
      }

      if (!string.IsNullOrEmpty(this.tagTest))
      {
        var objects = GameObject.FindGameObjectsWithTag(this.tagTest);
        foreach (var go in objects)
        {
          this.ProcessDetectedObject(go.transform);
        }
      }
    }

    private void ProcessDetectedObject(Transform obj)
    {
      if (this.IsInRange(obj))
      {
        if (!objectsInRange.Contains(obj))
        {
          //EditorDebug.Log($"Object {obj.name} added to list in OnDistanceEvent in {this.name}");
          objectsInRange.Add(obj);
          onObjectEnter?.Invoke(obj);
        }
      }
      else
      {
        if (objectsInRange.Contains(obj))
        {
          //EditorDebug.Log($"Object {obj.name} removed from list in OnDistanceEvent in {this.name}");
          objectsInRange.Remove(obj);
          onObjectExit?.Invoke(obj);
        }
      }
    }

    private bool IsInRange(Transform obj)
    {
      bool isInRange = Vector3.Distance(this.center.position, obj.position) <= this.distance;
      return isInRange;
    }
  }
}