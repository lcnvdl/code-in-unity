using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInUnity.Core.Exceptions
{
    [Serializable]
    public class SerializableError
    {
        public string type;
        public string message;
        public string stackTrace;
        public bool fatal;

        public SerializableError()
        {
        }

        public SerializableError(Exception ex)
            : this(ex, false)
        {
        }

        public SerializableError(Exception ex, bool fatal)
        {
            this.type = ex.GetType().Name;
            this.message = ex.Message;
            this.stackTrace = ex.StackTrace;
            this.fatal = fatal;
        }
    }
}
