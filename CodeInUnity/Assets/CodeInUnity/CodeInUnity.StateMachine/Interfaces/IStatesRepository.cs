namespace CodeInUnity.StateMachine.Interfaces
{
  public interface IStatesRepository
  {
    AnyState GetRootState();

    BaseState GetState(string id);
  }

  public interface IWriteableStatesRepository
  {
    void OverrideState(BaseState state);
  }
}