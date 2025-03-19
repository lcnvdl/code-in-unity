namespace CodeInUnity.Interfaces
{
  public interface ISmoothTask
  {
    string TaskGuid { get; }

    bool NextTick();
  }
}
