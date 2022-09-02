using System;
using System.IO;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Io;
using Viyi.Strings.Extensions;
using Viyi.Strings.Extensions.Internal;

namespace Viyi.Strings.Codec.Base32;

internal abstract class Base32EncoderBase : TextEncoder {
    const int PadIndex = 32;
    const int CharBits = 5;
    const int ByteBits = 8;


    readonly char[] b32Chars;

    protected Base32EncoderBase(Options.CodecOptions options, char[] chars) : base(options) {
        b32Chars = chars;
    }

    protected override void Encode(ICodecTextWriter writer, Stream input) {
        const int groupBytes = 5;
        const int groupChars = 8;

        byte[] buffer = new byte[groupBytes];
        char[] chars = new char[groupChars];

        int readCount;
        while ((readCount = input.Read(buffer, 0, CharBits)) == CharBits) {
            chars[0] = b32Chars[buffer[0] >> 3];
            chars[1] = b32Chars[(buffer[0] & 0x07) << 2 | (buffer[1] >> 6)];
            var combo = buffer.ToInt32(1);
            chars[2] = b32Chars[combo >> 25 & 0x1f];
            chars[3] = b32Chars[combo >> 20 & 0x1f];
            chars[4] = b32Chars[combo >> 15 & 0x1f];
            chars[5] = b32Chars[combo >> 10 & 0x1f];
            chars[6] = b32Chars[combo >> 5 & 0x1f];
            chars[7] = b32Chars[combo & 0x1f];
            writer.Write(chars);
        }

        if (readCount > 0) { writeBytes(); }

        void writeBytes() {
            // Base32 每组处理 5 个字节；如果分组有余，最大余数为 4（刚好够一个 Int32）
            const int restMaxBytes = 4;

            byte[] bytes = new byte[restMaxBytes];
            Array.Copy(buffer, bytes, readCount);
            var combo = bytes.ToInt32();
            int charsCount = (readCount * ByteBits + CharBits - 1) / CharBits;

            // 特殊处理最后一个
            int i = charsCount - 1;
            if (charsCount == 7) {
                const int restBits = restMaxBytes * ByteBits % CharBits;
                chars[i] = b32Chars[combo << (CharBits - restBits) & 0x1f];
                combo >>= restBits;
                i--;
            }
            else {
                combo >>= restMaxBytes * ByteBits - charsCount * CharBits;
            }

            for (; i >= 0; i--, combo >>= CharBits) {
                chars[i] = b32Chars[combo & 0x1f];
            }

            chars.Fill(b32Chars[PadIndex], charsCount, groupChars - charsCount);

            writer.Write(chars);
        }
    }
}
