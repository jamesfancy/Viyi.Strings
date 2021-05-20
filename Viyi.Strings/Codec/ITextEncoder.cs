using System;
using System.IO;

namespace Viyi.Strings.Codec
{
    public interface ITextEncoder
    {
        string Encode(byte[] data);
        string Encode(byte[] data, int start, int count);
        void Encode(TextWriter output, Stream input);
    }
}
