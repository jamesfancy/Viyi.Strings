using System;
using System.IO;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec
{
    [Obsolete("看样子没有必要啊，只是需要一个跳过无效字符的 Reader")]
    public class CodecReader : CodecAccessor
    {
        readonly Stream? inputStream;
        readonly TextReader? inputReader;
        protected TextReader Reader { get; }

        public bool CloseInput { get; set; }

        public CodecReader(Stream stream, CodecOptions? options = null)
             : base(options)
        {
            this.inputStream = stream;
            this.Reader = WrapStream(stream);
            CloseInput = true;
        }

        public CodecReader(TextReader reader, CodecOptions? options = null)
            : base(options)
        {
            this.inputReader = reader;
            this.Reader = reader;
        }

        public CodecReader(string s, CodecOptions options = null)
            : base(options)
        {
            this.Reader = new StringReader(s);
        }

        private static TextReader WrapStream(Stream stream) => new StreamReader(stream);

        public int Read(char[] buffer, int start, int count)
        {
            return Reader.Read(buffer, start, count);
        }

        public override void Dispose()
        {
            if (CloseInput)
            {
                this.inputStream?.Close();
                this.inputReader?.Close();
            }
        }
    }
}
