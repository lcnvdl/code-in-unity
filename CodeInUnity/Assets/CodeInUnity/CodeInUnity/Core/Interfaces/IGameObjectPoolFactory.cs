using UnityEngine;

namespace CodeInUnity.Interfaces
{
  public interface IGameObjectPoolFactory : IGameObjectFactory
  {
    void DestroyInstance(GameObject instance);

    void Clear();
  }
}
