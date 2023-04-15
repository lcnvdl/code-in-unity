using CodeInUnity.StateMachine;
using UnityEngine;

public class StateMachineScript : MonoBehaviour
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

    protected virtual void Update()
    {
        machine.Update(Time.deltaTime);
    }

    protected virtual StatesManagerBase GenerateNewMachine()
    {
        var machine = new StatesManagerBase();
        return machine;
    }
}
