using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeInUnity.Scripts.Actions.Events
{
  [Serializable]
  public class CollisionEvent : UnityEvent<Collision>
  {
  }
}