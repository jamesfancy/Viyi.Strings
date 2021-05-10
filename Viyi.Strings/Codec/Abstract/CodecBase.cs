using System;
using System.Collections.Generic;
using System.IO;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Abstract
{

    public abstract partial class CodecBase : ITextCodec
    {
        public abstract void Decode(Stream outStream, IEnumerable<string> lines);

        public virtual byte[] Decode(string code)
        {
            using (var stream = new MemoryStream())
            {
                Decode(stream, new[] { code });
                return stream.ToArray();
            }
        }

        public virtual void Decode(Stream outStream, TextReader reader)
        {
            Decode(outStream, ReadLines(reader));
        }

        protected virtual IEnumerable<string> ReadLines(TextReader reader)
        {
            using (reader)
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        protected abstract void Encode(ICharWriter writer, IEnumerable<byte[]> data);

        protected virtual ICharWriter MakeCharWriter(TextWriter writer)
            => LineLength <= 0
                ? (ICharWriter)new DirectCharWriter(writer)
                : new LineBreakCharWriter(writer, LineLength, EndOfLine);

        public virtual void Encode(TextWriter writer, IEnumerable<byte[]> data)
        {
            Encode(MakeCharWriter(writer), data);
        }

        public virtual string Encode(byte[] data) => Encode(data, 0, data.Length);

        public virtual string Encode(byte[] data, int start, int length)
        {
            byte[] segment = new byte[length];
            Array.Copy(data, start, segment, 0, length);
            return Encode(new byte[][] { segment });
        }

        public virtual string Encode(IEnumerable<byte[]> data)
        {
            using (var writer = new StringWriter())
            {
                Encode(writer, data);
                return writer.ToString();
            }
        }

        public virtual string Encode(Stream inStream)
        {
            using (var writer = new StringWriter())
            {
                Encode(writer, inStream);
                return writer.ToString();
            }
        }

        public virtual void Encode(TextWriter writer, Stream inStream)
        {
            Encode(writer, ReadBytes(inStream));
        }

        protected virtual IEnumerable<byte[]> ReadBytes(Stream inStream, int bufferSize = 64 * 1024)
        {
            byte[] buffer = new byte[bufferSize];
            using (inStream)
            {
                int count;
                while ((count = inStream.Read(buffer, 0, bufferSize)) == bufferSize)
                {
                    yield return buffer;
                }

                if (count > 0)
                {
                    byte[] more = new byte[count];
                    Array.Copy(buffer, 0, more, 0, count);
                    yield return more;
                }
            }
        }
    }
}
