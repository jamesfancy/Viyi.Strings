namespace Viyi.Strings.Codec.Io;

public partial class CodecWrappingWriter {
    protected internal interface ICharSequence {
        bool HasMore { get; }
        int ToWriter(TextWriter writer, int limit);
    }

    abstract class CharSequence : ICharSequence {
        int offset;
        int restCount;

        public bool HasMore => restCount > 0;

        protected CharSequence(int offset, int count) {
            this.offset = offset;
            restCount = count;
        }

        public virtual int ToWriter(TextWriter writer, int limit) {
            int count = Math.Min(limit, restCount);
            Write(writer, offset, count);
            offset += count;
            restCount -= count;
            return count;
        }

        protected abstract void Write(TextWriter writer, int offset, int count);
    }

    class ArrayCharSequence : CharSequence {
        readonly char[] source;

        public ArrayCharSequence(char[] data) : this(data, 0, data.Length) { }

        public ArrayCharSequence(char[] data, int start, int count) : base(start, count) {
            source = data;
        }

        protected override void Write(TextWriter writer, int offset, int count) =>
            writer.Write(source, offset, count);
    }

    class StringCharSequence : CharSequence {
        readonly string source;

        public StringCharSequence(string data) : base(0, data.Length) {
            source = data;
        }

#if NETSTANDARD2_0
        protected override void Write(TextWriter writer, int offset, int count) {
            var buffer = source.ToCharArray(offset, count);
            writer.Write(buffer);
        }
#else
        protected override void Write(TextWriter writer, int offset, int count) =>
            writer.Write(source.AsSpan(offset, count));
#endif
    }
}
