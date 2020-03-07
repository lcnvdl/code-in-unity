using System.IO;
using System.Reflection;

namespace CodeInUnity.Testing.Helpers.IO
{
    public static class EmbeddedFileHelper
    {
        public static string Read(Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}