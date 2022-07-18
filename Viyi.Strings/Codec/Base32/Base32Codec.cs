using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base32;

/// <summary>Base32 编/解码</summary>
/// <remarks>
/// 依据 RFC3548 中 Base 32 编码规则 (https://www.rfc-editor.org/rfc/rfc3548)
/// </remarks>
public class Base32Codec : CodecBase {
    public override ITextDecoder CreateDecoder(CodecOptions? options = null) {
        return new Base32Decoder(options ?? CodecOptions.CreateDefault());
    }

    public override ITextEncoder CreateEncoder(CodecOptions? options = null) {
        return new Base32Encoder(options ?? CodecOptions.CreateDefault());
    }
}
