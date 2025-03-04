namespace CodeInUnity.Interfaces
{
  public interface ICloneable<T> where T : class
  {
    T Clone();
  }
}
