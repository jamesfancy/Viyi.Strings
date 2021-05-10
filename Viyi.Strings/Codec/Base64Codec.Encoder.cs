using System.Collections.Generic;

namespace Viyi.Strings.Codec
{
    public partial class Base64Codec
    {
        sealed class Encoder
        {
            const int bufferSize = 3;
            readonly ICharWriter writer;
            readonly byte[] buffer = new byte[bufferSize];
            int bufferIndex;

            internal Encoder(ICharWriter writer)
            {
                this.writer = writer;
            }

            internal void Encode(IEnumerable<byte[]> data)
            {
                foreach (var segment in data)
                {
                    Encode(segment);
                }

                // 如果 bufferIndex 大于 0（必定小于 3），需要收尾
                switch (bufferIndex)
                {
                    case 1:
                        EncodeLast(buffer[0]);
                        break;
                    case 2:
                        EncodeLast(buffer[0], buffer[1]);
                        break;
                }
            }

            void Encode(byte[] segment)
            {
                if (segment == null || segment.Length == 0)
                {
                    return;
                }

                // i 指示在 segment 中的位置，贯穿始终
                int i = 0;

                // 如果上个 segment 没处理完，bufferIndex 表示上次剩下的字节数
                // 这里需要先将 buffer 填满
                if (bufferIndex > 0)
                {
                    while (bufferIndex < bufferSize && i < segment.Length)
                    {
                        buffer[bufferIndex++] = segment[i++];
                    }

                    // 如果 segment 太小，仍然没把上个 segment 留下的 buffer 填满，
                    // 只好等下一个 segment 来接着填
                    if (bufferIndex < bufferSize)
                    {
                        return;
                    }

                    // 填满了 buffer，Encode 处理掉，再将 bufferIndex 标记为 0，表示当前无 buffer
                    EncodeMiddle(buffer);
                    bufferIndex = 0;
                }

                // 因为每次都是处理 3 个字节，所以最后一个起始位置一定是至少比字节组长度少 3
                int lastStart = segment.Length - bufferSize;
                for (; i <= lastStart; i += bufferSize)
                {
                    EncodeMiddle(segment, i);
                }

                // 如果 i 没达到 segment.Length，说明还有不足 3 个字节需要处理
                // 存入 buffer 中
                for (; i < segment.Length; i++)
                {
                    buffer[bufferIndex++] = segment[i];
                }
            }

            void EncodeMiddle(byte[] data, int start = 0)
            {
                EncodeMiddle(data[start], data[start + 1], data[start + 2]);
            }

            void EncodeMiddle(byte b1, byte b2, byte b3)
            {
                int combo = b1 << 16 | b2 << 8 | b3;
                writer.Write(base64Chars[combo >> 18]);
                writer.Write(base64Chars[combo >> 12 & 0x3f]);
                writer.Write(base64Chars[combo >> 6 & 0x3f]);
                writer.Write(base64Chars[combo & 0x3f]);
            }

            void EncodeLast(byte b1, byte b2)
            {
                int combo = b1 << 8 | b2;
                writer.Write(base64Chars[combo >> 10]);
                writer.Write(base64Chars[combo >> 4 & 0x3f]);
                writer.Write(base64Chars[combo << 2 & 0x3f]);
                writer.Write(base64Chars[64]);
                writer.Flush();
            }

            void EncodeLast(byte b1)
            {
                writer.Write(base64Chars[b1 >> 2]);
                writer.Write(base64Chars[b1 << 4 & 0x3f]);
                writer.Write(base64Chars[64]);
                writer.Write(base64Chars[64]);
                writer.Flush();
            }
        }
    }
}
