using System;
using System.IO;

namespace Viyi.Strings.Codec.Abstract
{
    partial class CodecBase
    {
        protected interface ICharWriter : IDisposable
        {
            void Write(char ch);
            void Flush();
        }

        // 不能这么干，要从 System.IO.Writer 继承才行啊
        protected sealed class DirectCharWriter : ICharWriter
        {
            readonly TextWriter writer;

            public DirectCharWriter(TextWriter writer)
            {
                this.writer = writer;
            }

            public void Write(char ch)
            {
                writer.Write(ch);
            }

            public void Flush()
            {
                writer.Flush();
            }

            public void Dispose()
            {
                writer.Dispose();
            }
        }

        protected sealed class LineBreakCharWriter : ICharWriter
        {
            readonly TextWriter writer;
            readonly int lineLength;
            readonly string newLine;

            int count = 0;

            public LineBreakCharWriter(TextWriter writer, int lineLength, string newLine)
            {
                this.writer = writer;
                this.lineLength = lineLength;
                this.newLine = newLine;
            }

            public void Write(char ch)
            {
                if (count++ >= lineLength)
                {
                    count = 1;
                    writer.Write(newLine);
                }

                writer.Write(ch);
            }

            public void Flush()
            {
                writer.Flush();
            }

            public void Dispose()
            {
                writer.Dispose();
            }
        }
    }
}
