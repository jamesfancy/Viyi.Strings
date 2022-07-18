namespace Viyi.Strings.Codec.Io;

public interface ICodecTextReader {
    int Read(char[] buffer, int start, int count);
    int Read(char[] buffer);
    string? ReadLine();
    string ReadAll();
}
