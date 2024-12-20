using Viyi.Strings.Codec.Extensions;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64;

public static partial class Base64Extensions {
    const int DefaultBase64LineWidth = 76;

    public static byte[] DecodeBase64(this string base64) {
        return new Base64Codec().Decode(base64);
    }

    public static string EncodeBase64(this byte[] bytes, CodecOptions? options = null) {
        return bytes.IsEmpty()
            ? string.Empty
            : new Base64Codec().Encode(bytes, options);
    }

    public static string EncodeBase64(
        this byte[] bytes,
        Action<Base64CodecOptions.Builder> building
    ) {
        var optionsBuilder = Base64CodecOptions.Create();
        building?.Invoke(optionsBuilder);
        return EncodeBase64(bytes, optionsBuilder.Build());
    }

    public static string EncodeBase64(this byte[] bytes, int lineWidth) {
        return EncodeBase64(
            bytes,
            Base64CodecOptions.Create().SetLineWidth(lineWidth).Build()
        );
    }

    public static string EncodeBase64(this byte[] bytes, bool lineBreak) =>
        lineBreak ? EncodeBase64(bytes, DefaultBase64LineWidth) : EncodeBase64(bytes);

    public static string EncodeBase64(this string source, bool lineBreak) {
        return source.DecodeUtf8().EncodeBase64(lineBreak);
    }
}
