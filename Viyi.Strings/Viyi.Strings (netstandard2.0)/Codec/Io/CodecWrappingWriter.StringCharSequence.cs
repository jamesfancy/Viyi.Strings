namespace Viyi.Strings.Codec.Io {
    public partial class CodecWrappingWriter {
        class StringCharSequence : ArrayCharSequence {
            public StringCharSequence(string data) : base(data.ToCharArray()) { }
        }
    }
}

