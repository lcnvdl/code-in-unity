using UnityEngine;

namespace CodeInUnity.Interfaces
{
  public interface IGameObjectFactory
  {
    string EntityName { get; }

    GameObject GetNewInstance(GameObject prefab);

    GameObject GetNewInstance(GameObject prefab, Vector3 position, Quaternion rotation);
  }
}
