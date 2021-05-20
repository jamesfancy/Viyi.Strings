using System.IO;

namespace Viyi.Strings.Codec
{
    public interface ITextDecoder
    {
        byte[] Decode(string codes);
        void Decode(Stream output, TextReader input);
    }
}
