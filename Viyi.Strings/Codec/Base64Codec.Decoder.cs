using System;
using System.Collections.Generic;
using System.IO;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Io;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec
{
    public partial class Base64Codec
    {
        private static class DecodeHelper
        {
            public const int Offset = 43;
            public static readonly byte[] Codes =
            {                                               // ASCII offset 43
                62, 0xff, 0xff, 0xff, 63,                   // [+~/] 43~47 (count 5)
                52, 53, 54, 55, 56, 57, 58, 59, 60, 61,     // [0~9] 48~57 (count 10)
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,   // ASCII 58~64 (count 7)
                0, 1, 2, 3, 4, 5, 6,                        // [A~G] 65~71 (count 7)
                7, 8, 9, 10, 11, 12, 13,                    // [H~N] 72~78 (count 7)
                14, 15, 16, 17, 18, 19,                     // [O~T] 79~84 (count 6)
                20, 21, 22, 23, 24, 25,                     // [U~Z] 85~90 (count 6) 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff,         // ASCII 91~96
                26, 27, 28, 29, 30, 31, 32,                 // [a~g] 97~103 (count 7)
                33, 34, 35, 36, 37, 38, 39,                 // [h~n] 104~110 (count 7)
                40, 41, 42, 43, 44, 45,                     // [o~t] 111~116 (count 6)
                46, 47, 48, 49, 50, 51,                     // [u~z] 117~122 (count 6) 
            };

            public static bool IsValidChar(char ch) => ch < 43 || ch > 122
                ? false
                : Map(ch) != 0xff;

            public static byte Map(char ch) => Codes[ch - Offset];
        }

        sealed class Decoder : TextDecoder
        {
            //static bool IsValidChar(char ch) => ReverseCodes.IsValidChar(ch);

            const int bufferSize = 4;
            readonly Stream stream;
            readonly char[] buffer = new char[bufferSize];
            int bufferIndex;

            internal Decoder(Stream outStream, CodecOptions options)
                : base(options)
            {
                stream = outStream;
            }

            internal void Decode(IEnumerable<string> lines)
            {
                foreach (var line in lines)
                {
                    Decode(line);
                }

                switch (bufferIndex)
                {
                    case 1:
                        throw new FormatException("input is not complate base64 text");
                    case 2:
                        Decode(buffer[0], buffer[1]);
                        break;
                    case 3:
                        Decode(buffer[0], buffer[1], buffer[2]);
                        break;
                }
            }

            protected override ICodecTextReader WrapReader(TextReader reader) =>
                new CodecFilterableTextReader(reader, Options, DecodeHelper.IsValidChar);


            void Decode(string line)
            {
                // 本方法的详细注释参考 `Encoder.Encode(byte[])`

                if (string.IsNullOrEmpty(line))
                {
                    return;
                }

                foreach (var ch in line)
                {
                    if (!IsValidChar(ch))
                    {
                        continue;
                    }

                    buffer[bufferIndex++] = ch;
                    if (bufferIndex >= bufferSize)
                    {
                        DecodeBuffer();
                        bufferIndex = 0;
                    }
                }
            }

            void DecodeBuffer()
            {
                int v = reverseBase64Code[buffer[0] - reverseBase64CodeOffset] << 18
                    | reverseBase64Code[buffer[1] - reverseBase64CodeOffset] << 12
                    | reverseBase64Code[buffer[2] - reverseBase64CodeOffset] << 6
                    | reverseBase64Code[buffer[3] - reverseBase64CodeOffset];
                stream.WriteByte((byte)(v >> 16));
                stream.WriteByte((byte)(v >> 8 & 0xff));
                stream.WriteByte((byte)(v & 0xff));
            }

            void Decode(char ch1, char ch2)
            {
                stream.WriteByte((byte)
                    (reverseBase64Code[ch1 - reverseBase64CodeOffset] << 2
                    | reverseBase64Code[ch2 - reverseBase64CodeOffset] >> 4));
            }

            void Decode(char ch1, char ch2, char ch3)
            {
                int v = reverseBase64Code[ch1 - reverseBase64CodeOffset] << 10
                    | reverseBase64Code[ch2 - reverseBase64CodeOffset] << 4
                    | reverseBase64Code[ch3 - reverseBase64CodeOffset] >> 2;
                stream.WriteByte((byte)(v >> 8));
                stream.WriteByte((byte)(v & 0xff));
            }

            protected override void Decode(Stream output, ICodecTextReader reader)
            {
                //reader.ReadLine
            }
        }
    }
}
