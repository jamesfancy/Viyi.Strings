namespace Viyi.Strings.Codec.Io;

public interface ICodecTextWriter {
    void Write(char[] buffer);
    void Write(char[] buffer, int start, int length);
    void Write(string segment);
}
