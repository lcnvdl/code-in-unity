using System.Text;

namespace CodeInUnity.Core.Utils
{
  public class StringBuilderUtils
  {
    public static int CountOccurences(StringBuilder sb, char c)
    {
      int len = sb.Length;
      int count = 0;

      for (int i = 0; i < len; i++)
      {
        if (sb[i].Equals(c))
        {
          count++;
        }
      }

      return count;
    }
  }
}
