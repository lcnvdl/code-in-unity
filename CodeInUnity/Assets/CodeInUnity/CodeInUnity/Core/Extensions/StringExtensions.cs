using CodeInUnity.Core.Utils;

namespace CodeInUnity.Extensions
{
    public static class StringExtensions
    {
        /// <remarks>
        /// Recommended by Unity Team. 
        /// https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
        /// </remarks>
        public static bool FastEndsWith(this string a, string b)
        {
            return StringUtils.FastEndsWith(a, b);
        }

        /// <remarks>
        /// Recommended by Unity Team. 
        /// https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
        /// </remarks>
        public static bool FastStartsWith(this string a, string b)
        {
            return StringUtils.FastStartsWith(a, b);
        }
    }
}
