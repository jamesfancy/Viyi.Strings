using System.Runtime.CompilerServices;
using Viyi.Strings.Codec.Extensions;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64;

public static partial class Base64Extensions {
    const int DefaultBase64LineWidth = 76;

    extension(string source) {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte[] DecodeBase64() => new Base64Codec().Decode(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string EncodeBase64(bool lineBreak) => source.DecodeUtf8().EncodeBase64(lineBreak);
    }

    extension(byte[] bytes) {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string EncodeBase64(CodecOptions? options = null) =>
            bytes.IsEmpty() ? string.Empty : new Base64Codec().Encode(bytes, options);

        public string EncodeBase64(Action<Base64CodecOptions.Builder> building) {
            var optionsBuilder = Base64CodecOptions.Create();
            building?.Invoke(optionsBuilder);
            return EncodeBase64(bytes, optionsBuilder.Build());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string EncodeBase64(int lineWidth) =>
            EncodeBase64(bytes, Base64CodecOptions.Create().SetLineWidth(lineWidth).Build());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string EncodeBase64(bool lineBreak) =>
            lineBreak ? EncodeBase64(bytes, DefaultBase64LineWidth) : EncodeBase64(bytes);

    }
}
