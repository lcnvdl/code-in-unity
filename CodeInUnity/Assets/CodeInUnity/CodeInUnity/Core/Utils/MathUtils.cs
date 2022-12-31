using System.Linq;

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

        public static float Media(params float[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                return 0;
            }

            return numbers.Sum() / numbers.Length;
        }

        public static float NormalizeAngle180(float angle)
        {
            while (angle > 180)
            {
                angle -= 360;
            }

            while (angle < -180)
            {
                angle += 360;
            }

            return angle;
        }

        public static float NormalizeAngle(float angle)
        {
            while (angle > 360)
            {
                angle -= 360;
            }

            while (angle < 0)
            {
                angle += 360;
            }

            return angle;
        }
    }
}