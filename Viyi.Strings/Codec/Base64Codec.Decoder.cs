using System;
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
            public static readonly int[] Codes =
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
                : CharToInt(ch) != 0xff;

            public static int CharToInt(char ch) => Codes[ch - Offset];
        }

        sealed class Decoder : TextDecoder
        {
            const int BufferLength = 1024;

            readonly char[] buffer = new char[BufferLength];
            int offset = 0;
            private int rest => BufferLength - offset;

            Stream? output;

            internal Decoder(CodecOptions options)
                : base(options) { }

            protected override ICodecTextReader WrapReader(TextReader reader) =>
                new CodecFilterableTextReader(reader, Options, DecodeHelper.IsValidChar);

            protected override void Decode(Stream output, ICodecTextReader codecReader)
            {
                this.output = output;
                var reader = new BufferedReader(codecReader);

                int count;
                while ((count = reader.Read(buffer, offset, rest)) > 0)
                {
                    DecodeBuffer(count);
                }

                switch (offset)
                {
                    case 3:
                        DecodeLast3();
                        break;
                    case 2:
                        DecodeLast2();
                        break;
                    default:
                        throw new InvalidDataException("invalid base64 data length");
                }
            }

            void DecodeBuffer(int count)
            {
                var length = offset + count;
                if (length < 4) { return; }
                var rest = length % 4;

                for (var i = 0; i < length; i += 4)
                {
                    Decode(i);
                }

                if (rest > 0)
                {
                    Array.Copy(buffer, length - rest, buffer, 0, rest);
                    offset = rest;
                }
            }

            void Decode(int start)
            {
                int v = DecodeHelper.CharToInt(buffer[start]) << 18
                    | DecodeHelper.CharToInt(buffer[start + 1]) << 12
                    | DecodeHelper.CharToInt(buffer[start + 2]) << 6
                    | DecodeHelper.CharToInt(buffer[start + 3]);

                output!.WriteByte((byte)(v >> 16));
                output.WriteByte((byte)(v >> 8 & 0xff));
                output.WriteByte((byte)(v & 0xff));
            }

            void DecodeLast2()
            {
                output!.WriteByte((byte)(
                    DecodeHelper.CharToInt(this.buffer[0]) << 2
                    | DecodeHelper.CharToInt(this.buffer[1]) >> 4
                ));
            }

            void DecodeLast3()
            {
                int v = DecodeHelper.CharToInt(buffer[0]) << 10
                    | DecodeHelper.CharToInt(buffer[1]) << 4
                    | DecodeHelper.CharToInt(buffer[2]) >> 2;
                output!.WriteByte((byte)(v >> 8));
                output.WriteByte((byte)(v & 0xff));
            }
        }
    }
}
