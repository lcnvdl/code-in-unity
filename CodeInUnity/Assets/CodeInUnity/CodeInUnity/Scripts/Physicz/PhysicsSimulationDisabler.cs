using UnityEngine;

namespace CodeInUnity.Scripts.Physicz
{
  public class PhysicsSimulationDisabler : MonoBehaviour
  {
    void OnEnable()
    {
#if UNITY_2022_3_OR_NEWER
      Physics.simulationMode = SimulationMode.Script;
#else
      Physics.autoSimulation = false;
#endif
      Physics2D.simulationMode = SimulationMode2D.Script;
    }
  }
}