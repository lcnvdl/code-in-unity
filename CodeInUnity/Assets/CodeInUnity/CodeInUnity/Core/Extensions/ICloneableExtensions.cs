using CodeInUnity.Interfaces;

namespace CodeInUnity.Extensions
{
  public static class ICloneableExtensions
  {
    public static T[] CloneArrayOfCloneables<X, T>(this X self, T[] array)
      where X : class, ICloneable<X>
      where T : class, ICloneable<T>
    {
      if (array == null)
      {
        return new T[0];
      }

      int length = array.Length;

      if (length == 0)
      {
        return new T[0];
      }

      var result = new T[length];
      for (int i = 0; i < length; i++)
      {
        result[i] = array[i].Clone();
      }

      return result;
    }
  }
}
