using System;
using System.Collections.Generic;
using System.IO;
using Viyi.Strings.Codec.Abstract;

namespace Viyi.Strings.Codec
{
    public class HexCodec : CodecBase
    {
        static readonly char[] HexChars =
        {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        };

        static readonly char[] LowerHexChars =
        {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'
        };

        static readonly int[] ReverseHexCode =
        {                                   // ASCII offset 48
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9,   // ASCII 48 ~ 57 (count 10)
            0, 0, 0, 0, 0, 0, 0,            // ASCII 58 ~ 64 (count 7)
            10, 11, 12, 13, 14, 15,         // ASCII 65 ~ 70 (count 6)
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,   // ASCII 71 ~ 80
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,   // ASCII 81 ~ 90
            0, 0, 0, 0, 0, 0,               // ASCII 91 ~ 96
            10, 11, 12, 13, 14, 15          // ASCII 97 ~ 102
        };

        public override void Decode(Stream outStream, IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                // 对每行进行分段处理，非 HEX 字符作为分隔符。
                // 每段文本长度如果是奇数，前面补 0 凑成偶数
                int start = 0;
                int end = -1;
                while (end < line.Length)
                {
                    start = Helper.FindHexChar(line, end + 1);
                    if (start < 0)
                    {
                        start = end + 1;
                        end = line.Length;
                        Helper.DecodeSegment(outStream, line, start, end);
                        break;
                    }

                    end = Helper.FindNotHexChar(line, start + 1);
                    if (end < 0)
                    {
                        end = line.Length;
                    }
                    Helper.DecodeSegment(outStream, line, start, end);
                }
            }
        }

        protected override void Encode(ICharWriter writer, IEnumerable<byte[]> data)
        {
            char[] hexChars = Settings.Global.IsUpperCaseInHexadecimal
                ? HexChars
                : LowerHexChars;

            foreach (var bytes in data)
            {
                foreach (var b in bytes)
                {
                    writer.Write(hexChars[b >> 4]);
                    writer.Write(hexChars[b & 0x0f]);
                }
            }
        }

        static bool IsValidArguments(Array array, int start, ref int length)
        {
            if (array == null) { return false; }
            if (start < 0 || length < 0) { return false; }
            if (start > array.Length) { return false; }

            if (start + length > array.Length) { length = array.Length - start; }
            return true;
        }

        static class Helper
        {
            internal static void DecodeSegment(Stream outStream, string line, int start, int end)
            {
                var length = end - start;
                if (length < 1)
                {
                    return;
                }

                int v = length % 2 == 0
                    ? ReverseHexCode[line[start++] - 48] << 4 | ReverseHexCode[line[start++] - 48]
                    : ReverseHexCode[line[start++] - 48] & 0x0f;
                outStream.WriteByte((byte)v);

                for (var i = start; i < end;)
                {
                    v = ReverseHexCode[line[i++] - 48] << 4 | ReverseHexCode[line[i++] - 48];
                    outStream.WriteByte((byte)v);
                }
            }

            internal static int FindNotHexChar(string s, int start = 0)
            {
                for (var i = start; i < s.Length; i++)
                {
                    var c = s[i];
                    if (c < '0' || c > 'f' || c > '9' && c < 'A' || c > 'F' && c < 'a')
                    {
                        return i;
                    }
                }
                return -1;
            }

            internal static int FindHexChar(string s, int start = 0)
            {
                for (var i = start; i < s.Length; i++)
                {
                    var c = s[i];
                    if (c >= '0' && c <= '9' || c >= 'A' && c <= 'F' || c >= 'a' && c <= 'f')
                    {
                        return i;
                    }
                }
                return -1;
            }

        }
    }
}
