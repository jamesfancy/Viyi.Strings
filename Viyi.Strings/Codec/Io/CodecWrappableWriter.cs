using System;
using System.Diagnostics;
using System.IO;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Io
{
    public class CodecWrappableWriter : CodecTextWriter
    {
        static readonly string[] EndOfLines = new[]
        {
            Environment.NewLine,
            "\n",
            "\r\n",
            "\r",
        };

        readonly int lineWidth;
        readonly string endOfLine;
        int restWidth;

        public CodecWrappableWriter(TextWriter writer, CodecOptions options)
            : base(writer, options)
        {
            restWidth = lineWidth = options.LineWidth;
            endOfLine = EndOfLines[(int)options.LineEnding];
        }

        public override void Write(char[] buffer) => Write(buffer, 0, buffer.Length);

        public override void Write(char[] buffer, int start, int length)
        {
            if (restWidth >= length)
            {
                Writer.Write(buffer, start, length);
                OccupyRestWidth(length);
            }
            else
            {
                writeSegments();
            }

            void writeSegments()
            {
                int restLength = length;
                int position = start;
                while (restLength > 0)
                {
                    int count = Math.Min(restWidth, restLength);
                    Writer.Write(buffer, position, count);
                    position += count;
                    restLength -= count;
                    OccupyRestWidth(count);
                }
            }
        }

        public override void Write(string segment)
        {
            if (restWidth >= segment.Length)
            {
                Writer.Write(segment);
                OccupyRestWidth(segment.Length);
            }
            else
            {
                writeSegments();
            }

            void writeSegments()
            {
                char[] buffer = new char[lineWidth];
                int restLength = segment.Length;
                int position = 0;
                while (restLength > 0)
                {
                    int count = Math.Min(restWidth, restLength);
                    segment.CopyTo(position, buffer, 0, count);
                    Writer.Write(buffer, 0, count);
                    position += count;
                    restLength -= count;
                    OccupyRestWidth(count);
                }
            }
        }

        private void OccupyRestWidth(int count)
        {
            Debug.Assert(count <= restWidth);
            restWidth -= count;
            if (restWidth == 0)
            {
                Writer.Write(endOfLine);
                restWidth = lineWidth;
            }
        }
    }
}
