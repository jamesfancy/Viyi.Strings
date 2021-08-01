using System;
using System.IO;
using System.Text;
using System.Linq;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Io
{
    public class CodecFilterableTextReader : CodecTextReader
    {
        readonly Func<char, bool> filter;

        public CodecFilterableTextReader(
            TextReader reader,
            CodecOptions options,
            Func<char, bool> filter)
            : base(reader, options)
        {
            this.filter = filter;
        }

        public override int Read(char[] buffer, int start, int count)
        {
            int validCount = 0;
            while (validCount == 0)
            {
                var readCount = Reader.Read(buffer, start, count);
                if (readCount == 0) { return 0; }

                // 如果过滤后的结果是 0，需要再次读取，直的到结束或有数据为止
                validCount = FilterCharsBuffer(filter, buffer, start, readCount);
            }
            return validCount;
        }

        public override int Read(char[] buffer)
        {
            return Read(buffer, 0, buffer.Length);
        }

        public override string? ReadLine()
        {
            var line = Reader.ReadLine();
            if (line == null) { return null; }
            return FilterCharsInString(filter, line);
        }

        public override string ReadAll()
        {
            return FilterCharsInString(filter, Reader.ReadToEnd())!;
        }

        private static string FilterCharsInString(Func<char, bool> filter, string s)
        {
            if (s.All(filter)) { return s; }

            const int THRESHOLD = 1024;

            if (s.Length < THRESHOLD)
            {
                char[] buffer = s.ToCharArray();
                int count = FilterCharsBuffer(filter, buffer, 0, buffer.Length);
                return new string(buffer, 0, count);
            }
            else
            {
                StringBuilder builder = new StringBuilder(THRESHOLD);
                char[] buffer = new char[THRESHOLD];
                for (int i = 0; i < s.Length; i += THRESHOLD)
                {
                    var length = Math.Min(THRESHOLD, s.Length - i);
                    s.CopyTo(i, buffer, 0, length);
                    var count = FilterCharsBuffer(filter, buffer, 0, length);
                    builder.Append(buffer, 0, count);
                }
                return builder.ToString();
            }
        }

        private static int FilterCharsBuffer(
            Func<char, bool> filter,
            char[] buffer, int start, int length)
        {
            // 记录已经越过的连续有效字符数
            int validCount = 0;
            // 记录已经越过的连续无效字符数
            int invalidCount = 0;
            // 已核对指针，在此之前的所有缓存区数据均为有效
            int finishIndex = start;

            for (var i = start; i < length; i++)
            {
                if (filter(buffer[i]))
                {
                    // 有效字符只需要计数
                    validCount++;
                }
                else
                {
                    // 遇到无效字符需要判断是否需要核实越过的有效字符，
                    // 根据情况往前挪
                    if (validCount > 0)
                    {
                        collateValidChars();
                    }
                    invalidCount++;
                }
            }

            // 如果最后还有未核对的有效字符，进行一次核对
            if (validCount > 0) { collateValidChars(); }
            return finishIndex - start;

            void collateValidChars()
            {
                // 只有在遇到无效字符的时候才需要对之前的区域进行处理。
                // 此时 validCount 记录了之前的有效字符区域大小。
                // 检查，❶ 如果存在无效区域记录，说明需要移动，
                // 并将核对指针 finishCount 记录在移动后的有效数据末位置，
                // 移动后无效空间后移与后面的无效空间连接，
                // 但大小不变，所以 invalidCount 不变；
                // ❷ 如果不存在无效区域记录，即 invalidCount == 0，
                // 直接改变已核对指针 finishCount

                // 如果有无效区域，进行移动。移动会改变已完成指针
                if (invalidCount > 0)
                {
                    Array.Copy(
                        buffer, finishIndex + invalidCount,
                        buffer, finishIndex,
                        validCount);
                    finishIndex += validCount;
                }
                else
                {
                    finishIndex = validCount;
                }

                // 处理后需要重置 validCount，因为已经核对过了
                validCount = 0;
            }
        }
    }
}
