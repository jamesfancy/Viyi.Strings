using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64; 
public static partial class Base64Extensions {
    /// <summary>
    /// 对 Base64 编码字符串进行解码，解码时兼容处理 `+/-_` 这 4 个特号。
    /// </summary>
    /// <remarks>
    /// 兼容处理与 base 64 和 base 64 url 的区别在于：
    /// base 64 会把 `-_` 作为无效字符过滤掉；
    /// base 64 url 会把 `+/` 作为无效字符过滤掉；
    /// 而 base 64 compatible 会保留所有 `+/-_` 字符，把它们当作有效字符处理。
    /// </remarks>
    public static byte[] DecodeBase64Compatible(this string base64) {
        var decoder = new Base64CompatibleDecoder(CodecOptions.CreateDefault());
        return decoder.Decode(base64);
    }
}
