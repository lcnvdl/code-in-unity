using UnityEngine;

namespace CodeInUnity.Scripts.Physicz
{
  public class PhysicsSimulationDisabler : MonoBehaviour
  {
    void OnEnable()
    {
      Physics.autoSimulation = false;
      Physics2D.simulationMode = SimulationMode2D.Script;
    }
  }
}