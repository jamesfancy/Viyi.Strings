using System.IO;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Io;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base16 {
    class Base16Encoder : TextEncoder {
        static readonly char[] UpperHexChars = {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        };

        static readonly char[] LowerHexChars = {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'
        };

        readonly char[] hexChars;

        public Base16Encoder(CodecOptions options) : base(options) {
            hexChars = options.UpperCase ? UpperHexChars : LowerHexChars;
        }

        protected override void Encode(ICodecTextWriter writer, Stream input) {
            BufferedStream bs = new(input);
            int b;
            char[] chars = new char[2];
            while ((b = bs.ReadByte()) >= 0) {
                chars[0] = hexChars[b >> 4];
                chars[1] = hexChars[b & 0x0f];
                writer.Write(chars);
            }
        }
    }
}
