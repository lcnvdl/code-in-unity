@startuml
!theme reddress-darkorange

package UnityEngine {
  class MonoBehaviour
}

package StateMachine {
  class StateMachineScript extends UnityEngine.MonoBehaviour
   
  abstract class BaseState
  
  class AnyState <<sealed>> extends BaseState
  
  interface IStatesManagerFactory {
    StatesManagerBase CreateNewStatesManager();
  }
  
  interface IStatesRepository {
    AnyState GetRootState()
    BaseState GetState(String id)
  }
    
  interface IWriteableStatesRepository
  {
      void OverrideState(BaseState state);
  }

  abstract class StatesManagerBase
  
  abstract class StatesRepositoryBaseScript extends UnityEngine.MonoBehaviour implements IStatesRepository
  
  StateMachineScript *-- StatesManagerBase
  StateMachineScript - IStatesManagerFactory : CreateNewStatesManager(): StatesManagerBase >
  StateMachineScript - StatesManagerBase : Initialize() >
  StateMachineScript - StatesManagerBase : Update() >
  StatesManagerBase - StatesRepositoryBaseScript : GetState() >
  StatesManagerBase - StatesRepositoryBaseScript : GetRootState() >
  StatesRepositoryBaseScript *-- BaseState
}

package Example {
  class IdleState extends StateMachine.BaseState
  class WalkState extends StateMachine.BaseState
  class WalkableStatesManager extends StateMachine.StatesManagerBase 
  class WalkableStatesManagerFactory extends UnityEngine.MonoBehaviour implements StateMachine.IStatesManagerFactory 
  class WalkableStatesRepository extends StateMachine.StatesRepositoryBaseScript
  
  WalkableStatesManagerFactory - WalkableStatesManager : new() >
  WalkableStatesRepository *-- IdleState
  WalkableStatesRepository *-- WalkState
}

@enduml
