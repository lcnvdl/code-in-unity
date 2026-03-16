namespace CodeInUnity.StateMachine
{
  public static class StateUtils
  {
    public static string GetDefaultIdentifier<T>() where T : BaseState, new()
    {
      return new T().identifier;
    }
  }
}
