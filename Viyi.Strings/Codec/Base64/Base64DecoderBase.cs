using System;
using System.IO;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Io;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64;

abstract class Base64DecoderBase : TextDecoder {
    const int BufferLength = 1024;

    readonly ReverseCharset charset;
    readonly char[] buffer = new char[BufferLength];
    int offset = 0;
    int Rest => BufferLength - offset;

    Stream? output;

    protected Base64DecoderBase(ReverseCharset charset, CodecOptions options)
        : base(options) {
        this.charset = charset;
    }

    protected override ICodecTextReader WrapReader(TextReader reader) =>
        new CodecFilterableTextReader(reader, Options, charset.IsValid);

    protected override void Decode(Stream output, ICodecTextReader codecReader) {
        this.output = output;
        var reader = new BufferedReader(codecReader);

        int count;
        while ((count = reader.Read(buffer, offset, Rest)) > 0) {
            DecodeBuffer(count);
        }

        switch (offset) {
            case 0:
                return;
            case 3:
                DecodeLast3();
                break;
            case 2:
                DecodeLast2();
                break;
            default:
                throw new CodecException("invalid base64 data length");
        }
    }

    void DecodeBuffer(int count) {
        const int groupLength = 4;

        var length = offset + count;
        if (length < groupLength) {
            offset += length;
            return;
        }

        var rest = length % groupLength;
        int fixedLength = length - rest;

        for (var i = 0; i < fixedLength; i += groupLength) {
            Decode(i);
        }

        if (rest > 0) {
            Array.Copy(buffer, fixedLength, buffer, 0, rest);
            offset = rest;
        }
    }

    void Decode(int start) {
        int v = charset.ToInt(buffer[start]) << 18
            | charset.ToInt(buffer[start + 1]) << 12
            | charset.ToInt(buffer[start + 2]) << 6
            | charset.ToInt(buffer[start + 3]);

        output!.WriteByte((byte) (v >> 16));
        output.WriteByte((byte) (v >> 8 & 0xff));
        output.WriteByte((byte) (v & 0xff));
    }

    void DecodeLast2() {
        output!.WriteByte((byte) (
            charset.ToInt(this.buffer[0]) << 2
            | charset.ToInt(this.buffer[1]) >> 4
        ));
    }

    void DecodeLast3() {
        int v = charset.ToInt(buffer[0]) << 10
            | charset.ToInt(buffer[1]) << 4
            | charset.ToInt(buffer[2]) >> 2;
        output!.WriteByte((byte) (v >> 8));
        output.WriteByte((byte) (v & 0xff));
    }
}

