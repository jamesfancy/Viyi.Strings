using System.IO;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Io;

namespace Viyi.Strings.Codec.Base64
{
    sealed class Base64Encoder : TextEncoder
    {
        static readonly char[] Base64Chars = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
            'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/', '='
        };

        public Base64Encoder(Options.CodecOptions options) : base(options) { }

        protected override void Encode(ICodecTextWriter writer, Stream input)
        {
            byte[] buffer = new byte[3];
            char[] chars = new char[4];

            int readCount;
            while ((readCount = input.Read(buffer, 0, 3)) == 3)
            {
                int combo = buffer[0] << 16 | buffer[1] << 8 | buffer[2];
                chars[0] = Base64Chars[combo >> 18];
                chars[1] = Base64Chars[combo >> 12 & 0x3f];
                chars[2] = Base64Chars[combo >> 6 & 0x3f];
                chars[3] = Base64Chars[combo & 0x3f];
                writer.Write(chars);
            }

            writeRest();

            void writeRest()
            {
                switch (readCount)
                {
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

                // writing here
                writer.Write(chars);
            }

            void encodeLast2(char[] chars, byte b1, byte b2)
            {
                int combo = b1 << 8 | b2;
                chars[0] = Base64Chars[combo >> 10];
                chars[1] = Base64Chars[combo >> 4 & 0x3f];
                chars[2] = Base64Chars[combo << 2 & 0x3f];
                chars[3] = Base64Chars[64];
            }

            void encodeLast1(char[] chars, byte b1)
            {
                chars[0] = Base64Chars[b1 >> 2];
                chars[1] = Base64Chars[b1 << 4 & 0x3f];
                chars[2] = Base64Chars[64];
                chars[3] = Base64Chars[64];
            }
        }
    }
}

