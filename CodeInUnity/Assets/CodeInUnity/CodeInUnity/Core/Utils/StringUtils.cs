using System.Linq;

namespace CodeInUnity.Core.Utils
{
  public static class StringUtils
  {
    /// <remarks>
    /// Recommended by Unity Team. 
    /// https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
    /// </remarks>
    public static bool FastEndsWith(string a, string b)
    {
      int ap = a.Length - 1;
      int bp = b.Length - 1;

      while (ap >= 0 && bp >= 0 && a[ap] == b[bp])
      {
        ap--;
        bp--;
      }

      return (bp < 0);
    }

    /// <remarks>
    /// Recommended by Unity Team. 
    /// https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
    /// </remarks>
    public static bool FastStartsWith(string a, string b)
    {
      int aLen = a.Length;
      int bLen = b.Length;

      int ap = 0; int bp = 0;

      while (ap < aLen && bp < bLen && a[ap] == b[bp])
      {
        ap++;
        bp++;
      }

      return (bp == bLen);
    }

    public static bool ContainsAnyUppercaseCharacter(string text)
    {
      int count = text.Length;

      for (int i = 0; i < count; i++)
      {
        if (char.IsUpper(text[i]))
        {
          return true;
        }
      }

      return false;
    }

    public static int CountOccurences(string text, char c)
    {
      int len = text.Length;
      int count = 0;

      for (int i = 0; i < len; i++)
      {
        if (text[i].Equals(c))
        {
          count++;
        }
      }

      return count;
    }

    public static string Decapitalize(string word)
    {
      if (word == null || word.Length == 0)
      {
        return word;
      }

      if (word.Length == 1)
      {
        return word.ToLower();
      }

      return word.Remove(1).ToLower() + word.Substring(1);
    }

    public static string DecapitalizeAllWords(string word)
    {
      if (!word.Contains(' '))
      {
        return Decapitalize(word);
      }

      return string.Join(" ", word.Split(' ').Select(m => Decapitalize(m.Trim())));
    }

    public static string Capitalize(string word, bool tailToLowerCase)
    {
      if (word == null || word.Length == 0)
      {
        return word;
      }

      if (word.Length == 1)
      {
        return word.ToUpper();
      }

      return word.Remove(1).ToUpper() + (tailToLowerCase ? word.Substring(1).ToLower() : word.Substring(1));
    }

    public static bool IsOnlyDigits(string input)
    {
      foreach (char c in input)
      {
        if (!char.IsDigit(c))
        {
          return false;
        }
      }

      return true;
    }
  }
}