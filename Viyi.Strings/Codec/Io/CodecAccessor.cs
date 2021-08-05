using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Io
{
    public abstract class CodecAccessor
    {
        protected CodecOptions Options { get; }

        /// <summary>
        /// 传入一个配置，根据配置来决定如何读/写
        /// </summary>
        /// <param name="options"></param>
        protected CodecAccessor(CodecOptions? options)
        {
            this.Options = options ?? CodecOptions.CreateDefault();
        }
    }
}
