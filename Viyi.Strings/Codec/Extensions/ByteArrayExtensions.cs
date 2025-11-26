using System.Runtime.CompilerServices;
using System.Text;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Extensions;

public static class ByteArrayExtensions {
    extension(byte[] bytes) {
        internal bool IsEmpty() => bytes.Length == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string EncodeUtf8() => Encode(bytes, Encoding.UTF8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string EncodeUtf8(int start, int count) => Encode(bytes, start, count, Encoding.UTF8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Encode(Encoding encoding) => encoding.GetString(bytes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Encode(int start, int count, Encoding encoding) =>
            encoding.GetString(bytes, start, count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Encode(ITextCodec codec, CodecOptions? options) => codec.Encode(bytes, options);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Encode(int start, int count, ITextCodec codec, CodecOptions? options) =>
            codec.Encode(bytes, start, count, options);

        public string Encode(string encoding) {
            if (IsEmpty(bytes)) { return string.Empty; }

            var codec = TextCodec.CreateOrNull(encoding);
            return codec is null
                ? Encoding.GetEncoding(encoding).GetString(bytes)
                : codec.Encode(bytes);
        }

        public string Encode(int start, int count, string encoding) {
            if (IsEmpty(bytes) || count == 0) { return string.Empty; }

            var codec = TextCodec.CreateOrNull(encoding);
            return codec is null
                ? Encoding.GetEncoding(encoding).GetString(bytes, start, count)
                : codec.Encode(bytes, start, count);
        }
    }
}
