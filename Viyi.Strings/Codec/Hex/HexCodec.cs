using Viyi.Strings.Codec.Base16;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec {
    /// <summary>十六进制编解码器</summary>
    /// <remarks>和 Base16 功能一样，应该使用 Baes16 代替</remarks>
    public class HexCodec : Base16Codec { }

    internal class HexDecoder : Base16Decoder {
        public HexDecoder(CodecOptions options) : base(options) { }
    }

    class HexEncoder : Base16Encoder {
        public HexEncoder(CodecOptions options) : base(options) {
        }
    }
}
