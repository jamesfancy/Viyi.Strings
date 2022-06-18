using System.IO;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Io;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base16 {
    internal class Base16Decoder : TextDecoder {
        static readonly int[] ReverseHexCodes = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9,           // ASCII 48 ~ 57 (count 10)
            -1, -1, -1, -1, -1, -1, -1,             // ASCII 58 ~ 64 (count 7)
            10, 11, 12, 13, 14, 15,                 // ASCII 65 ~ 70 (count 6)
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // ASCII 71 ~ 80
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // ASCII 81 ~ 90
            -1, -1, -1, -1, -1, -1,                 // ASCII 91 ~ 96
            10, 11, 12, 13, 14, 15                  // ASCII 97 ~ 102
        };

        static bool IsValidChar(char ch) =>
            ch >= 48 && ch <= 102 && ReverseHexCodes[ch] != -1;

        public Base16Decoder(CodecOptions options) : base(options) { }

        protected override ICodecTextReader WrapReader(TextReader reader) {
            return new CodecFilterableTextReader(reader, Options, IsValidChar);
        }

        protected override void Decode(Stream output, ICodecTextReader codecReader) {
            var reader = new BufferedReader(codecReader);
            var writer = new BufferedStream(output);
            var chars = new char[2];

            while (reader.Read(chars) == 2) {
                writer.WriteByte(
                    (byte) (ReverseHexCodes[chars[0]] << 4
                    | ReverseHexCodes[chars[1]])
                );
            }
            writer.Flush();
        }
    }
}
