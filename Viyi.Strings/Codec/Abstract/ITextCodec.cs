using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Abstract {
    public interface ITextCodec {
        /// <summary>
        /// 一次性编码所有 data，得到编码结果，适用于内容不是很多的情况
        /// </summary>
        string Encode(byte[] data, CodecOptions? options = null);

        /// <summary>
        /// 编码 data 中指定的内容
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="start">起始位置（索引号）</param>
        /// <param name="count">长度</param>
        /// <param name="options">配置项，可选</param>
        string Encode(byte[] data, int start, int count, CodecOptions? options = null);

        /// <summary>
        /// 一次解码所有数据，适用于数据量不是特别大的情况
        /// <summary>
        byte[] Decode(string code, CodecOptions? options = null);

        /// <summary>
        /// 创建 Encoder 用于更灵活的编码方式。
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        ITextEncoder CreateEncoder(CodecOptions? options = null);

        /// <summary>
        /// 创建 Decoder 用于更灵活的解码方式。
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        ITextDecoder CreateDecoder(CodecOptions? options = null);
    }
}
