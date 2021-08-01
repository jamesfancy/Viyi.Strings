using System;
using System.Runtime.Serialization;

namespace Viyi.Strings.Codec
{
    public class CodecException : Exception
    {
        public CodecException() { }

        public CodecException(string message) : base(message) { }

        public CodecException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
