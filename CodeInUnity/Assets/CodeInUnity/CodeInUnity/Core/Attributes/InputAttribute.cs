using System;

namespace CodeInUnity.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InputAttribute : Attribute
    {
        public string name = null;
    }
}
