using CodeInUnity.StateMachine.Interfaces;
using UnityEngine;

namespace CodeInUnity.StateMachine.Components
{
  public class StateMachineManualScript : MonoBehaviour
  {
    [SerializeReference]
    [HideInInspector]
    public StatesManagerBase machine;

    [Tooltip("Optional: states repository.")]
    public GameObject statesRepositoryReference;

    [Tooltip("Optional. Requires states repository.")]
    public string initialState;

    protected virtual void Start()
    {
      this.machine = this.GenerateNewMachine();
      this.machine.target = gameObject;
      this.machine.statesRepositoryReference = this.statesRepositoryReference;
      this.machine.initialState = initialState;
      this.machine.Initialize();
    }

    public void SetTrigger(string name)
    {
      this.machine.Trigger(name);
    }

    public void SetValue(string name, float value)
    {
      this.machine.variables[name] = value;
    }

    protected virtual StatesManagerBase GenerateNewMachine()
    {
      if (this.TryGetComponent(out IStatesManagerFactory states))
      {
        return states.CreateNewStatesManager();
      }

      var machine = new StatesManagerBase();
      return machine;
    }
  }
}