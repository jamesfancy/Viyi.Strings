using System.IO;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Io;

namespace Viyi.Strings.Codec.Base64;

abstract class Base64EncoderBase : TextEncoder {
    readonly char[] base64Chars;

    protected Base64EncoderBase(char[] base64Chars, Options.CodecOptions options) : base(options) {
        this.base64Chars = base64Chars;
    }

    protected override void Encode(ICodecTextWriter writer, Stream input) {
        byte[] buffer = new byte[3];
        char[] chars = new char[4];

        int readCount;
        while ((readCount = input.Read(buffer, 0, 3)) == 3) {
            int combo = buffer[0] << 16 | buffer[1] << 8 | buffer[2];
            chars[0] = base64Chars[combo >> 18];
            chars[1] = base64Chars[combo >> 12 & 0x3f];
            chars[2] = base64Chars[combo >> 6 & 0x3f];
            chars[3] = base64Chars[combo & 0x3f];
            writer.Write(chars);
        }

        writeRest();

        void writeRest() {
            switch (readCount) {
                case 2:
                    encodeLast2(chars, buffer[0], buffer[1]);
                    // should write later
                    break;
                case 1:
                    encodeLast1(chars, buffer[0]);
                    // should writer later
                    break;
                default:
                    // All done. Does not need writing.
                    return;
            }

            writer.Write(chars);
        }

        void encodeLast2(char[] chars, byte b1, byte b2) {
            int combo = b1 << 8 | b2;
            chars[0] = base64Chars[combo >> 10];
            chars[1] = base64Chars[combo >> 4 & 0x3f];
            chars[2] = base64Chars[combo << 2 & 0x3f];
            chars[3] = base64Chars[64];
        }

        void encodeLast1(char[] chars, byte b1) {
            chars[0] = base64Chars[b1 >> 2];
            chars[1] = base64Chars[b1 << 4 & 0x3f];
            chars[2] = base64Chars[64];
            chars[3] = base64Chars[64];
        }
    }
}
