using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInUnity.Core.Utils
{
    public static class MathUtils
    {
        public static int Fibbonacci(int number)
        {
            if (number == 0)
            {
                return 0;
            }

            return number + Fibbonacci(number - 1);
        }
    }
}