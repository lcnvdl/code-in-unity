using UnityEngine;

namespace CodeInUnity.Interfaces
{
  public interface IGameObjectFactory
  {
    string EntityName { get; }

    GameObject GetNewInstance(GameObject prefab);
  }
}
