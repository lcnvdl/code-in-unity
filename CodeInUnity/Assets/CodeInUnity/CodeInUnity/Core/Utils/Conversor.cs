using System.Globalization;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
    public static class Conversor
    {
        private const string Units = "KM";

        public static string NumberToK(float n, int maxDecimals = 2)
        {
            int i = 0;

            while (i < Units.Length && n >= 1000)
            {
                n /= 1000f;
                i++;
            }

            string unit = (i == 0) ? string.Empty : Units[i - 1].ToString();

            if (maxDecimals == 0)
            {
                n = Mathf.Floor(n);
            }
            else
            {
                float d = Mathf.Pow(10, maxDecimals);
                n = Mathf.Floor(n * d) / d;
            }

            if (maxDecimals == 0)
            {
                return string.Format(CultureInfo.InvariantCulture, "{0:0}{1}", n, unit);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, "{0:0." + string.Empty.PadRight(maxDecimals, '#') + "}{1}", n, unit);
            }
        }
    }
}
