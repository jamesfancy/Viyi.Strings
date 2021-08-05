using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec
{
    public class HexCodec : CodecBase
    {
        public override ITextDecoder CreateDecoder(CodecOptions? options = null)
        {
            return new HexDecoder(options ?? CodecOptions.Default);
        }

        public override ITextEncoder CreateEncoder(CodecOptions? options = null)
        {
            return new HexEncoder(options ?? CodecOptions.Default);
        }
    }
}
