using System;

namespace Viyi.Strings.Codec.Io;

partial class BufferedReader {
    /// <summary>
    /// 管理需要写入数据的缓冲区
    /// </summary>
    sealed class BufferWriter {
        readonly char[] buffer;
        int offset;
        int rest;
        int writeCount = 0;

        public bool Full => rest == 0;
        public int WriteCount => writeCount;

        public BufferWriter(char[] buffer, int start, int count) {
            this.buffer = buffer;
            offset = start < 0 ? 0 : Math.Min(start, buffer.Length - 1);
            rest = Math.Min(count, buffer.Length - start);
        }

        public int WriteFromCache(char[] cache, int start, int count) {
            var writeCount = Math.Min(rest, count);
            Array.Copy(cache, start, buffer, offset, writeCount);
            Forward(writeCount);
            return writeCount;
        }

        void Forward(int count) {
            offset += count;
            writeCount += count;
            rest -= count;
        }
    }
}
