using System.Runtime.CompilerServices;
using System.Text;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Extensions;

public static class StringExtensions {
    extension(string? str) {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte[] DecodeUtf8() => Decode(str, Encoding.UTF8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte[] Decode(Encoding encoding) =>
            str is null ? [] : encoding.GetBytes(str);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte[] Decode(ITextCodec codec, CodecOptions? options) =>
            str is null ? [] : codec.Decode(str, options);

        public byte[] Decode(string encoding) {
            if (str is null) { return []; }

            var codec = TextCodec.CreateOrNull(encoding);
            return codec is null
                ? Encoding.GetEncoding(encoding).GetBytes(str)
                : codec.Decode(str);
        }
    }
}
