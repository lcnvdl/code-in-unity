using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static bool FastStartsWith(this string a, string b)
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
    }
}
