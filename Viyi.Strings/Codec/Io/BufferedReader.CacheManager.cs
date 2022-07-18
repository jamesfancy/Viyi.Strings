using System.Diagnostics;

namespace Viyi.Strings.Codec.Io;

partial class BufferedReader {
    // 管理 BufferedReader 的内部缓存区
    sealed class CacheManager {
        // 缓存空间
        readonly char[] cache;

        // cacheOffset 记录缓存中有效数据起始位置，cacheCount 记录缓存中有效数据长度
        // 使用这两个变量主要是为了避免数据在数组中的移动
        int cacheOffset = 0;
        int cacheCount = 0;

        public bool HasMore => cacheCount > 0;

        public CacheManager(int capacity = DefaultCapacity) {
            cache = new char[capacity];
        }

        public int FromReader(ICodecTextReader reader) {
            // 每次从 reader 读取数据都应该是缓冲区空的时候（外部逻辑保证）
            Debug.Assert(cacheCount == 0 && cacheOffset == 0);
            int count = reader.Read(cache);
            AddChars(count);
            return count;
        }

        public int ToWriter(BufferWriter writer) {
            int writeCount = writer.WriteFromCache(cache, cacheOffset, cacheCount);
            UseChars(writeCount);
            return writeCount;
        }

        void AddChars(int count) {
            cacheCount += count;
        }

        void UseChars(int count) {
            cacheCount -= count;
            cacheOffset = cacheCount <= 0 ? 0 : cacheOffset + count;
        }
    }
}
