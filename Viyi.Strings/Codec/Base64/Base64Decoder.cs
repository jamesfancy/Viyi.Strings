using System;
using System.IO;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Io;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64 {
    sealed class Base64Decoder : TextDecoder {
        const int BufferLength = 1024;

        readonly char[] buffer = new char[BufferLength];
        int offset = 0;
        int Rest => BufferLength - offset;

        Stream? output;

        internal Base64Decoder(CodecOptions options)
            : base(options) { }

        protected override ICodecTextReader WrapReader(TextReader reader) =>
            new CodecFilterableTextReader(reader, Options, ReverseCharset.IsValid);

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
            int v = ReverseCharset.ToInt(buffer[start]) << 18
                | ReverseCharset.ToInt(buffer[start + 1]) << 12
                | ReverseCharset.ToInt(buffer[start + 2]) << 6
                | ReverseCharset.ToInt(buffer[start + 3]);

            output!.WriteByte((byte) (v >> 16));
            output.WriteByte((byte) (v >> 8 & 0xff));
            output.WriteByte((byte) (v & 0xff));
        }

        void DecodeLast2() {
            output!.WriteByte((byte) (
                ReverseCharset.ToInt(this.buffer[0]) << 2
                | ReverseCharset.ToInt(this.buffer[1]) >> 4
            ));
        }

        void DecodeLast3() {
            int v = ReverseCharset.ToInt(buffer[0]) << 10
                | ReverseCharset.ToInt(buffer[1]) << 4
                | ReverseCharset.ToInt(buffer[2]) >> 2;
            output!.WriteByte((byte) (v >> 8));
            output.WriteByte((byte) (v & 0xff));
        }
    }
}

