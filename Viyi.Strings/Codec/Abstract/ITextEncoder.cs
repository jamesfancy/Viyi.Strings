using System.IO;

namespace Viyi.Strings.Codec.Abstract;

public interface ITextEncoder {
    string Encode(byte[] data);
    string Encode(byte[] data, int start, int count);
    void Encode(TextWriter output, Stream input);
}
