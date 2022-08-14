using System;
using System.IO;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Io;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base32;

internal abstract class Base32DecoderBase : TextDecoder {
    const int BufferLength = 1024;

    // Base32 编码 8 个字符为 1 组
    const int GroupLength = 8;

    // Base32 每个编码表示 5 位
    const int CharBits = 5;
    const int ByteBits = 8;

    readonly long[] reverseCodes;

    readonly char[] buffer = new char[BufferLength];
    int offset = 0;
    int Rest => BufferLength - offset;

    Stream? output;

    internal Base32DecoderBase(CodecOptions options, long[] codes)
        : base(options) {
        reverseCodes = codes;
    }

    protected abstract bool IsValid(char ch);

    protected override ICodecTextReader WrapReader(TextReader reader) =>
        new CodecFilterableTextReader(reader, Options, IsValid);

    protected override void Decode(Stream output, ICodecTextReader codecReader) {
        this.output = output;
        var reader = new BufferedReader(codecReader);

        int count;
        while ((count = reader.Read(buffer, offset, Rest)) > 0) {
            DecodeBuffer(count);
        }

        switch (offset) {
            case 0: return;
            case 2:
            case 4:
            case 5:
            case 7:
                DecodeLast(offset);
                break;
            default:
                throw new CodecException("invalid Base32 data length");
        }
    }

    void DecodeBuffer(int count) {

        var length = offset + count;
        if (length < GroupLength) {
            offset += length;
            return;
        }

        var rest = length % GroupLength;
        int fixedLength = length - rest;

        for (var i = 0; i < fixedLength; i += GroupLength) {
            Decode(i);
        }

        if (rest > 0) {
            Array.Copy(buffer, fixedLength, buffer, 0, rest);
            offset = rest;
        }
    }

    void Decode(int start) {
        long n = 0L;
        for (int i = start; i < start + 8; i++) {
            n <<= CharBits;
            n |= reverseCodes[buffer[i]];
        }

        const int firstOffset = CharBits * ByteBits - ByteBits;
        for (int i = firstOffset; i >= 0; i -= ByteBits) {
            output!.WriteByte((byte) (n >> i & 0xffL));
        }
    }

    // count 只可能是 2、4、5、7（调用前逻辑保证）
    // 对应解码结果是 1, 2, 3, 4 字节
    void DecodeLast(int count) {
        long n = 0L;
        for (int i = 0; i < count; i++) {
            n <<= CharBits;
            n |= reverseCodes[buffer[i]];
        }

        int byteCount = count * CharBits / ByteBits;
        int restBits = count * CharBits % ByteBits;
        int firstOffset = byteCount * ByteBits - ByteBits + restBits;
        for (int i = firstOffset; i >= 0; i -= ByteBits) {
            output!.WriteByte((byte) (n >> i & 0xffL));
        }
    }
}
