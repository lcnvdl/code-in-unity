using System.Reflection;

namespace CodeInUnity.Core.Utils
{
    public static class GameObjectUtils
    {
        private static MethodInfo findObjectFromInstanceIDMethod;

        private static MethodInfo FindObjectFromInstanceIDMethod
        {
            get
            {
                if (findObjectFromInstanceIDMethod == null)
                {
                    findObjectFromInstanceIDMethod = typeof(UnityEngine.Object)
                        .GetMethod("FindObjectFromInstanceID", BindingFlags.NonPublic | BindingFlags.Static);
                }

                return findObjectFromInstanceIDMethod;
            }
        }

        public static UnityEngine.Object FindObjectFromInstanceID(int iid)
        {
            return (UnityEngine.Object)FindObjectFromInstanceIDMethod.Invoke(null, new object[] { iid });
        }
    }
}
