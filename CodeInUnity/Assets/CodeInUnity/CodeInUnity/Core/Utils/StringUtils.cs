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
    }
}