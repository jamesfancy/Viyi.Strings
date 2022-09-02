using System;
using System.IO;

namespace Viyi.Strings.Codec.Io;
/// <summary>
/// 内建缓存，保证在调用 Read 方法时每次都能把传入的缓冲区写满（按指定的有效空间），
/// 除非数据中的数据已经被读完。
/// </summary>
public partial class BufferedReader {
    const int DefaultCapacity = 4096;
    readonly ICodecTextReader reader;

    readonly CacheManager cache;

    /// <summary></summary>
    /// <param name="reader"></param>
    /// <param name="capacity">指定缓冲区大小。可选，默认大小为 4KB</param>
    public BufferedReader(ICodecTextReader reader, int capacity = DefaultCapacity) {
        this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        cache = new CacheManager(capacity);
    }

    /// <summary></summary>
    /// <param name="reader"></param>
    /// <param name="capacity">指定缓冲区大小。可选，默认大小为 4KB</param>
    public BufferedReader(TextReader reader, int capacity = DefaultCapacity)
        : this(new CodecTextReader(reader), capacity) { }

    public int Read(char[] buffer) => Read(buffer, 0, buffer.Length);

    public int Read(char[] buffer, int start, int count) {
        CheckParameters(buffer.Length, start, count);
        var bufferWriter = new BufferWriter(buffer, start, count);

        while (true) {
            if (cache.HasMore) {
                cache.ToWriter(bufferWriter);
                if (bufferWriter.Full) {
                    // 出口 1：写满 buffer
                    break;
                }
            }

            int readCount = cache.FromReader(reader);
            if (readCount == 0) {
                // 出口 2：读完数据源
                break;
            }
        }

        return bufferWriter.WriteCount;
    }

    private static void CheckParameters(int bufferSize, int start, int count) {
        if (start < 0 || count < 0) {
            throw new ArgumentOutOfRangeException();
        }

        if (start + count > bufferSize) {
            throw new ArgumentException("buffer length is not enough for 'count'");
        }
    }
}
