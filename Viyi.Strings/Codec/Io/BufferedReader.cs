using System;
using System.Diagnostics;

namespace Viyi.Strings.Codec.Io
{
    class BufferedReader
    {
        const int DefaultCapacity = 4096;
        readonly ICodecTextReader reader;
        readonly int capacity;
        readonly char[] cache;
        int cacheOffset = 0;
        int cacheCount = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="bufferSize">指定缓冲区大小。可选，默认大小为 4K</param>
        public BufferedReader(ICodecTextReader reader, int bufferSize = DefaultCapacity)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            capacity = bufferSize;
            cache = new char[capacity];
        }

        public int Read(char[] buffer) => Read(buffer, 0, buffer.Length);

        public int Read(char[] buffer, int start, int count)
        {
            CheckParameters(buffer.Length, start, count);
            var bufferWriter = new BufferWriter(buffer, start, count);

            // ❶ 如果缓存中有数据，先从缓存中获取数据
            transferFromCacheToBuffer();
            if (bufferWriter.Full) { return count; }

            // 如果未填满 buffer，则表示缓存数据已用完
            Debug.Assert(cacheCount == 0 && cacheOffset == 0);

            // ❷ 开始从 Reader 中获取数据直到剩余空间小于
            while (bufferWriter.Rest >= capacity)
            {
                bufferWriter.WriteBy(reader.Read);
            }
            if (bufferWriter.Full) { return count; }
            // 第 2 阶段完成仍然未填满 buffer，说明剩余空间小于 cache 空间

            // ❸ 现在要先读到 cache，再拷贝到 buffer，
            // 因为 buffer 剩余空间越来越小，但 cache 足够大，
            // 先读取 cache 可以减少读取次数
            while (bufferWriter.Rest > 0)
            {
                var readCount = reader.Read(cache);
                if (readCount == 0)
                {
                    // reader 中的数据已经读完了
                    return count - bufferWriter.Rest;
                }

                cacheCount = readCount;
                transferFromCacheToBuffer();
            }

            // 循环正常结束说明 buffer 填满了
            return count;

            void transferFromCacheToBuffer()
            {
                if (cacheCount <= 0) { return; }

                var usedCount = bufferWriter.WriteFromCache(cache, cacheOffset, cacheCount);
                cacheCount -= usedCount;
                cacheOffset = cacheCount == 0 ? 0 : cacheOffset + usedCount;
            }
        }

        private static void CheckParameters(int bufferSize, int start, int count)
        {
            if (start < 0 || count < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (start + count > bufferSize)
            {
                throw new ArgumentException("buffer length is not enought to 'count'");
            }
        }

        sealed class BufferWriter
        {
            readonly char[] buffer;
            int offset;
            int rest;

            public int Rest => rest;
            public bool Full => rest == 0;

            public BufferWriter(char[] buffer, int start, int count)
            {
                this.buffer = buffer;
                offset = start;
                rest = count;
            }

            public int WriteBy(Func<char[], int, int, int> read)
            {
                int count = read(buffer, offset, rest);
                Forward(count);
                return count;
            }

            public int WriteFromCache(char[] cache, int start, int count)
            {
                var writeCount = Math.Min(rest, count);
                Array.Copy(cache, start, buffer, offset, writeCount);
                Forward(writeCount);
                return writeCount;
            }

            void Forward(int count)
            {
                offset += count;
                rest -= count;
            }
        }
    }
}
