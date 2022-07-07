using System;
using Viyi.Strings.Codec.Extensions;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64;

public static partial class Base64Extensions {
    public static byte[] DecodeBase64Url(this string base64) {
        return new Base64UrlCodec().Decode(base64);
    }

    public static string EncodeBase64Url(this byte[] bytes, CodecOptions? options = null) {
        return bytes.IsEmpty()
            ? string.Empty
            : new Base64UrlCodec().Encode(bytes, options);
    }

    public static string EncodeBase64Url(
        this byte[] bytes,
        Action<Base64CodecOptions.Builder> building
    ) {
        var optionsBuilder = Base64CodecOptions.Create();
        building?.Invoke(optionsBuilder);
        return EncodeBase64Url(bytes, optionsBuilder.Build());
    }

    public static string EncodeBase64Url(this byte[] bytes, int lineWidth) {
        return EncodeBase64Url(
            bytes,
            Base64CodecOptions.Create().SetLineWidth(lineWidth).Build()
        );
    }

    public static string EncodeBase64Url(this byte[] bytes, bool lineBreak) =>
        lineBreak ? EncodeBase64Url(bytes, DefaultBaes64LineWidth) : EncodeBase64Url(bytes);

    public static string EncodeBaes64Url(this string source, bool lineBreak) {
        return source.DecodeUtf8().EncodeBase64Url(lineBreak);
    }
}
