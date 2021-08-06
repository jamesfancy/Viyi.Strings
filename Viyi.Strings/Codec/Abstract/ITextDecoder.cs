using System.IO;

namespace Viyi.Strings.Codec.Abstract
{
    public interface ITextDecoder
    {
        byte[] Decode(string codes);
        void Decode(Stream output, TextReader input);
    }
}
